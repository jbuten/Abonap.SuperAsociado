using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseApp.Shared.Entities
{
using MongoDB.Bson.Serialization.Attributes;
    using System.ComponentModel.DataAnnotations;

    public class App
    {
        [BsonId]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required(ErrorMessage = "El nombre es un valor requerido")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre corto es un valor requerido")]
        public string ShortName { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string IconUrl { get; set; } = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAJYAAACWBAMAAADOL2zRAAAAG1BMVEVsdX3////Hy86jqK1+ho2Ql521ur7a3N7s7e5YhiPTAAAA80lEQVR4Xu3SoU/DQBiG8afXWzt5XQsaEOieQ64CXwJiskuWZXILSXXDSODPhn4Gy1W/P/mIN5fLh4iIiIiIiIiIiIiQUVTVwOn6l4Kl4rsl0T1uPPb57X6LARcsxWNDmlhRtlAObqBofA15FSyNPJLmOVK+QbbNJ/hwZ/CvwVLDE4k6ssuGbruaIMYe8GFOvmG3YOuwGzp8gHKDbc3pEOjStyCf7F0UNbY1p89F74JVsP9i/YVtWVr0X5HiXA7uDk4vrW1ZGnlI31q/79vf++rxtZtsy1I83qRvrS5X7O7d5GvbsvSPuxcRERERERERERH5AVmlJVGG0qyRAAAAAElFTkSuQmCC";
        public string Descripcion { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public bool Inactive { get; set; }
        public List<Rol> Rols { get; set; } = new List<Rol>();
        public List<Menu> Menus { get; set; } = new List<Menu>();


        public string ShortDescription
        {
            get {

                if (Descripcion.Length > 150) { return Descripcion.Substring(0,150) + " ..."; }
                else { return Descripcion; }
            }
        }


    }
}
