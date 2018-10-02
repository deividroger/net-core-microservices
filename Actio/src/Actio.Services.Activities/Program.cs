using Actio.Common.Commands;
using Actio.Common.Services;

namespace Action.Services.Activities
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ServiceHost.Create<Startup>(args)
                .UseRabbitMq()
                .SubscriberToCommand<CreateActivity>()
                .Build()
                .Run();
        }
    }
}
