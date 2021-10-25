using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Shared.Models.Universal
{
    public class User
    {
        public string Username { get; set; }

        public Author Author { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsBanned { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastLogin { get; set; }

        [JsonConstructor]
        public User() { }

        public User(Author author)
        {
            Username = author.Username;
            Author = author;

            Created = DateTime.Now;
        }

        public static implicit operator MongoDB.User(User input)
        {
            if (input == null)
            {
                return null;
            }

            return new MongoDB.User
            {
                Username = input.Username,
                Author = input.Author,
                IsAdmin = input.IsAdmin,
                IsBanned = input.IsBanned,
                Created = input.Created,
                LastLogin = input.LastLogin,
            };
        }
    }
}
