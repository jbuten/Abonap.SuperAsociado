using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseApp.Shared.Entities
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    public class Log
    {
        [BsonId]
        //[BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public DateTime Fecha { get; set; } = DateTime.UtcNow;
        public string User { get; set; } = string.Empty;
        public string Metodo { get; set; } = string.Empty;
        public string request { get; set; } = string.Empty;
        public string response { get; set; } = string.Empty;

    }
}
