namespace Shared.Models.Universal
{
    public record Reaction
    {
        public int CommentId { get; set; }

        public string Username { get; set; }

        public string Emoji { get; set; } // has to be string, not char

        public Reaction(int commentId, string username, string emoji)
        {
            CommentId = commentId;
            Username = username;
            Emoji = emoji;
        }

        public static implicit operator MongoDB.Reaction(Reaction input) => new MongoDB.Reaction(input.CommentId, input.Username, input.Emoji);
    }
}
