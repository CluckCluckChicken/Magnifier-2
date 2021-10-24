using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Shared.Models.MongoDB
{
    public record Reaction
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string _Id { get; set; }

        public int CommentId { get; set; }

        public string Username { get; set; }

        public string Emoji { get; set; } // has to be string, not char

        public Reaction(int commentId, string username, string emoji)
        {
            CommentId = commentId;
            Username = username;
            Emoji = emoji;
        }

        public static implicit operator Universal.Reaction(Reaction input)
        {
            if (input == null)
            {
                return null;
            }

            return new Universal.Reaction(input.CommentId, input.Username, input.Emoji);
        }
    }
}
