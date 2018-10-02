using Actio.Common.Commands;
using Actio.Common.Services;

namespace Action.Services.Identity
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ServiceHost.Create<Startup>(args)
                .UseRabbitMq()
                .SubscriberToCommand<CreateUser>()
                .Build()
                .Run();
        }

        
    }
}
