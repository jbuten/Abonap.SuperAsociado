using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseApp.Shared.Entities
{
    public class VentasExtraordinaria
    {
        public int ID { get; set; }
        public DateTime Fecha_Registro { get; set; }
        public DateTime Fecha_Venta { get; set; } 
        public decimal Valor { get; set; }
        public string Usuario { get; set; } = string.Empty;
        public string Empresa { get; set; } = "INDV";

    }
}
