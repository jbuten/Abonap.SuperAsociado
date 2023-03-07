using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abonap.Customers
{
    using MongoDB.Bson.Serialization.Attributes;

    public class Query
    {
        [BsonIgnoreIfNull]
        public string? Company { get; set; }

        [BsonIgnoreIfNull]
        public string? CustomerCode { get; set; }

        [BsonIgnoreIfNull]
        public string? Identification { get; set; }


        [BsonIgnoreIfNull]
        public string? CustomerName { get; set; }

        [BsonIgnoreIfNull]
        public string? EstatusID { get; set; }

        public int Top { get; set; } = 1;
    }

}
