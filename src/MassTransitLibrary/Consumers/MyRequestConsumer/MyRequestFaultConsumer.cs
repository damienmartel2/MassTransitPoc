using MassTransit;
using MassTransitLibrary.Contracts;
using Microsoft.Extensions.Logging;

namespace MassTransitLibrary.Consumers.MyMessageConsumer
{
    public class MyRequestFaultConsumer :
        IConsumer<Fault<MyMessage>>
    {
        readonly ILogger<MyRequestConsumer> _logger;

        public MyRequestFaultConsumer(ILogger<MyRequestConsumer> logger)
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