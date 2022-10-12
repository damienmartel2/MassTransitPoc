namespace MassTransitLibrary.Contracts.MyRequest
{
    public record MyRequestReceived
    {
        public string Value { get; init; }

        public DateTime ReceivedAt { get; init; }
    }
}
