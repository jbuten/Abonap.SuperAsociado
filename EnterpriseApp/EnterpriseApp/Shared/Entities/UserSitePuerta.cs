using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseApp.Shared.Entities
{
    using Abonap.Users;
    using MongoDB.Bson.Serialization.Attributes;
    using System.ComponentModel.DataAnnotations;

    public class UserSitePuerta
    {
        [BsonId]
        public string Id { get; set; } = DateTime.Now.Ticks.ToString();

        public User User { get; set; } = new();
        public Site Site { get; set; } = new();
        public SitePuerta SitePuerta { get; set; } = new ();
        public string AsignadoPor { set; get; } = String.Empty;
        public DateTime Creado { get; set; } = DateTime.Now;



    }
}
