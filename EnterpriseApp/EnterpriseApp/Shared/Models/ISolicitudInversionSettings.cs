using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseApp.Shared.Models
{
    public interface ISolicitudInversionSettings
    {
        public string UrlBase { get; set; }
    }
    public class SolicitudInversionSettings : ISolicitudInversionSettings
    {
        public string UrlBase { get; set; }
    }
}
