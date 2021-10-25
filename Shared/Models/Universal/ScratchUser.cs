using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Shared.Models.Universal
{
    public struct UserHistory
    {
        [JsonPropertyName("joined")]
        public DateTime Joined { get; set; }
    }

    public struct UserProfile
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("bio")]
        public string Bio { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("images")]
        public ProfilePictures Images { get; set; }
    }

    public class ScratchUser
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("scratchteam")]
        public bool IsScratchTeam { get; set; }

        [JsonPropertyName("history")]
        public UserHistory History { get; set; }

        [JsonPropertyName("profile")]
        public UserProfile Profile { get; set; }
    }
}
