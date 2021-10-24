using System.Text.Json.Serialization;

namespace Shared.Models.Universal
{
    public class Author
    {
        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("image")]
        public string ProfilePicture { get; set; }

        public Author(string username, string profilePicture)
        {
            Username = username;
            ProfilePicture = profilePicture;
        }
    }
}
