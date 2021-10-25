using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Migrator
{
    class Program
    {
        private static Dictionary<string, string> emojis = new Dictionary<string, string>()
        {
            { "thumbsup", "👍" },
            { "thumbsdown", "👎" },
            { "laugh", "😄" },
            { "hooray", "🎉" },
            { "confused", "😕" },
            { "heart", "❤️" },
            { "rocket", "🚀" },
            { "eyes", "👀" },
            { "sad", "😔" }
        };

        static void Main(string[] args)
        {
            Console.WriteLine("welcome to magnifier data migration tool. only the higest professtional results will be produced by this tool.");

            Console.Write("comments (c), users (u): ");

            char dataType = Console.ReadKey().KeyChar;

            switch (dataType)
            {
                case 'c':
                    MigrateComments();
                    break;

                case 'u':
                    MigrateUsers();
                    break;
            }
        }

        public static void MigrateComments()
        {

            Console.WriteLine("--- migrate comments ---");

            Console.Write("input comments json path: ");

            List<Legacy.Comment> inputComments = JsonSerializer.Deserialize<List<Legacy.Comment>>(File.ReadAllText(Console.ReadLine()));

            Console.Write("output comments json path: ");

            string outputPath = Console.ReadLine();

            List<Shared.Models.Universal.Reaction> outputReactions = new List<Shared.Models.Universal.Reaction>();

            foreach (Legacy.Comment comment in inputComments)
            {
                foreach (Legacy.UserReaction userReaction in comment.reactions)
                {
                    outputReactions.Add(new Shared.Models.Universal.Reaction(comment.commentId, userReaction.user, emojis[userReaction.reaction]));
                }
            }

            File.WriteAllText(outputPath, JsonSerializer.Serialize(outputReactions));
        }

        public static void MigrateUsers()
        {
            Console.WriteLine("--- migrate users ---");

            Console.Write("input users json path: ");

            List<Legacy.User> inputUsers = JsonSerializer.Deserialize<List<Legacy.User>>(File.ReadAllText(Console.ReadLine()));

            Console.Write("output users json path: ");

            string outputPath = Console.ReadLine();

            List<Shared.Models.Universal.User> outputUsers = new List<Shared.Models.Universal.User>();

            foreach (Legacy.User legacy in inputUsers)
            {
                outputUsers.Add(new Shared.Models.MongoDB.User()
                {
                    Username = legacy.username,
                    Author = new Shared.Models.Universal.Author(legacy.username, legacy.scratchUser.image),
                    IsAdmin = legacy.isAdmin,
                    IsBanned = legacy.isBanned,
                    Created = legacy.created
                    //Stars = new List<int>()
                });
            }

            File.WriteAllText(outputPath, JsonSerializer.Serialize(outputUsers));
        }
    }
}
