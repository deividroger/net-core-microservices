using Actio.Services.Activities.Domain.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Actio.Services.Activities.Repositories
{
    public class ActivitiesRepository : IActivityRepository
    {
        private readonly IMongoDatabase _database;

        public ActivitiesRepository(IMongoDatabase mongoDatabase) => _database = mongoDatabase;
        public async Task AddAsync(Activity activity) => await Collection.InsertOneAsync(activity);

        public async Task<Activity> GetAsync(Guid id) => await Collection.AsQueryable().SingleOrDefaultAsync(x => x.Id == id);

        private IMongoCollection<Activity> Collection => _database.GetCollection<Activity>("Activities");
    }
}
