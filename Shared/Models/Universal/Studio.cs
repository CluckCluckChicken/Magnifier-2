using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Shared.Models.Universal
{
    public struct StudioHistory
    {
        [JsonPropertyName("created")]
        public DateTime Created { get; set; }

        [JsonPropertyName("modified")]
        public DateTime Modified { get; set; }
    }

    public struct StudioStats
    {
        [JsonPropertyName("comments")]
        public int Comments { get; set; }

        [JsonPropertyName("followers")]
        public int Followers { get; set; }

        [JsonPropertyName("managers")]
        public int Managers { get; set; }

        [JsonPropertyName("projects")]
        public int Projects { get; set; }
    }

    public class Studio
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("host")]
        public int Host { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("visibility")]
        public string Visibility { get; set; }

        [JsonPropertyName("public")]
        public bool Public { get; set; }

        [JsonPropertyName("open_to_all")]
        public bool OpenToAll { get; set; }

        [JsonPropertyName("comments_allowed")]
        public bool CommentsAllowed { get; set; }

        [JsonPropertyName("image")]
        public string Image { get; set; }

        [JsonPropertyName("history")]
        public StudioHistory History { get; set; }

        [JsonPropertyName("stats")]
        public StudioStats Stats { get; set; }
    }
}
