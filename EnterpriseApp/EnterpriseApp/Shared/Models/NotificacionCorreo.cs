using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseApp.Shared.Models
{
    public class NotificacionCorreo
    {
        public string Titulo { get; set; } = string.Empty;
        public string Cuerpo { get; set; } = string.Empty;
        public string SolicitudID { get; set; } = string.Empty;
        public Dictionary<string, string> pairs { get; set; } = new Dictionary<string, string>();
    }
}
