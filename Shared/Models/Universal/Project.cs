using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Shared.Models.Universal
{
    public struct ProjectHistory
    {
        [JsonPropertyName("created")]
        public DateTime Created { get; set; }

        [JsonPropertyName("modified")]
        public DateTime Modified { get; set; }

        [JsonPropertyName("shared")]
        public DateTime Shared { get; set; }
    }

    public struct ProjectStats
    {
        [JsonPropertyName("views")]
        public int Views { get; set; }

        [JsonPropertyName("loves")]
        public int Loves { get; set; }

        [JsonPropertyName("favourites")]
        public int Favourites { get; set; }

        [JsonPropertyName("remixes")]
        public int Remixes { get; set; }
    }

    public class Project
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("instructions")]
        public string Instructions { get; set; }

        [JsonPropertyName("visibility")]
        public string Visibility { get; set; }

        [JsonPropertyName("public")]
        public bool Public { get; set; }

        [JsonPropertyName("comments_allowed")]
        public bool CommentsAllowed { get; set; }

        [JsonPropertyName("is_published")]
        public bool IsPublished { get; set; }

        [JsonPropertyName("author")]
        public Author Author { get; set; }

        [JsonPropertyName("image")]
        public string Image { get; set; }

        [JsonPropertyName("history")]
        public ProjectHistory History { get; set; }

        [JsonPropertyName("stats")]
        public ProjectStats Stats { get; set; }
    }
}
