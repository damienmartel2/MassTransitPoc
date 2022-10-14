using MassTransit;
using MassTransitLibrary.Contracts.MyOrder;
using Microsoft.Extensions.Logging;

namespace MassTransitLibrary.Consumers.MyMessageConsumer
{
    public class MyOrderSubmitConsumer :
        IConsumer<MyOrderSubmitted>
    {
        readonly ILogger<MyOrderSubmitConsumer> _logger;

        public MyOrderSubmitConsumer(ILogger<MyOrderSubmitConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<MyOrderSubmitted> context)
        {
            _logger.LogInformation($"Received order: {context.Message.Order.Id}");
            return Task.CompletedTask;
        }
    }
    public class MyOrderAcceptedConsumer :
        IConsumer<MyOrderAccepted>
    {
        readonly ILogger<MyOrderAcceptedConsumer> _logger;

        public MyOrderAcceptedConsumer(ILogger<MyOrderAcceptedConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<MyOrderAccepted> context)
        {
            _logger.LogInformation($"Accepted order: {context.Message.Order.Id}");
            return Task.CompletedTask;
        }
    }

}