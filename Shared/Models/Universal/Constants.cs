using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Shared.Models.Universal
{
    public class Constants
    {
        public static readonly string[] EMOJIS = { "👍", "👎", "😄", "🎉", "😕", "❤️", "🚀", "👀" };

        public const string CONSONANTS = "BCDFGHJKLMNPQRSTVWXYZbcdfghjklmnpqrstvwxyz";

        public static readonly Uri authProject = new Uri("https://api.scratch.mit.edu/users/furrycat-auth/projects/534514916/comments");

        public static readonly Regex URL_REGEX = new Regex("/(\b(https?):\\/\\/[-A-Z0-9+&@#/%?=~_|!:,.;]*scratch\\.mit\\.edu[-A-Z0-9+&@#/%=~_|]*)/ig", RegexOptions.IgnoreCase);
        public static readonly Regex MENTION_REGEX = new Regex("(?<=(^|[\s.,!])(?!\S*[:#]))[@|＠][A-Za-z0-9_-]{1,20}(?=(?:\b(?!@|＠)|$))", RegexOptions.IgnoreCase);
    }
}
