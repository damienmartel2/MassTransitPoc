using MassTransit;


namespace MassTransitLibrary.Consumers.MyMessageConsumer
{
    public class MyRequestConsumerDefinition :
        ConsumerDefinition<MyRequestConsumer>
    {
        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<MyRequestConsumer> consumerConfigurator)
        {
            endpointConfigurator.UseMessageRetry(r => r.Interval(5, 1000));
            endpointConfigurator.DiscardFaultedMessages();
        }
    }
}