using MassTransit;

namespace MassTransitLibrary.StateMachines
{
    public class OrderStateDefinition :
        SagaDefinition<OrderState>
    {
        readonly IServiceProvider _provider;

        public OrderStateDefinition(IServiceProvider provider)
        {
            _provider = provider;
        }

        protected override void ConfigureSaga(IReceiveEndpointConfigurator endpointConfigurator,
            ISagaConfigurator<OrderState> consumerConfigurator)
        {
            endpointConfigurator.UseMessageRetry(r => r.Intervals(10, 50, 100, 1000, 1000, 1000, 1000, 1000));
        }
    }
}
