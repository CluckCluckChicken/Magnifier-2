using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Shared.Models.Universal
{
    public class Comment
    {
        [JsonPropertyName("id")]
        public int CommentId { get; set; }

        [JsonPropertyName("content")]
        public string Content { get; set; }

        [JsonPropertyName("datetime_created")]
        public DateTime Created { get; set; }

        [JsonPropertyName("author")]
        public Author Author { get; set; }

        public List<Comment> Replies { get; set; }

        public Comment(int commentId, string content, DateTime created, Author author)
        {
            CommentId = commentId;
            Content = content;
            Created = created;
            Author = author;
            Replies = new List<Comment>();
        }
    }
}
