using MassTransit;
using MassTransitLibrary.Consumers.MyTaskConsumer;
using MassTransitLibrary.Contracts.MyTask;

namespace MassTransitLibrary.Consumers.MyMessageConsumer
{
    public class MyTaskConsumerDefinition :
        ConsumerDefinition<MyTaskExportConsumer>
    {
        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<MyTaskExportConsumer> consumerConfigurator)
        {
            endpointConfigurator.UseMessageRetry(r => r.Interval(5, 1000));
            endpointConfigurator.DiscardFaultedMessages();
        }
    }
}