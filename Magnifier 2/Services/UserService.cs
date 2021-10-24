using Magnifier_2.Models;
using MongoDB.Driver;
using Shared.Models.Universal;
using System.Linq;

namespace Magnifier_2.Services
{
    public class UserService
    {
        private readonly IMongoCollection<Shared.Models.MongoDB.User> users;

        public UserService(Magnifier2Settings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase("magnifier");

            users = database.GetCollection<Shared.Models.MongoDB.User>("users");
        }

        public User Get(string username) =>
            users.Find(user => user.Username == username).FirstOrDefault();

        public User Create(User user)
        {
            users.InsertOne(user);
            return user;
        }

        public void Update(string username, User userIn) =>
            users.ReplaceOne(user => user.Username == username, userIn);

        public void Remove(User userIn) =>
            users.DeleteOne(user => user.Username == userIn.Username);

        public void Remove(string username) =>
            users.DeleteOne(user => user.Username == username);
    }
}
