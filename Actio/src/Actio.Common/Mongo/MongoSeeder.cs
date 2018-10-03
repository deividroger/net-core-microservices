using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Actio.Common.Mongo
{
    public class MongoSeeder : IDatabaseSeeder
    {
        protected readonly IMongoDatabase Datatabase;

        

        public MongoSeeder(IMongoDatabase mongoDatabase)
        {
            Datatabase = mongoDatabase;
        }
        public async Task SeedAsync()
        {
            var collectionCursor = await Datatabase.ListCollectionsAsync();

            var collection = await collectionCursor.ToListAsync();

            if (collectionCursor.Any())
            {
                return;
            }

            await CustomSeedAsync();

        }

        protected virtual async Task CustomSeedAsync()
        {
            await Task.CompletedTask;   
        }
    }
}
