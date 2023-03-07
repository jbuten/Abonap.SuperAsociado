
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseApp.Shared.Models
{
    using MongoDB.Bson.Serialization.Attributes;
    public class ListItem
    {
        [BsonId]
        public string Id { get; set; } = String.Empty;
        public string Type { get; set; } = String.Empty;
        public string Value { get; set; } = String.Empty;
    }
}
