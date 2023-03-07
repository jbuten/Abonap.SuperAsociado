using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseApp.Shared.Contexts
{
    using EnterpriseApp.Shared.Entities;
    using MongoDB.Driver;

    public partial interface IDbService
    {
        public Task<List<Empresa>> GetEmpresas();

        string SaveEmpresa(Empresa req);

        Task<string> UpdateEmpresa(Empresa req);
        Task<List<Models.ListItem>> ListEmpresas();
        Task<List<Models.ListItem>> ListCentros(string idEmpresa);

    }

    public partial class DbService
    {
        public async Task<List<Empresa>> GetEmpresas()
        {
            return await _Empresa.Find(FilterDefinition<Empresa>.Empty).ToListAsync();
        }


        public async Task<List<Models.ListItem>> ListEmpresas()
        {
            var data = await _Empresa.Find(FilterDefinition<Empresa>.Empty)
               .Sort("{Nombre: 1}")
               .Project(x => new
               Models.ListItem
               {
                   Id = x.Codigo,
                   Value = x.Nombre,
                   Type = "EMPRESA",
               }).ToListAsync();

            return data;
        }

        public async Task<List<Models.ListItem>> ListCentros(string idEmpresa)
        {
            var filter = Builders<Empresa>.Filter.Eq(a => a.Codigo, idEmpresa);

            var record = await _Empresa.FindAsync(filter).Result.FirstOrDefaultAsync();

            return record.Centros.Select(a => new Models.ListItem { Id = a.IdCentro, Value = a.IdCentro + " - " + a.NombreCentro }).OrderBy(a => a.Id).ToList();
        }

        public string SaveEmpresa(Empresa req)
        {
            var filter = Builders<Empresa>.Filter.Eq(a => a.Id, req.Id);
            _Empresa.ReplaceOne(filter, req, new ReplaceOptions { IsUpsert = true });
            return "ok";
        }

        public async Task<string> UpdateEmpresa(Empresa req)
        {
            var filter = Builders<Empresa>.Filter.Eq(a => a.Codigo, req.Codigo);

            var record = await _Empresa.FindAsync(filter).Result.FirstOrDefaultAsync();

            record.Nombre = req.Nombre; record.RNC = req.RNC; record.Direccion = req.Direccion; record.Telefonos = req.Telefonos;

            await _Empresa.ReplaceOneAsync(filter, record, new ReplaceOptions { IsUpsert = true });

            return "ok";
        }




    }
}
