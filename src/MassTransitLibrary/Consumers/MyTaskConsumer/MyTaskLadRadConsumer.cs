using MassTransit;
using MassTransitLibrary.Contracts.MyTask;
using Microsoft.Extensions.Logging;

namespace MassTransitLibrary.Consumers.MyTaskConsumer
{
    [TaskType("LadRad")]
    public class MyTaskLadRadConsumer :
        IConsumer<MyTask>
    {
        readonly ILogger<MyTaskLadRadConsumer> _logger;

        public MyTaskLadRadConsumer(ILogger<MyTaskLadRadConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<MyTask> context)
        {
            _logger.LogInformation($"LadRad : Received task: {context.Message.Type}");
            return Task.CompletedTask;
        }
    }
}