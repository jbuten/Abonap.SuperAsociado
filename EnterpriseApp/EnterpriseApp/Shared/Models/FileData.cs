using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseApp.Shared.Models
{
    public class FileData
    {
        public byte[]? Data { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
    }
}
