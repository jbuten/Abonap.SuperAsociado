using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseApp.Shared.Entities
{
    using MongoDB.Bson.Serialization.Attributes;
    using System.ComponentModel.DataAnnotations;
    public class MenuOpton
    {
        [BsonId]
        public string Id { get; set; } = DateTime.Now.Ticks.ToString();
        [Required(ErrorMessage = "El nombre es un valor requerido")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "El nombre es un valor requerido")]
        public string Url { get; set; } = string.Empty;
        [Required(ErrorMessage = "El nombre es un valor requerido")]
        public string Icon { get; set; } = "fas fa-caret-right nav-icon";
        public int Index { get; set; } = 1;
        public string Descripcion { get; set; } = string.Empty;
        public bool Inactive { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public List<string> Permissions { get; set; } = new List<string>();

        private bool _IsChecked;

        public bool IsChecked
        {
            get { return _IsChecked; }
            set { _IsChecked = value; }
        }

        public void Setchecked(string rol)
        {
            _IsChecked = Permissions.Any(a => a == rol);
        }
    }
}
