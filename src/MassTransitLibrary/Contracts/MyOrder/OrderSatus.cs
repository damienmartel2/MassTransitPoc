using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassTransitLibrary.Contracts.MyOrder
{
    [Serializable]
    public enum OrderStatus
    {
        Submitted,
        Accepted,
    }
}
