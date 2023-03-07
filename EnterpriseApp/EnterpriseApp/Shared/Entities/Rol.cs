using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseApp.Shared.Entities
{
    using MongoDB.Bson.Serialization.Attributes;
    using System.ComponentModel.DataAnnotations;

    public class Rol
    {
        [BsonId]
        public string Id { get; set; } = DateTime.Now.Ticks.ToString();

        [Required(ErrorMessage = "El nombre es un valor requerido")]
        public string Name { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public bool Inactive { get; set; } 
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
