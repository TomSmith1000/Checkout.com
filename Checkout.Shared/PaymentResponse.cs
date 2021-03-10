using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.Shared
{
    public class PaymentResponse
    {
        public PaymentResponse() { }

        public PaymentResponse(PaymentRecord payment)
        {
            PaymentId = payment.PaymentId;
            TransactionId = payment.TransactionId;
            Status = payment.Status;
        }

        public string PaymentId { get; set; }

        public string TransactionId { get; set; }

        public string Status { get; set; }

    }
}
