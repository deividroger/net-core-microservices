using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Actio.Api.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;


namespace Actio.Api.Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly IMongoDatabase _mongoDatabase;

        public ActivityRepository(IMongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
        }

        public async Task AddAsync(Activity model)
        => await Collection.InsertOneAsync(model);

        public async Task<IEnumerable<Activity>> BrowseAsync(Guid userId)
        => await Collection
            .AsQueryable()
            .Where(x => x.UserId == userId)
            .ToListAsync();

        public async Task<Activity> GetAsync(Guid id)
        => await Collection
            .AsQueryable()
            .SingleOrDefaultAsync(x => x.Id == id);

        private IMongoCollection<Activity> Collection
            => _mongoDatabase.GetCollection<Activity>("Activities");
    }
}
