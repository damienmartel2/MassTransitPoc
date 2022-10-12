namespace MassTransitLibrary.Contracts
{
    public record MyRequestRejected
    {
        public string Value { get; init; }

        public DateTime ReceivedAt { get; init; }

        public string Reason { get; init; }
    }
}
