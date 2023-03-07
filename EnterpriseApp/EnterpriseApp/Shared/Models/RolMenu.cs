using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseApp.Shared.Models
{
    public class RolMenu : MenuOption
    {
        public string Type { get; set; } = String.Empty;
        public List<MenuOption> Options { get; set; } = new List<MenuOption>();
    }

    public class MenuOption
    {
        public int Index { get; set; } = 0;
        public string Id { get; set; } = String.Empty;
        public string Title { get; set; } = String.Empty;
        public string Url { get; set; } = String.Empty;
        public string Icon { get; set; } = String.Empty;
        public bool IsChecked { get; set; } = false;
    }
}
