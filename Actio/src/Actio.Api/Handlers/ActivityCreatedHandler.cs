using Actio.Api.Models;
using Actio.Api.Repositories;
using Actio.Common.Events;
using System;
using System.Threading.Tasks;

namespace Actio.Api.Handlers
{
    public class ActivityCreatedHandler : IEventHandler<ActivityCreated>
    {
        private readonly IActivityRepository _activityRepository;

        public ActivityCreatedHandler(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }

        public async Task HandleAsync(ActivityCreated @event)
        {

            await Task.CompletedTask;

            await _activityRepository.AddAsync(new Activity()
            {
                Category = @event.Category,
                CreateAt = @event.CreatedAt,
                Description = @event.Description,
                Id = @event.Id,
                Name = @event.Name,
                UserId = @event.UserId
            });


            Console.WriteLine($"Activity created: {@event.Name}");
        }
    }
}
