using MoonlightShadow.Models;
using MongoDB.Driver;
using System.Linq;
using System.Collections.Generic;
using MoonlightShadow.Models.Products;

namespace MoonlightShadow.Services
{
    public class TokenService
    {
        private readonly IMongoCollection<Token> _Token;

        public TokenService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _Token = database.GetCollection<Token>(settings.TokenCollectionName);
        }

        public List<Token> Get()
        {
            return _Token.Find(Token => true).ToList();
        }

        public Token GetBy(string id)
        {
            return _Token.Find(Token => Token.Id == id).FirstOrDefault();
        }

        public Token GetByValue(string value)
        {
            return _Token.Find(Token => Token.value == value).FirstOrDefault();
        }

        public Token Create(Token token)
        {
            _Token.InsertOne(token);
            return token;
        }

        public void Update(string id, Token TokenIn) =>
            _Token.ReplaceOne(Token => Token.Id == id, TokenIn);

        public void Remove(Token TokenIn) =>
            _Token.DeleteOne(Token => Token.Id == TokenIn.Id);

        public void Remove(string id) =>
            _Token.DeleteOne(Token => Token.Id == id);
    }
}