using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Shared.Models.Universal
{
    public class EmojiData
    {
        public string Emoji { get; set; }

        public List<Reaction> Reactions { get; set; }

        [JsonConstructor]
        public EmojiData() { }

        public EmojiData(string emoji)
        {
            Emoji = emoji;

            Reactions = new List<Reaction>();
        }
    }
}
