namespace MassTransitLibrary.Contracts
{
    public record MyRequestReceived
    {
        public string Value { get; init; }

        public DateTime ReceivedAt { get; init; }
    }
}
