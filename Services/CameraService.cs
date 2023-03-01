using MoonlightShadow.Models;
using MongoDB.Driver;
using System.Linq;
using System.Collections.Generic;
using MoonlightShadow.Models.Products;

namespace MoonlightShadow.Services
{
    public class CameraService
    {
        private readonly IMongoCollection<Camera> _Camera;

        public CameraService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _Camera = database.GetCollection<Camera>(settings.CameraCollectionName);
        }

        public List<Camera> Get()
        {
            return _Camera.Find(Camera => true).ToList();
        }

        public Camera GetBy(string id)
        {
            return _Camera.Find(Camera => Camera.Id == id).FirstOrDefault();
        }

        public Camera Create(Camera camera)
        {
            _Camera.InsertOne(camera);
            return camera;
        }

        public void Update(string id, Camera CameraIn) =>
            _Camera.ReplaceOne(Camera => Camera.Id == id, CameraIn);

        public void Remove(Camera CameraIn) =>
            _Camera.DeleteOne(Camera => Camera.Id == CameraIn.Id);

        public void Remove(string id) =>
            _Camera.DeleteOne(Camera => Camera.Id == id);
    }
}