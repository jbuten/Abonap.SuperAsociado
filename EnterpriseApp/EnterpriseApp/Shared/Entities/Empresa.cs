using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseApp.Shared.Entities
{
    using System.ComponentModel.DataAnnotations;
    using MongoDB.Bson.Serialization.Attributes;

    public class Empresa
    {
        [BsonId]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required(ErrorMessage = "El codigo es un valor requerido")]
        public string Codigo { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre es un valor requerido")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El RNC es un valor requerido")]
        public string RNC { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Telefonos { get; set; } = string.Empty;
        public string Logo { get; set; } = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAJYAAACWBAMAAADOL2zRAAAAG1BMVEVsdX3////Hy86jqK1+ho2Ql521ur7a3N7s7e5YhiPTAAAA80lEQVR4Xu3SoU/DQBiG8afXWzt5XQsaEOieQ64CXwJiskuWZXILSXXDSODPhn4Gy1W/P/mIN5fLh4iIiIiIiIiIiIiQUVTVwOn6l4Kl4rsl0T1uPPb57X6LARcsxWNDmlhRtlAObqBofA15FSyNPJLmOVK+QbbNJ/hwZ/CvwVLDE4k6ssuGbruaIMYe8GFOvmG3YOuwGzp8gHKDbc3pEOjStyCf7F0UNbY1p89F74JVsP9i/YVtWVr0X5HiXA7uDk4vrW1ZGnlI31q/79vf++rxtZtsy1I83qRvrS5X7O7d5GvbsvSPuxcRERERERERERH5AVmlJVGG0qyRAAAAAElFTkSuQmCC";
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public bool Inactive { get; set; }
        public List<Centro> Centros = new List<Centro>();
    }

    public partial class Centro
    {
        public string IdCentro { get; set; } = string.Empty;
        public string NombreCentro { get; set; } = string.Empty;
    }
}
