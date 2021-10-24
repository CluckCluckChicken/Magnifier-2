using Magnifier_2.Models;
using MongoDB.Driver;
using Shared.Models.MongoDB;

namespace Magnifier_2.Services
{
    public class SessionService
    {
        private readonly IMongoCollection<Session> sessions;

        public SessionService(Magnifier2Settings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase("magnifier");

            sessions = database.GetCollection<Session>("sessions");
        }

        public Session Get(string sessionId) => sessions.Find(session => session.SessionId == sessionId).FirstOrDefault();

        public Session Create(Session session)
        {
            sessions.InsertOne(session);
            return session;
        }

        public void Update(string sessionId, Session sessionIn) =>
            sessions.ReplaceOne(session => session.SessionId == sessionId, sessionIn);

        public void Invalidate(Session session)
        {
            session.IsValid = false;
            Update(session.SessionId, session);
        }
    }
}
