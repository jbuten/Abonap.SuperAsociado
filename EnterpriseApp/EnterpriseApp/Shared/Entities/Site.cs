using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseApp.Shared.Entities
{
    using MongoDB.Bson.Serialization.Attributes;
    using System.ComponentModel.DataAnnotations;
    public class Site
    {
        [BsonId]
        public string Id { get; set; } = DateTime.Now.Ticks.ToString();

        [Required(ErrorMessage = "El Codigo es un valor requerido")]
        public string Codigo { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre es un valor requerido")]
        public string Nombre { get; set; } = string.Empty;

        public string Latitud { get; set; } = string.Empty;
        public string Longitud { get; set; } = string.Empty;
        public string Empresa { get; set; } = string.Empty;
        public bool Inactive { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public List<SitePuerta> Puertas { get; set; } = new List<SitePuerta>();

    }
    public class SitePuerta
    {
        [BsonId]
        public string Id { get; set; } = DateTime.Now.Ticks.ToString();

        [Required(ErrorMessage = "El codigo es un valor requerido")]
        public string Codigo { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre es un valor requerido")]
        public string Nombre { get; set; } = string.Empty;
        public string Latitud { get; set; } = string.Empty;
        public string Longitud { get; set; } = string.Empty;
        public bool Inactive { get; set; }

    }

}
