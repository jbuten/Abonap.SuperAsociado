using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abonap.Users
{

    public class MailSettings : IMailSettings
    {
        public string User { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string From { get; set; } = string.Empty;
        public string UrlMailRow { get; set; } = string.Empty;

    }
}
