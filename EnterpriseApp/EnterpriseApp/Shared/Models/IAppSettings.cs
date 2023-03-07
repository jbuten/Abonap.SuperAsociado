using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseApp.Shared.Models
{
    public interface IAppSettings
    {
        string CollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string JWTKey { get; set; }
        string DomainServer { get; set; }
        string DomainUser { get; set; }
        string DomainPass { get; set; }
        string PhotoPath { get; set; }
       
    }

    public class AppSettings : IAppSettings
    {
        public string CollectionName { get; set; } = string.Empty;
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
        public string JWTKey { get; set; } = string.Empty;
        public string DomainServer { get; set; } = string.Empty;
        public string DomainUser { get; set; } = string.Empty;
        public string DomainPass { get; set; } = string.Empty;
        public string PhotoPath { get; set; } = string.Empty;

    }
}
