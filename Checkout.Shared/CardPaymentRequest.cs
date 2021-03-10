using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.Shared
{
    public class CardPaymentRequest : PaymentRequest
    {
        public CardDetails CardDetails { get; set; }
        
    }
}
