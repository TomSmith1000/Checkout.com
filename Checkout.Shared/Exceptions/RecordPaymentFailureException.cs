using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.Shared.Exceptions
{
    public class RecordPaymentFailureException : Exception
    {
        public RecordPaymentFailureException()
        {
        }

        public RecordPaymentFailureException(string message)
            : base(message)
        {
        }

        public RecordPaymentFailureException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
