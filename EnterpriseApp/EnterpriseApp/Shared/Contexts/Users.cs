using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseApp.Shared.Contexts
{
    using Abonap.Users;
    using EnterpriseApp.Shared.Models;
    using MongoDB.Bson;
    using MongoDB.Driver;

    public partial interface IDbService
    {
        Task<User> UserAdd(User usr);
        User UserADUpdate(User usr);
        Task<User> UserUpdate(User usr);
        Task<User> User(string id);
        Task<List<User>> Users();
        Task<List<User>> UsersCustomQuery(string query);
        Task<List<UserItem>> UserSearch(List<string> parameters);
        Task<List<UserRol>> RolSave(SaveRol parameters);
        Task<List<UserRol>> RolDelete(SaveRol parameters);
        Task<List<DataItem>> GetInfo();
        Task<List<ListItem>> GetUsersList(bool all);
        Task<List<User>> GetUsersForUpdate();

        Task<List<UserListItem>> ListUsersFilter(string like);
        Task<List<UserListItem>> ListUsersCFilter(string like);
    }


    public partial class DbService
    {
        public DbService()
        {
        }

        public async Task<User> User(string id)
        {
            /*
             * var filter = Builders<User>.Filter.Eq(x => x.Enabled, true);
            filter &= (Builders<User>.Filter.Eq(x => x.Username, "r.fernandez0000000") | Builders<User>.Filter.Eq(x => x.SAMAccountName, "PSRSF"));

            FilterDefinition<User>[] filtros = {
            builder.Eq(x => x.Username, "r.fernandez0000000"),
            builder.Eq(x => x.SAMAccountName, "PSRSF")
            };
            filter &= builder.Or( filtros );
            */
            string json = "{ $or: [ {\"Username\": \"" + id + "\"} ,{  \"SAMAccountName\" : /^" + id + "/i }  ] }";
            return await _Users.Find(json).FirstOrDefaultAsync();
        }

        public async Task<User> UserAdd(User usr)
        {
            await _Users.InsertOneAsync(usr);
            return usr;
        }

        public User UserADUpdate(User usr)
        {
            var filter = Builders<User>.Filter.Eq(a => a.Username, usr.Username);

            var reg = _Users.Find(filter).FirstOrDefault();

            if (reg != null)
            {

                //if (reg.PhotoPath != "user.png")
                //{
                //    usr.PhotoPath = reg.PhotoPath;
                //    usr.Photo = reg.Photo;
                //}
                usr.Id = reg.Id; usr.Rols = reg.Rols;
            }

            _Users.ReplaceOne(filter, usr, new ReplaceOptions { IsUpsert = true });

            return usr;
        }

        public async Task<User> UserUpdate(User usr)
        {
            var filter = Builders<User>.Filter.Eq(a => a.Username, usr.Username);

            var reg = await _Users.Find(filter).FirstOrDefaultAsync();

            if (reg != null) { usr.Id = reg.Id; usr.Rols = reg.Rols; }

            _Users.ReplaceOne(filter, usr, new ReplaceOptions { IsUpsert = true });

            return usr;
        }

        public async Task<List<UserItem>> UserSearch(List<string> parameters)
        {
            List<UserItem> data = new List<UserItem>();

            string json = "{ 'Sn': 'Buten Peralta',  'GivenName': 'Jose Manuel' , 'PhysicalDeliveryOfficeNameID': 'SITEABONAP',  quantity:  }";
            json = "{'CompanyID': { $nin: ['PROCESOIT', 'none']  }, 'Enabled': true }";
            json = "{'Enabled': false }";

            json = "{" + string.Join(",", parameters) + "}";

            //System.Console.WriteLine(json);

            BsonDocument document = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>(json);

            data = await _Users.Find(document)
                .Sort("{DisplayName: 1}")
                .Project(x => new
                UserItem
                {
                    Photo = x.PhotoPath,
                    UserName = x.Username,
                    DisplayName = x.DisplayName.Trim(),
                    Titulo = x.Title,
                    Department = x.Department,
                    DepartmentID = x.DepartmentID,
                    EmployeeCode = x.PostOfficeBox,
                    UserDomain = x.SAMAccountName,
                    Company = x.Company,
                    CompanyID = x.CompanyID,
                    LocationID = x.PhysicalDeliveryOfficeNameID,
                    Enabled = x.Enabled,
                    Apellidos = x.Sn,
                    Nombres = x.GivenName
                }).ToListAsync();

            return data;
        }

        public async Task<List<UserRol>> RolSave(SaveRol parameters)
        {

            var filter = Builders<User>.Filter.Eq(a => a.Username, parameters.UserName);

            User user = await _Users.Find(filter).FirstOrDefaultAsync();

            var app = _Apps.Find(x => x.Id == parameters.App).First();

            if (user.Rols.Any(a => a.App == parameters.App && a.Inactive == false))
            {
                foreach (var r in user.Rols.Where(a => a.App == parameters.App))
                {
                    r.Rol = parameters.Rol;
                    r.RolName = app.Rols.First(a => a.Id == parameters.Rol).Name;
                }
            }
            else
            {

                user.Rols.Add(new UserRol
                {
                    AppName = app.Name,
                    RolName = app.Rols.First(a => a.Id == parameters.Rol).Name,
                    App = parameters.App,
                    Rol = parameters.Rol,
                    AddBy = parameters.UpdateBy,
                    AddDate = DateTime.Now
                });
            }

            _Users.ReplaceOne(filter, user, new ReplaceOptions { IsUpsert = true });

            return user.Rols;
        }

        public async Task<List<UserRol>> RolDelete(SaveRol parameters)
        {
            var filter = Builders<User>.Filter.Eq(a => a.Username, parameters.UserName);

            User user = await _Users.Find(filter).FirstOrDefaultAsync();

            foreach (var r in user.Rols.Where(a => a.App == parameters.App))
            {
                r.Inactive = true; r.DeleteBy = parameters.UpdateBy; r.DeleteDate = DateTime.Now;
            }

            _Users.ReplaceOne(filter, user, new ReplaceOptions { IsUpsert = true });

            return user.Rols;
        }

        public Task<List<DataItem>> GetInfo()
        {
            var result = _Users.AsQueryable()
                .GroupBy(x => x.Enabled)
                .Select(x => new DataItem
                {
                    Id = (x.Key) ? "A" : "I",
                    Value = x.Count().ToString()
                }).ToList();

            int apps = _Apps.Find(new BsonDocument { }).ToList().Count();

            result.Add(new DataItem { Id = "apps", Value = apps.ToString() });

            return Task.FromResult(result);
        }

        public async Task<List<ListItem>> GetUsersList(bool all)
        {
            if (!all)
            {
                string[] persons = { "CITRICOSTROPICALES", "INDUSPALMA", "INDUVECA", "ABONAP" };
                return await _Users.Find(x => x.Enabled == true && persons.Contains(x.CompanyID))
                .Project(x => new ListItem { Type = "USERS", Id = x.Username, Value = x.Name }).ToListAsync();
            }
            else
            {
                return await _Users.Find(new BsonDocument { })
                    .Project(x => new ListItem { Type = "USERS", Id = x.Username, Value = x.Name }).ToListAsync();
            }
        }

        public async Task<List<User>> GetUsersForUpdate()
        {
            return await _Users.Find(new BsonDocument { })
                .Project(r => new User { Mail = r.Mail, PostOfficeBox = r.PostOfficeBox, Photo = r.Photo, PhotoPath = r.PhotoPath }
                )
                //.Limit(100)
                .ToListAsync();
        }

        public async Task<List<UserListItem>> ListUsersFilter(string like)
        {
            List<UserListItem> data = new List<UserListItem>();

            List<string> par = new List<string>();

            par.Add(" 'DisplayName': /" + like + "/i ");
            par.Add($" 'Enabled': true ");

            string json = "{" + string.Join(",", par) + "}";

            System.Console.WriteLine(json);

            BsonDocument document = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>(json);

            data = await _Users.Find(document)
                .Sort("{DisplayName: 1}")
                .Project(x => new
                UserListItem
                {
                    UserName = x.Username,
                    DisplayName = x.DisplayName.Trim(),
                    Title = x.Title,
                    PhotoPath = x.PhotoPath,
                    Email = x.Mail
                }).ToListAsync();

            return data;
        }

        public async Task<List<UserListItem>> ListUsersCFilter(string like)
        {
            List<UserListItem> data = new List<UserListItem>();

            List<string> par = new List<string>();

            par.Add(" 'DisplayName': /" + like + "/i ");
            par.Add($" 'Enabled': true ");

            string json = "{" + string.Join(",", par) + "}";

            System.Console.WriteLine(json);

            BsonDocument document = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>(json);

            data = await _Users.Find(document)
                .Sort("{DisplayName: 1}")
                .Project(x => new
                UserListItem
                {
                    UserName = x.Username,
                    DisplayName = x.DisplayName.Trim(),
                    Title = x.Title,
                    PhotoPath = x.PhotoPath,
                    Company = x.Company,
                    Email = x.Mail
                }).ToListAsync();

            return data;
        }

        public async Task<List<User>> Users()
        {
            /*
             * var filter = Builders<User>.Filter.Eq(x => x.Enabled, true);
            filter &= (Builders<User>.Filter.Eq(x => x.Username, "r.fernandez0000000") | Builders<User>.Filter.Eq(x => x.SAMAccountName, "PSRSF"));

            FilterDefinition<User>[] filtros = {
            builder.Eq(x => x.Username, "r.fernandez0000000"),
            builder.Eq(x => x.SAMAccountName, "PSRSF")
            };
            filter &= builder.Or( filtros );
            */
            string json = "{'Enabled': true}";
            return await _Users.Find(json).ToListAsync();
        }
        public async Task<List<User>> UsersCustomQuery(string query)
        {
            return await _Users.Find(query).ToListAsync();
        }

    }
}
