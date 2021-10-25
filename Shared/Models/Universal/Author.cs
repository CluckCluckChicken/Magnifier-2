using System.Text.Json.Serialization;

namespace Shared.Models.Universal
{
    public class ProfilePictures
    {
        [JsonPropertyName("90x90")]
        public string NinetyByNinety { get; set; }

        [JsonPropertyName("big")]
        public string Big => NinetyByNinety;
    }

    public class Profile
    {
        [JsonPropertyName("images")]
        public ProfilePictures Images { get; set; }
    }

    public class Author
    {
        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("image")]
        public string ProfilePicture { get; set; }

        [JsonPropertyName("profile")]
        public Profile Profile { get; set; }

        public Author(string username, string profilePicture)
        {
            Username = username;
            ProfilePicture = profilePicture;
        }
    }
}
