//using MassTransit;

//namespace MassTransitLibrary.Contracts.MyOrder
//{
//    public class MyOrderSaga : ISaga, InitiatedBy<IMyOrderSubmitted>, Orchestrates<IMyOrderAccepted>
//    {
//        public Guid CorrelationId { get; set; }
//        public DateTime? SubmitDate { get; set; }
//        public DateTime? AcceptDate { get; set; }

//        public async Task Consume(ConsumeContext<IMyOrderSubmitted> context)
//        {
//            SubmitDate = context.Message.Date;
//        }

//        public async Task Consume(ConsumeContext<IMyOrderAccepted> context)
//        {
//            AcceptDate = context.Message.Date;
//        }
//    }
//}
