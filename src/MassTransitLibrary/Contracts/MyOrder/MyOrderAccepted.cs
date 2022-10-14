namespace MassTransitLibrary.Contracts.MyOrder
{
    public record MyOrderAccepted
    {
        public Guid CorrelationId { get; init; }
        public MyOrder Order { get; init; }
    }
}
