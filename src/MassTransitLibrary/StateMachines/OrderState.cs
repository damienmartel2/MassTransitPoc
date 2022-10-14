using MassTransit;
using MassTransitLibrary.Contracts.MyOrder;

namespace MassTransitLibrary.StateMachines
{
    public class OrderState : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }

        public string CurrentState { get; set; }

        public MyOrder Order { get; set; }
    }
}
