using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassTransitLibrary.Contracts.MyOrder
{
    [Serializable]
    public class MyOrder
    {
        public Guid Id { get; set; }
        public DateTime? SubmittedDate { get; set; }
        public DateTime? AcceptedDate { get; set; }
        public OrderStatus Status { get; set; }
    }
}
