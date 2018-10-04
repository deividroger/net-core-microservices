using Actio.Common.Exceptions;
using Actio.Services.Activities.Domain.Models;
using Actio.Services.Activities.Repositories;
using System;
using System.Threading.Tasks;

namespace Actio.Services.Activities.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepository _activityRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ActivityService(IActivityRepository activityRepository, ICategoryRepository categoryRepository)
        {
            _activityRepository = activityRepository;
            _categoryRepository = categoryRepository;   

        }

        public async Task AddAsync(Guid id, Guid userId, string category, string name, string description, DateTime createAt)
        {
            var activityCategory = await _categoryRepository.GetAsync(category);

            if (activityCategory == null)
            {
                throw new ActioException("category_not_found", category);
            }

            var activity = new Activity(id, activityCategory, userId, name, description, createAt);

            await _activityRepository.AddAsync(activity);

        }
    }
}