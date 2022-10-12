using MassTransit;
using MassTransitLibrary.Contracts;
using Microsoft.Extensions.Logging;

namespace MassTransitLibrary.Consumers.MyMessageConsumer
{
    public class MyMessageConsumer :
        IConsumer<MyMessage>
    {
        readonly ILogger<MyMessageConsumer> _logger;

        public MyMessageConsumer(ILogger<MyMessageConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<MyMessage> context)
        {
            _logger.LogInformation($"Received message: {context.Message.Value}");
            //throw new System.Exception("test");
            return Task.CompletedTask;
        }
    }
}