using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseApp.Shared.Models
{
    public class UserTocken
    {
        public string Token { get; set; } = "";
        public DateTime Expiration { get; set; } = new DateTime();
    }
}
