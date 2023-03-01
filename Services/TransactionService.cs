using MoonlightShadow.Models;
using MoonlightShadow.Models.Products;
using MongoDB.Driver;
using System.Linq;
using System.Collections.Generic;
using MoonlightShadow.Models.Transaction;

namespace MoonlightShadow.Services
{
    public class TransactionService
    {
        private readonly IMongoCollection<Transaction> _Transaction;

        public TransactionService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _Transaction = database.GetCollection<Transaction>(settings.TransactionCollectionName);
        }

        public List<Transaction> Get()
        {
            return _Transaction.Find(Transaction => true).ToList();
        }

        public Transaction GetBy(string id)
        {
            return _Transaction.Find(Transaction => Transaction.Id == id).FirstOrDefault();
        }

        public Transaction Create(Transaction Transaction)
        {
            _Transaction.InsertOne(Transaction);
            return Transaction;
        }

        public void Update(Transaction TransactionIn) =>
            _Transaction.ReplaceOne(Transaction => Transaction.Id == TransactionIn.Id, TransactionIn);

        public void Remove(Transaction TransactionIn) =>
            _Transaction.DeleteOne(Transaction => Transaction.Id == TransactionIn.Id);

        public void Remove(string id) => 
            _Transaction.DeleteOne(Transaction => Transaction.Id == id);
    }

}