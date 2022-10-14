using MassTransit;
using MassTransitLibrary.Contracts.MyTask;
using Microsoft.Extensions.Logging;

namespace MassTransitLibrary.Consumers.MyTaskConsumer
{
    [TaskType("Export")]
    public class MyTaskExportConsumer :
        IConsumer<MyTask>
    {
        readonly ILogger<MyTaskExportConsumer> _logger;

        public MyTaskExportConsumer(ILogger<MyTaskExportConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<MyTask> context)
        {
            _logger.LogInformation($"Export => Received task: {context.Message.Type}");
            return Task.CompletedTask;
        }
    }
}