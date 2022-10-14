using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MassTransitLibrary.Contracts;
using MassTransitLibrary.Contracts.MyMessage;
using MassTransitLibrary.Contracts.MyOrder;
using MassTransitLibrary.Contracts.MyRequest;
using MassTransitLibrary.Contracts.MyTask;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MassTransitProducer
{
    public class Worker : BackgroundService
    {
        readonly ILogger<Worker> _logger;
        readonly IBus _bus;

        public Worker(
            ILogger<Worker> logger,
            IBus bus)
        {
            _logger = logger;
            _bus = bus;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // await ProcessMessageAsync(stoppingToken);
            // await ProcessRequestAsync(stoppingToken);
            // await ProcessOrderAsync(stoppingToken);
            await ProcessTasksAsync(stoppingToken);
        }

        protected async Task ProcessMessageAsync(CancellationToken stoppingToken)
        {
            await _bus.Publish(new MyMessage { Value = $"Message of Damien at {DateTimeOffset.Now}" }, stoppingToken);
        }

        protected async Task ProcessRequestAsync(CancellationToken stoppingToken)
        {
            var client = _bus.CreateRequestClient<MyRequest>();
            
            var (accepted, rejected) = await client.GetResponse<MyRequestReceived, MyRequestRejected>(new MyRequest
            {
                Value = $"Request send Damien at {DateTimeOffset.Now}"
            });

            if (accepted.IsCompleted)
            {
                var acceptedResponse = await accepted;

                _logger.LogInformation($"Message received at {acceptedResponse.Message.ReceivedAt}");
                return;
            }

            var rejectedResponse = await rejected;

            _logger.LogInformation($"Message rejected at {rejectedResponse.Message.ReceivedAt} because {rejectedResponse.Message.Reason}");
        }

        protected async Task ProcessOrderAsync(CancellationToken stoppingToken)
        {
            var order = new MyOrder()
            {
                Id = Guid.NewGuid(),
                Status = OrderStatus.Submitted
            };

            await _bus.Publish<MyOrderSubmitted>(new MyOrderSubmitted()
            {
                CorrelationId = order.Id,
                Order = order
            }, stoppingToken);
        }

        protected async Task ProcessTasksAsync(CancellationToken stoppingToken)
        {
            await _bus.Publish(new MyTask { Type="LadRad", Value = $"LadRad Task1" }, stoppingToken);
            await _bus.Publish(new MyTask { Type = "LadRad", Value = $"LadRad Task2" }, stoppingToken);
            await _bus.Publish(new MyTask { Type = "Export", Value = $"Export Task1" }, stoppingToken);
            await _bus.Publish(new MyTask { Type = "LadRad", Value = $"LadRad Task3" }, stoppingToken);
            await _bus.Publish(new MyTask { Type = "Export", Value = $"Export Task2" }, stoppingToken);
            await _bus.Publish(new MyTask { Type = "Export", Value = $"Export Task3" }, stoppingToken);
        }
    }
}
