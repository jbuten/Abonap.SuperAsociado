using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseApp.Shared.Models
{
    using System.ComponentModel.DataAnnotations;
    public class LoginRequest
    {
        [Required(ErrorMessage = "{0}, valor requerido")]
        [StringLength(140, ErrorMessage = "{0}, debe tener al menos {2} un maximo de {1} caracteres. ", MinimumLength = 4)]
        [Display(Name = "Aplicacion")]
        public string App { get; set; } = "";

        [Required(ErrorMessage = "{0}, valor requerido")]
        [StringLength(50, ErrorMessage = "{0}, debe tener al menos {2} un maximo de {1} caracteres. ", MinimumLength = 4)]
        [Display(Name = "Usuario")]
        public string UserName { get; set; } = "";

        [Required(ErrorMessage = "{0}, valor requerido")]
        [StringLength(50, ErrorMessage = "{0}, debe tener al menos {2} un maximo de {1} caracteres. ", MinimumLength = 4)]
        [Display(Name = "Password")]
        public string Password { get; set; } = "";
    }
}
