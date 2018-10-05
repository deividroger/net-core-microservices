using Actio.Services.Identity.Domain.Models;
using Actio.Services.Identity.Domain.Repositories;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Linq;
using System.Threading.Tasks;
namespace Actio.Services.Identity.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoDatabase _mongoDatabase;

        public UserRepository(IMongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
        }

        public async Task AddAsync(User user)
        =>    await Collection.InsertOneAsync(user);
        

        public async Task<User> GetAsync(Guid id)
        =>
            await Collection
                .AsQueryable()
                .SingleOrDefaultAsync(x => x.Id == id);

        public async Task<User> GetAsync(string email)
        =>
            await Collection
                .AsQueryable()
                .SingleOrDefaultAsync(x => x.Email == email.ToLowerInvariant());

        private IMongoCollection<User> Collection
            => _mongoDatabase.GetCollection<User>("Users");

    }
}
