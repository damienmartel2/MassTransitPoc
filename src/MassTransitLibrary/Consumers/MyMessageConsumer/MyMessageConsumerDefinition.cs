using MassTransit;


namespace MassTransitLibrary.Consumers.MyMessageConsumer
{
    public class MyMessageConsumerDefinition :
        ConsumerDefinition<MyMessageConsumer>
    {
        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<MyMessageConsumer> consumerConfigurator)
        {
            endpointConfigurator.UseMessageRetry(r => r.Interval(5, 1000));
            endpointConfigurator.DiscardFaultedMessages();
        }
    }
}