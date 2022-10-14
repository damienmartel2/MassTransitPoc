using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using MassTransitLibrary.Contracts.MyOrder;
using Microsoft.Extensions.Logging;

namespace MassTransitLibrary.StateMachines
{
    public class OrderStateMachine : MassTransitStateMachine<OrderState>
    {
        private readonly ILogger<OrderStateMachine> _logger;

        public OrderStateMachine(ILogger<OrderStateMachine> logger)
        {
            this._logger = logger;
            InstanceState(x => x.CurrentState);
            this.ConfigureCorrelationIds();

            Initially(
                When(SubmitOrder)
                    .Then(context =>
                    {
                        context.Saga.Order = context.Message.Order;
                        context.Saga.Order.SubmittedDate = DateTime.Now;
                    })
                    .Then(context => _logger.LogInformation($"Order {context.Saga.Order.Id} submitted at {context.Saga.Order.SubmittedDate}"))
                    .TransitionTo(Submitted)
                    .PublishAsync(context => context.Init<MyOrderAccepted>(new { CorrelationId = context.Saga.CorrelationId, Order = context.Saga.Order }))
            );

            During(Submitted,
                When(AcceptOrder)
                    .Then(context =>
                    {
                        context.Saga.Order.AcceptedDate = DateTime.Now;
                    })
                    .Then(context => _logger.LogInformation($"Order {context.Saga.Order.Id} accepted at {context.Saga.Order.AcceptedDate}"))
                    .Then(context => context.Message.Order.AcceptedDate = DateTime.Now)
                    .TransitionTo(Accepted)
            );

        }

        private void ConfigureCorrelationIds()
        {
            Event(() => SubmitOrder, x => x.CorrelateById(c => c.Message.CorrelationId));
            Event(() => AcceptOrder, x => x.CorrelateById(c => c.Message.CorrelationId));
        }

        public State Submitted { get; private set; }
        public State Accepted { get; private set; }

        public Event<MyOrderSubmitted> SubmitOrder { get; private set; }

        public Event<MyOrderAccepted> AcceptOrder { get; private set; }

        private void UpdateSagaState(OrderState state, MyOrder order)
        {
            state.Order = order;
        }
    }
}
