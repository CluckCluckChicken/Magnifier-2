using Magnifier_2.Models;
using MongoDB.Driver;
using Shared.Models.Universal;
using System.Collections.Generic;

namespace Magnifier_2.Services
{
    public class ReactionService
    {
        private readonly IMongoCollection<Shared.Models.MongoDB.Reaction> reactions;

        public ReactionService(Magnifier2Settings settings)
        {
            MongoClient client = new MongoClient(settings.ConnectionString);
            IMongoDatabase database = client.GetDatabase("magnifier");

            reactions = database.GetCollection<Shared.Models.MongoDB.Reaction>("reactions");
        }

        public List<EmojiData> Get(int commentId)
        {
            List<EmojiData> emojis = new List<EmojiData>();

            List<Shared.Models.MongoDB.Reaction> unsortedReactions = reactions.Find(reaction => reaction.CommentId == commentId).ToList();

            foreach (string emoji in Constants.EMOJIS)
            {
                emojis.Add(new EmojiData(emoji));
            }

            foreach (Shared.Models.MongoDB.Reaction reaction in unsortedReactions)
            {
                EmojiData emojiData = emojis.Find(emoji => emoji.Emoji == reaction.Emoji);

                if (emojiData == null)
                {
                    emojiData = new EmojiData(reaction.Emoji);

                    emojis.Add(emojiData);
                }

                emojiData.Reactions.Add(reaction);
            }

            return emojis;
        }

        public Reaction Get(int commentId, string username, string emoji) => reactions.Find(reaction => reaction.CommentId == commentId && reaction.Username == username && reaction.Emoji == emoji).FirstOrDefault();

        public Reaction Create(Reaction reaction)
        {
            reactions.InsertOne(reaction);
            return reaction;
        }

        public void Remove(Reaction reactionToRemove) => reactions.DeleteOne(reaction => reaction.CommentId == reactionToRemove.CommentId && reaction.Username == reactionToRemove.Username && reaction.Emoji == reactionToRemove.Emoji);
    }
}
