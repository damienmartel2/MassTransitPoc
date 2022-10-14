namespace MassTransitLibrary.Contracts.MyOrder
{
    public record MyOrderSubmitted
    {
        public Guid CorrelationId { get; init; }
        public MyOrder Order { get; init; }
    }
}
