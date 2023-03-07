using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseApp.Shared.Models
{
    public class UserItem
    {
        public string Photo { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string Nombres { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string EmployeeCode { get; set; } = string.Empty;
        public string Titulo { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string DepartmentID { get; set; } = string.Empty;
        public string Company { get; set; } = string.Empty;
        public string CompanyID { get; set; } = string.Empty;
        public string LocationID { get; set; } = string.Empty;
        public string UserDomain { get; set; } = string.Empty;
        public bool Enabled { get; set; }

        public string DataString
        {
            get { return (UserName + DisplayName + EmployeeCode + Department + UserDomain + Company).ToLower(); }
        }
    }
}
