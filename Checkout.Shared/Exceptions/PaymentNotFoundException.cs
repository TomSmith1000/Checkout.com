using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.Shared.Exceptions
{
    public class PaymentNotFoundException : Exception
    {
        public PaymentNotFoundException()
        {
        }

        public PaymentNotFoundException(string message)
            : base(message)
        {
        }

        public PaymentNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
