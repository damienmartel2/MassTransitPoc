using MassTransit;
using MassTransitLibrary.Contracts;
using Microsoft.Extensions.Logging;

namespace MassTransitLibrary.Consumers.MyMessageConsumer
{
    public class MyRequestConsumer :
        IConsumer<MyRequest>
    {
        readonly ILogger<MyRequestConsumer> _logger;

        public MyRequestConsumer(ILogger<MyRequestConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<MyRequest> context)
        {
            _logger.LogInformation($"Received request: {context.Message.Value}");

            await context.RespondAsync<MyRequestReceived>(new MyRequestReceived()
            {
                Value = context.Message.Value,
                ReceivedAt = InVar.Timestamp
            });

            //await context.RespondAsync<MyRequestRejected>(new MyRequestRejected()
            //{
            //    Value = context.Message.Value,
            //    ReceivedAt = InVar.Timestamp,
            //    Reason = "Ton message est bidon !"
            //});
        }
    }
}