using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MassTransitLibrary.Contracts;
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
            //await ProcessMessageAsync(stoppingToken);
            await ProcessRequestAsync(stoppingToken);
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
            }

            var rejectedResponse = await rejected;

            _logger.LogInformation($"Message rejected at {rejectedResponse.Message.ReceivedAt} because {rejectedResponse.Message.Reason}");
        }
    }
}
