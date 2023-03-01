using MoonlightShadow.Models;
using MoonlightShadow.Models.Products;
using MongoDB.Driver;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace MoonlightShadow.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _user;
        private readonly IMongoClient _client;

        public UserService(IDatabaseSettings settings)
        {
            _client = new MongoClient(settings.ConnectionString);
            var database = _client.GetDatabase(settings.DatabaseName);

            _user = database.GetCollection<User>(settings.UsersCollectionName);
        }

        public void Create(User userIn)
        {
            _user.InsertOne(userIn);
        }

        public async Task<bool> CreateAsync(User userIn)
        {
            using (var session = _client.StartSession())
            {
                var cancellationTokenSource = new CancellationTokenSource();

                try
                {
                    session.StartTransaction();

                    await _user.InsertOneAsync(session, userIn, null, cancellationTokenSource.Token);

                    return true;
                }
                catch (Exception e)
                {
                    System.Console.WriteLine(e.GetType().FullName);
                    System.Console.WriteLine(e.Message);

                    await session.AbortTransactionAsync(cancellationTokenSource.Token);
                    return false;
                }
            }
        }

        public async Task<IAsyncCursor<User>> GetAsync()
        {
            return await _user.FindAsync(user => true);
        }

        public User GetByLogin(string login)
        {
            return _user.Find(user => user.Login == login).FirstOrDefault();
        }

        public User GetByEmail(string email)
        {
            return _user.Find(user => user.Email == email).FirstOrDefault();
        }

        public void Update(User userIn)
        {
            _user.ReplaceOne(user => user.Login == userIn.Login, userIn);
        }

        public async Task<bool> UpdateAsync(User userIn)
        {
            using (var session = await _client.StartSessionAsync())
            {
                session.StartTransaction();

                var cancellationTokenSource = new CancellationTokenSource();

                try
                {
                    session.StartTransaction();

                    await _user.UpdateOneAsync(
                        session, 
                        MongoDB.Driver.Builders<User>.Filter.Eq(user => user.Login, userIn.Login),
                        MongoDB.Driver.Builders<User>.Update.Set(user => user, userIn),
                        null,
                        cancellationTokenSource.Token
                    );

                    return true;
                }
                catch (Exception e)
                {
                    System.Console.WriteLine(e.GetType().FullName);
                    System.Console.WriteLine(e.Message);

                    await session.AbortTransactionAsync(cancellationTokenSource.Token);
                    return false;
                }
            }
        }

        public async Task<bool> RemoveAsync(User userOut)
        {
            using (var session = await _client.StartSessionAsync())
            {
                session.StartTransaction();

                var cancellationTokenSource = new CancellationTokenSource();

                try
                {
                    session.StartTransaction();

                    await _user.DeleteOneAsync(
                        session,
                        MongoDB.Driver.Builders<User>.Filter.Eq(user => user.Login, userOut.Login),
                        null,
                        cancellationTokenSource.Token
                    );

                    return true;
                }
                catch (Exception e)
                {
                    System.Console.WriteLine(e.GetType().FullName);
                    System.Console.WriteLine(e.Message);

                    await session.AbortTransactionAsync(cancellationTokenSource.Token);
                    return false;
                }
            }
        }
    }
}