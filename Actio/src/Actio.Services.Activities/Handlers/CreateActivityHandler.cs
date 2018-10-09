using Actio.Common.Commands;
using Actio.Common.Events;
using Actio.Common.Exceptions;
using Actio.Services.Activities.Services;
using RawRabbit;
using System;
using System.Threading.Tasks;

namespace Actio.Services.Activities.Handlers
{
    public class CreateActivityHandler : ICommandHandler<CreateActivity>
    {
        private readonly IBusClient _busClient;
        private readonly IActivityService _activityService;
        

        public CreateActivityHandler(IBusClient busClient, IActivityService activityService)
        {
            _busClient = busClient;
            _activityService = activityService;
           
        }

        public async Task HandleAsync(CreateActivity command)
        {

            try
            {
                await _activityService.AddAsync(command.Id, command.UserId, command.Category, command.Name, command.Description, command.CreatedAt);

                await _busClient.PublishAsync(new ActivityCreated(command.Id,
                                            command.UserId,
                                            command.Category,
                                            command.Name,
                                            command.Description,
                                            command.CreatedAt));

                return;
            }
            catch (ActioException ex)
            {

                await _busClient.PublishAsync(new CreateUserRejected(command.Id.ToString(), ex.Code, ex.Message));

                
            }

            catch (Exception ex)
            {
                await _busClient.PublishAsync(new CreateUserRejected(command.Id.ToString(), "error", ex.Message));

                
            }


        }
    }
}
