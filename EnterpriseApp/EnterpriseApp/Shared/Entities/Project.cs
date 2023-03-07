using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseApp.Shared.Entities
{
    using MongoDB.Bson.Serialization.Attributes;
    using System.ComponentModel.DataAnnotations;
    public class Project
    {
        [BsonId]
        public string Id { get; set; } = "0";

        public string NoReq
        {
            get { return "RQ-" + Id.PadLeft(4, '0'); }
        }

        [StringLength(3)]
        public string Status { get; set; } = "SOL";

        [Required(ErrorMessage = "{0} valor requerido")]
        [StringLength(255, ErrorMessage = "{0} debe tener al menos {2} un maximo de {1} caracteres. ", MinimumLength = 4)]
        [Display(Name = "Nombre projecto", AutoGenerateFilter = true)]
        public string Nombre { get; set; } = "";


        [Display(Name = "Alias", AutoGenerateFilter = true)]
        public string Alias { get; set; } = "";

        [Required(ErrorMessage = "{0} valor requerido")]

        [Display(Name = "Solicitante", AutoGenerateFilter = true)]
        public string Solicitante { get; set; } = "";


        [Display(Name = "Fecha", AutoGenerateFilter = true)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime ProyectDate { get; set; }


        [Required(ErrorMessage = "{0} valor requerido")]
        public string Empresa { get; set; } = "GSID";

        [Required(ErrorMessage = "{0} valor requerido")]
        public string CentroCosto { get; set; } = "";
        

        [Required(ErrorMessage = "{0} valor requerido")]
        [Display(Name = "Grupo")]
        public string Grupo { get; set; } = string.Empty;


        [Required(ErrorMessage = "{0} valor requerido")]
        [Display(Name = "Categoria")]
        public string Categoria { get; set; } = "";

        public string Prioridad { get; set; } = "";

        [Required(ErrorMessage = "{0} valor requerido")]
        public string Descripcion { get; set; } = "";









        [Display(Name = "Fecha estimada de engrega", AutoGenerateFilter = true)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime DeliveryDate { get; set; }


        public string Detail { get; set; } = "";

       
        public DateTime CreatedDate { get; set; }
        

        public string Url
        {
            get
            {
                string url = "../Consult?id=" + Id;
                if (Status == "SOL")
                {
                    url = "../Newrq?id=" + Id;
                }
                else if (Status == "DES")
                {
                    url = "../Newrq?id=" + Id;
                }
                else if (Status == "APR")
                {
                    url = "../Approve?id=" + Id;
                }
                else if (Status == "ANL")
                {
                    url = "../Anl?id=" + Id;
                }
                else if (Status == "DEV")
                {
                    url = "../Dev?id=" + Id;
                }
                else if (Status == "QA")
                {
                    url = "../QA?id=" + Id;
                }
                return url;
            }
        }

        public string StatusName
        {
            get
            {
                string url = "Solicitud";
                if (Status == "SOL")
                {
                    url = "Solicitud";
                }
                else if (Status == "APR")
                {
                    url = "Aprobacion";
                }
                else if (Status == "DES")
                {
                    url = "Descartado";
                }
                else if (Status == "ANL")
                {
                    url = "Analisis";
                }
                else if (Status == "DEV")
                {
                    url = "Desarrollo";
                }
                else if (Status == "QA")
                {
                    url = "QA";
                }
                else if (Status == "PRO")
                {
                    url = "PRODUCCION";
                }
                return url;
            }
        }

    }
}
