using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Models.MongoDB
{
    public record Session
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string _Id { get; set; } // MongoDB document id

        public string SessionId { get; set; } // Token

        public string Username { get; set; } // Username of user

        public bool IsValid { get; set; }

        public DateTime Created;

        public Session(string username)
        {
            SessionId = $"M:S:{Guid.NewGuid()}";
            Username = username;
            IsValid = true;
            Created = DateTime.Now;
        }
    }
}
