using System.Reflection;
using System.Threading.Tasks;
using MassTransit;
using MassTransitLibrary.Consumers;
using MassTransitLibrary.Consumers.MyTaskConsumer;
using MassTransitLibrary.Contracts.MyTask;
using MassTransitLibrary.StateMachines;
using Microsoft.Extensions.Hosting;

namespace MassTransitConsumer
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await CreateHostBuilder(args).Build().RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddMassTransit(x =>
                    {
                        x.SetKebabCaseEndpointNameFormatter();

                        var entryAssembly = Assembly.GetAssembly(typeof(AnchorConsumer));

                        //x.AddConsumers(entryAssembly);

                        x.AddConsumer<MyTaskExportConsumer>(x =>
                            x.ConsumerMessage<MyTask>(p => p.UseFilter(new TaskTypeFilter<MyTaskExportConsumer>())));

                        x.AddConsumer<MyTaskLadRadConsumer>(x =>
                        x.ConsumerMessage<MyTask>(p => p.UseFilter(new TaskTypeFilter<MyTaskLadRadConsumer>())));

                        x.AddSagaStateMachine<OrderStateMachine, OrderState, OrderStateDefinition>()
                            .InMemoryRepository();

                        x.UsingRabbitMq((context, cfg) =>
                        {
                            cfg.ConfigureEndpoints(context);
                        });
                    });
                });
    }
}
