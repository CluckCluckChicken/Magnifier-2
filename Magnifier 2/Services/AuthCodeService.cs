using Magnifier_2.Models;
using MongoDB.Driver;
using Shared.Models.MongoDB;
using System.Collections.Generic;
using System.Linq;

namespace Magnifier_2.Services
{
    public class AuthCodeService
    {
        private readonly IMongoCollection<AuthCode> authCodes;

        public AuthCodeService(Magnifier2Settings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase("magnifier");

            authCodes = database.GetCollection<AuthCode>("authCodes");
        }

        public List<AuthCode> Get() =>
            authCodes.Find(authCode => true).ToList();

        public AuthCode Get(string code) =>
            authCodes.Find(authCode => authCode.code == code).FirstOrDefault();

        public AuthCode Create(AuthCode authCode)
        {
            authCodes.InsertOne(authCode);
            return authCode;
        }

        public void Update(string code, AuthCode authCodeIn) =>
            authCodes.ReplaceOne(authCode => authCode.code == code, authCodeIn);

        public void Remove(AuthCode authCodeIn) =>
            authCodes.DeleteOne(comment => comment.id == authCodeIn.id);

        public void Remove(string code) =>
            authCodes.DeleteOne(authCode => authCode.code == code);
    }
}