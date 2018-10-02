using Actio.Common.Events;
using Actio.Common.Services;

namespace Action.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ServiceHost.Create<Startup>(args)
                .UseRabbitMq()
                .SubscriberToEvent<ActivityCreated>()
                .Build()
                .Run();
        }

        
    }
}
