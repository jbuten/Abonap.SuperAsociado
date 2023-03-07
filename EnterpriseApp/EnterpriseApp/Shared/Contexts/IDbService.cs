using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseApp.Shared.Contexts
{
    using Abonap.Users;
    using EnterpriseApp.Shared.Entities;
    using EnterpriseApp.Shared.Models;
    using MongoDB.Bson;
    using MongoDB.Driver;

    public partial interface IDbService
    {
        bool UpdateLists();
        Task<List<Models.ListItem>> GetLists(string parameters);

        Task<List<ListApp>> ListApps();
        Task<string> LogInsert(string metodo, string req, string res);
    }

    public partial class DbService : IDbService
    {
        private string JWTKey;

        private readonly IMongoDatabase database;
        private readonly IMongoCollection<Log> _Log;
        private readonly IMongoCollection<Models.ListItem> _Lists;
        private readonly IMongoCollection<Abonap.Users.User> _Users;
        private readonly IMongoCollection<App> _Apps;

        private readonly IMongoCollection<Empresa> _Empresa;
        private readonly IMongoCollection<Project> _Project;

        private readonly IMongoCollection<Site> _Sites;
        private readonly IMongoCollection<UserSitePuerta> _UserSitePuerta;

        public DbService(IAppSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            database = client.GetDatabase(settings.DatabaseName);
            JWTKey = settings.JWTKey;

            Console.WriteLine("settings.ConnectionString: {0}", settings.ConnectionString);

            bool isMongoLive = database.RunCommandAsync((Command<BsonDocument>)"{ping:1}").Wait(1000);

            if (isMongoLive)
            {
                Console.WriteLine("Conectado!");
            }
            else
            {
                Console.WriteLine("NOconectado");
            }

            _Log = database.GetCollection<Log>("logs");
            _Lists = database.GetCollection<Models.ListItem>("lists");
            _Users = database.GetCollection<Abonap.Users.User>("users");
            _Apps = database.GetCollection<App>("applications");

            _Empresa = database.GetCollection<Empresa>("empresas");
            _Project = database.GetCollection<Project>("proyectos");
            _Sites = database.GetCollection<Site>("sites");
            _UserSitePuerta = database.GetCollection<UserSitePuerta>("user_site_puerta");

        }

        public async Task<string> LogInsert(string metodo, string req, string res)
        {
            try
            {
                var log = new Log { Fecha = DateTime.Now, Metodo = metodo, request = req, response = res };

                await _Log.InsertOneAsync(log);

                return log.Id.ToString();
            }
            catch (Exception ex) { return ex.Message; }
        }

        public async Task<List<Models.ListItem>> GetLists(string parameters)
        {
            FilterDefinition<Models.ListItem> filter = Builders<Models.ListItem>.Filter.Eq(a => a.Id, parameters);
            if (parameters.Split("|").Count() >= 1)
            {
                filter = Builders<Models.ListItem>.Filter.In(a => a.Type, parameters.Split("|"));
            }
            return await _Lists.Find(filter).ToListAsync();
        }

        public bool UpdateLists()
        {
            bool res = false;

            List<Models.ListItem> companies = new List<Models.ListItem>();

            var _companies = _Users.Distinct(a => a.Company, new BsonDocument());

            foreach (var d in _companies.ToList())
            {
                string ID = EnterpriseApp.Shared.Tools.StrToID(d ?? "NONE", "COMPANY");

                Console.WriteLine("{0} --- {1}", ID, d ?? "NONE");

                var filter = Builders<Models.ListItem>.Filter.Eq(a => a.Id, ID);

                _Lists.ReplaceOne(filter, (new Models.ListItem { Id = ID, Type = "COMPANY", Value = d ?? "NONE" }), new ReplaceOptions { IsUpsert = true });
            }


            var _ubicaciones = _Users.Distinct(a => a.PhysicalDeliveryOfficeName, new BsonDocument());

            foreach (var d in _ubicaciones.ToList())
            {
                string ID = EnterpriseApp.Shared.Tools.StrToID(d);

                Console.WriteLine("{0} --- {1}", ID, d);

                var filter = Builders<Models.ListItem>.Filter.Eq(a => a.Id, ID);

                _Lists.ReplaceOne(filter, (new Models.ListItem { Id = ID, Type = "LOCATION", Value = d }), new ReplaceOptions { IsUpsert = true });
            }


            var _department = _Users.Distinct(a => a.Department, new BsonDocument());

            foreach (var d in _department.ToList())
            {
                string ID = EnterpriseApp.Shared.Tools.StrToID(d);

                Console.WriteLine("{0} --- {1}", ID, d);

                var filter = Builders<Models.ListItem>.Filter.Eq(a => a.Id, ID);

                _Lists.ReplaceOne(filter, (new Models.ListItem { Id = ID, Type = "DEPARTMENT", Value = d }), new ReplaceOptions { IsUpsert = true });
            }

            return res;
        }


        public async Task<List<ListApp>> ListApps()
        {
            List<ListApp> data = new List<ListApp>();

            string json = "{ 'Inactive':false , 'Rols': {$elemMatch :  { 'Inactive': true }} }";

            List<string> par = new List<string>();

            json = "{ 'Inactive': false }";

            //System.Console.WriteLine(json);

            BsonDocument document = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>(json);

            data = await _Apps.Find(document)
                .Sort("{Name: 1}")
                .Project(x => new
                ListApp
                {
                    Id = x.Id,
                    Name = x.Name,
                    Rols = (from q in x.Rols
                            where q.Inactive == false
                            select new DataItem { Id = q.Id, Value = q.Name }).ToList(),
                }).ToListAsync();

            return data;
        }


    }

}
