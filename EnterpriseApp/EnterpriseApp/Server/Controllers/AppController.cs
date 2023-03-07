using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseApp.Server.Controllers
{
    using EnterpriseApp.Shared.Entities;
    using EnterpriseApp.Shared.Models;
    using EnterpriseApp.Shared.Contexts;

    [Route("api/[controller]")]
    [ApiController]
    public class AppController : ControllerBase
    {
        private readonly IDbService db;

        public AppController(IDbService _db)
        {
            db = _db;
        }

        [HttpGet]
        //[Produces("application/json")]
        public Task<List<App>> GetAsync()
        {
            List<App> res = new List<App>();
            try
            {
                res = db.GetApplications();
            }
            catch (Exception ex)
            {
                res.Add(new App { Name = ex.Message });
            }
            return Task.FromResult(res);
        }

        [HttpGet("Items")]
        public async Task<List<ListItem>> GetItemsAsync()
        {
            List<ListItem> res = new List<ListItem>();
            try
            {
                res = await db.GetAppItems();
            }
            catch (Exception ex)
            {
                res.Add(new ListItem { Id = "0", Type = "app", Value = ex.Message });
            }
            return res;
        }


        [HttpGet("Info")]
        public async Task<List<DataItem>> GetInfoAsync()
        {
            List<DataItem> res = new List<DataItem>();
            try
            {
                res = await db.GetInfo();
            }
            catch (Exception ex)
            {
                res.Add(new DataItem { Id = "0", Value = ex.Message });
            }
            return res;
        }


        [HttpGet("{id}")]
        public async Task<App> GetAsync(string id)
        {
            App res = new App();
            try
            {
                res = await db.GetApplication(id);
            }
            catch (Exception ex)
            {
                res.Name = ex.Message;
            }
            return res;
        }


        [HttpPost]
        public Task<List<App>> SaveApp([FromBody] App application)
        {
            List<App> res = new List<App>();

            try
            {
                db.SaveApplication(application);
                res = db.GetApplications();
            }
            catch (Exception ex)
            {
                res.Add(new App { Name = ex.Message });
            }
            return Task.FromResult(res);
        }

        [HttpPost("SaveRol")]
        public async Task<App> SaveAppRol([FromBody] App application)
        {
            App app = application;

            try
            {
                db.SaveApplication(application);
                app = await GetAsync(application.Id);
            }
            catch (Exception ex)
            {
                app.Rols.Add(new Rol { Id = "0", Name = ex.Message });
            }

            return app;
        }

    }
}
