using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Migrator.Legacy
{
    public class ScratchComment
    {
        [JsonConstructor]
        public ScratchComment() { }

        public ScratchComment(int _id, string _content, ScratchCommentAuthor _author, DateTime? _datetime_created)
        {
            id = _id;
            content = _content;
            datetime_created = _datetime_created;
            author = _author;
        }

        public int id { get; set; }

        public string content { get; set; }

        public DateTime? datetime_created { get; set; }

        public ScratchCommentAuthor author { get; set; }
    }
}
