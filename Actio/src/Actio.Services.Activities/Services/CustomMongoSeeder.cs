using Actio.Common.Mongo;
using Actio.Services.Activities.Domain.Models;
using Actio.Services.Activities.Repositories;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq;

namespace Actio.Services.Activities.Services
{
    public class CustomMongoSeeder : MongoSeeder
    {
        private readonly IMongoDatabase _mongoDatabase;
        private readonly ICategoryRepository _categoryRepository;

        public CustomMongoSeeder(IMongoDatabase mongoDatabase, ICategoryRepository categoryRepository) : base(mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
            _categoryRepository = categoryRepository;
        }
        protected override async Task CustomSeedAsync()
        {
            var categories = new List<string>() {
               "work",
               "sport",
               "hobby"
            };

            await Task.WhenAll(categories.Select(x =>
                        _categoryRepository.AddAsync(new Category(x))));

        }
    }
}
