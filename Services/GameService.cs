using MoonlightShadow.Models;
using MoonlightShadow.Models.Products;
using MongoDB.Driver;
using System.Linq;
using System.Collections.Generic;

namespace MoonlightShadow.Services
{
    public class GameService
    {
        private readonly IMongoCollection<Game> _Game;

        public GameService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _Game = database.GetCollection<Game>(settings.GameCollectionName);
        }

        public List<Game> Get()
        {
            return _Game.Find(Game => true).ToList();
        }

        public Game GetBy(string id)
        {
            return _Game.Find(Game => Game.Id == id).FirstOrDefault();
        }

        public Game Create(Game Game)
        {
            _Game.InsertOne(Game);
            return Game;
        }

        public void Update(string id, Game GameIn) =>
            _Game.ReplaceOne(Game => Game.Id == id, GameIn);

        public void Remove(Game GameIn) =>
            _Game.DeleteOne(Game => Game.Id == GameIn.Id);

        public void Remove(string id) => 
            _Game.DeleteOne(Game => Game.Id == id);
    }

}