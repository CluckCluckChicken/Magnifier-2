using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Models.Universal
{
    public class Constants
    {
        public static readonly string[] EMOJIS = { "👍", "👎", "😄", "🎉", "😕", "❤️", "🚀", "👀" };

        public const string CONSONANTS = "BCDFGHJKLMNPQRSTVWXYZbcdfghjklmnpqrstvwxyz";

        public static readonly Uri authProject = new Uri("https://api.scratch.mit.edu/users/furrycat-auth/projects/534514916/comments");
    }
}
