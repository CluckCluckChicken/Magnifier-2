using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Shared.Models.MongoDB
{
    public record AuthCode
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string id { get; set; }

        public string code { get; set; }

        public bool hasBeenUsed { get; set; }

        public AuthCode(string code, bool hasBeenUsed = false)
        {
            this.code = code;
            this.hasBeenUsed = hasBeenUsed;
        }
    }
}
