using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Actio.Common.Mongo
{
    public class MongoInitializer : IDatabaseInitializer
    {
        private bool _initialized;

        private readonly bool _seed;

        private readonly IMongoDatabase _mongoDatabase;
        private readonly IDatabaseSeeder _databaseSeeder;

        public MongoInitializer(IMongoDatabase mongoDatabase,IDatabaseSeeder databaseSeeder, IOptions<MongoOptions> options)
        {
            _mongoDatabase = mongoDatabase;
            _databaseSeeder = databaseSeeder;
            _seed = options.Value.Seed;
        }

        public async Task InitializeAsync()
        {
            if (_initialized) return;


            RegisterConventions();

            _initialized = true;

            if (!_seed) return;

            await _databaseSeeder.SeedAsync();
        }

        private void RegisterConventions()
        {
            ConventionRegistry.Register("ActionConventions", new MongoConvention(),x=> true);
        }

        private class MongoConvention : IConventionPack
        {
            public IEnumerable<IConvention> Conventions => new List<IConvention>
            {
                new IgnoreExtraElementsConvention(true),
                new EnumRepresentationConvention(MongoDB.Bson.BsonType.String),
                new CamelCaseElementNameConvention()
            };
        }
    }
}
