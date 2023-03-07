using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseApp.Shared.Entities
{
    public class MotivoPedido
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "{0} valor requerido")]
        [StringLength(50, ErrorMessage = "{0} debe tener al menos {2} un maximo de {1} caracteres. ", MinimumLength = 4)]
        public string MOTIVO{ get; set; } = string.Empty;

    }

    public class FechaMotivoPedido
    {
        public int ID { get; set; }
        public DateTime Fecha_Registro { get; set; }
        public DateTime Fecha_Motivo { get; set; }
        public int ID_MOTIVO { get; set; }
        public string Motivo { get; set; } = string.Empty;
        public string Usuario { get; set; } = string.Empty;
        public string Empresa { get; set; } = "INDV";

    }

}
