using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Migrator.Legacy
{
    public class ScratchCommentAuthor
    {
        [JsonConstructor]
        public ScratchCommentAuthor() { }

        public ScratchCommentAuthor(string _username, string _image)
        {
            username = _username;
            image = _image;
        }

        public string _id { get; set; }

        public string username { get; set; }

        public string image { get; set; }
    }
}
