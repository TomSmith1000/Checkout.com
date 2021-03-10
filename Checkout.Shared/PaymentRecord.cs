using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.Shared
{
    public class PaymentRecord
    {
        public string PaymentId { get; set; } = Guid.NewGuid().ToString();

        public string TransactionId { get; set; }

        public string MerchantId { get; set; }

        public string TransactionTime { get; set; }

        public int Amount { get; set; }

        public string Currency { get; set; }

        public string Reference { get; set; }

        public string Status { get; set; }

        public string CardNumber { get; set; }

        public int ExpiryMonth { get; set; }

        public int ExpiryYear { get; set; }
    }
}
