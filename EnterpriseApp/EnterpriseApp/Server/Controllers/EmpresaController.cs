using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseApp.Server.Controllers
{
    using EnterpriseApp.Shared.Contexts;
    using EnterpriseApp.Shared.Entities;
    using EnterpriseApp.Shared.Models;
    using System;
    using System.Drawing;


    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        private readonly IDbService db;
        private readonly string path = "";

        public EmpresaController(IDbService _db, IWebHostEnvironment webHostEnvironment)
        {
            db = _db;
            path = webHostEnvironment.ContentRootPath + "wwwroot\\img\\";
        }

        [HttpGet]
        public async Task<List<Empresa>> GetAsync()
        {
            List<Empresa> res = new List<Empresa>();
            try
            {
                res = await db.GetEmpresas();
            }
            catch (Exception ex)
            {
                res.Add(new Empresa { Nombre = ex.Message });
            }
            return res;
        }

        [HttpGet("list")]
        public async Task<List<ListItem>> GetListAsync()
        {
            List<ListItem> res = new List<ListItem>();
            try
            {
                res = await db.ListEmpresas();
            }
            catch (Exception ex)
            {
                res.Add(new ListItem { Id = "0", Type = "EMPRESA", Value = ex.Message });
            }
            return res;
        }



        [HttpGet("Centros/{idEmpresa}")]
        public async Task<List<ListItem>> GetCentrosAsync(string idEmpresa)
        {
            List<ListItem> res = new List<ListItem>();
            try
            {
                res = await db.ListCentros(idEmpresa);
            }
            catch (Exception ex)
            {
                res.Add(new ListItem { Id = "0", Type = "EMPRESA", Value = ex.Message });
            }
            return res;
        }


        [HttpPost]
        public async Task<List<Empresa>> SaveApp([FromBody] Empresa reg)
        {
            List<Empresa> res = new List<Empresa>();
            try
            {
                db.SaveEmpresa(reg);
                SaveByteArrayAsImage(path + reg.Codigo + ".png", reg.Logo.Replace("data:image/png;base64,", ""));
                res = await db.GetEmpresas();
            }
            catch (Exception ex)
            {
                res.Add(new Empresa { Nombre = ex.Message });
            }
            return res;
        }


        private void SaveByteArrayAsImage(string fullOutputPath, string base64String)
        {
            try
            {

                var bytes = Convert.FromBase64String(base64String);
                using (var imageFile = new FileStream(fullOutputPath, FileMode.Create))
                {
                    imageFile.Write(bytes, 0, bytes.Length);
                    imageFile.Flush();
                }

            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        [HttpPost("update")]
        public async Task<string> SaveClientes([FromBody] List<Empresa> regs)
        {
            string res = "done", msg = "";
            try
            {
                foreach (var c in regs)
                {
                    msg = c.Nombre;
                    msg = await db.UpdateEmpresa(c);
                }
            }
            catch (Exception ex)
            {
                res = $"Error cargango el cliente {msg}: {ex.Message}";
            }
            return res;
        }

    }
}
