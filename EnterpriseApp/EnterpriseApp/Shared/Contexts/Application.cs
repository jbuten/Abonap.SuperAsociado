using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseApp.Shared.Contexts
{

    using MongoDB.Driver;
    using EnterpriseApp.Shared.Entities;
    using EnterpriseApp.Shared.Models;

    public partial interface IDbService
    {
        public List<App> GetApplications();
        public List<App> GetApplications(List<string> _in);
        public Task<App> GetApplication(string id);
        string SaveApplication(App application);
        Task<List<ListItem>> GetAppItems();

    }

    public partial class DbService
    {
        public List<App> GetApplications()
        {
            return _Apps.Find(FilterDefinition<App>.Empty).ToList();
        }

        public List<App> GetApplications(List<string> _in)
        {
            var filter = Builders<App>.Filter.In(a => a.Id, _in);
            filter &= Builders<App>.Filter.Eq(x => x.Inactive, false);

            return _Apps.Find(filter).ToList();
        }
        public async Task<App> GetApplication(string id)
        {
            return await _Apps.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public string SaveApplication(App application)
        {
            var reg = _Apps.Find(x => x.Id == application.Id).FirstOrDefault();
            if (reg == null)
            {
                _Apps.InsertOne(application);
            }
            else
            {
                _Apps.ReplaceOne(x => x.Id == application.Id, application);
            }
            return "ok";
        }

        public async Task<List<ListItem>> GetAppItems()
        {
            return await _Apps.Find(x => true).Project(x => new ListItem { Type = "app", Id = x.Id, Value = x.Name }).ToListAsync();
        }

    }

}
