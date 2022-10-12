using MassTransit;
using MassTransitLibrary.Contracts.MyMessage;
using Microsoft.Extensions.Logging;

namespace MassTransitLibrary.Consumers.MyMessageConsumer
{
    public class MyMessageFaultConsumer :
        IConsumer<Fault<MyMessage>>
    {
        readonly ILogger<MyMessageConsumer> _logger;

        public MyMessageFaultConsumer(ILogger<MyMessageConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<Fault<MyMessage>> context)
        {
            _logger.LogInformation($"Received fault message: {context.Message.Message.Value}");
            return Task.CompletedTask;
        }
    }
}