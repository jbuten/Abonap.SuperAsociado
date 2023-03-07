using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abonap.Customers
{
    public class Sequence
    {
        [BsonId]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string Id { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public int Value { get; set; }

        internal static long GetNextSequenceValue(string sequenceName, IMongoDatabase database)
        {
            var collection = database.GetCollection<Sequence>("sequence");
            var filter = Builders<Sequence>.Filter.Eq(a => a.Id, sequenceName);
            var update = Builders<Sequence>.Update.Inc(a => a.Value, 1);
            var sequence = collection.FindOneAndUpdate(filter, update);

            if (sequence is null) { sequence = new Sequence { Id = sequenceName, Value = 1 }; collection.InsertOne(sequence); }

            return sequence.Value;
        }

    }
}
