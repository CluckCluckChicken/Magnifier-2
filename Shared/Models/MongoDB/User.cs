using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Shared.Models.Universal;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Shared.Models.MongoDB
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string _Id { get; set; }

        public string Username { get; set; }

        public Author Author { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsBanned { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastLogin { get; set; }

        public List<int> Stars { get; set; }

        [JsonConstructor]
        public User() { }

        public User(Author author)
        {
            Username = author.Username;
            Author = author;

            Created = DateTime.Now;

            Stars = new List<int>();
        }

        public static implicit operator Universal.User(User input)
        {
            if (input == null)
            {
                return null;
            }

            return new Universal.User
            {
                Username = input.Username,
                Author = input.Author,
                IsAdmin = input.IsAdmin,
                IsBanned = input.IsBanned,
                Created = input.Created,
                LastLogin = input.LastLogin,
                Stars = input.Stars
            };
        }
    }
}
