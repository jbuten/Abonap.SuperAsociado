using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseApp.Server.Controllers
{
    using EnterpriseApp.Shared.Contexts;
    using EnterpriseApp.Shared.Models;
    using EnterpriseApp.Shared.Entities;


    [Route("api/[controller]")]
    [ApiController]
    public class DataListController : ControllerBase
    {
        private readonly IDbService db;

        public DataListController(IDbService _db)
        {
            db = _db;
        }

        [HttpGet]
        public string GetItems()
        {
            return "Jose Manuel Buten Peralta".Split("|").Count().ToString();
        }

        [HttpGet("{type}")]
        public async Task<List<ListItem>> GetItemsAsync(string type)
        {
            List<ListItem> res = new List<ListItem>();
            try
            {
                res = await db.GetLists(type);

                if (type.Contains("EMPRESAS"))
                {
                    var empresas = await db.GetEmpresas();
                    var emp =
                        from q in empresas
                        where q.Inactive == false
                        select new ListItem { Id = q.Codigo, Value = q.Nombre, Type = "EMPRESAS" };
                    res.AddRange(emp.OrderBy(a => a.Value));
                }

                if (type.Contains("USERS"))
                {
                    var users = await db.GetUsersList(false);
                    res.AddRange(users.OrderBy(a => a.Value));
                }

            }
            catch (Exception ex)
            {
                res.Add(new ListItem { Id = "0", Type = "app", Value = ex.Message });
            }
            return res;
        }

        [HttpGet("Apps")]
        public async Task<List<ListApp>> GetAppsAsync()
        {
            List<ListApp> res = new List<ListApp>();
            try
            {
                res = await db.ListApps();
            }
            catch (Exception ex)
            {
                res.Add(new ListApp { Id = "0", Name = ex.Message });
            }
            return res;
        }

    }
}
