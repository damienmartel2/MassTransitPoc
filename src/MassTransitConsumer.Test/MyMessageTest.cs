using MassTransit;
using MassTransit.Testing;
using MassTransitLibrary.Consumers.MyMessageConsumer;
using MassTransitLibrary.Contracts.MyMessage;
using Microsoft.Extensions.Logging;
using Moq;

namespace MassTransitConsumer.Test
{
    public class MyMessageTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task TestConsumer()
        {
            var harness = new InMemoryTestHarness();
            var logger = Mock.Of<ILogger<MyMessageConsumer>>();
            var consumerHarness = harness.Consumer<MyMessageConsumer>(() => new MyMessageConsumer(logger));

            await harness.Start();
            try
            {
                const string messageValue = "Hello World!";

                var message = new MyMessage()
                {
                    Value = messageValue
                };


                await harness.InputQueueSendEndpoint.Send(message);

                // did the endpoint consume the message
                Assert.IsTrue(harness.Consumed.Select<MyMessage>().Any());

                // did the actual consumer consume the message
                var messageConsumed = consumerHarness.Consumed.Select<MyMessage>();

                Assert.IsTrue(messageConsumed.Any());

                var messageReturn = (MyMessage)messageConsumed.First().MessageObject;
                Assert.That(messageReturn.Value, Is.EqualTo(messageValue));
            }
            finally
            {
                await harness.Stop();
            }

            Assert.IsTrue(await harness.Sent.Any<MyMessage>());
        }
    }
}