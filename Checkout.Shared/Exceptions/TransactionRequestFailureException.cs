using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.Shared.Exceptions
{
    public class TransactionRequestFailureException : Exception
    {
        public TransactionRequestFailureException()
        {
        }

        public TransactionRequestFailureException(string message)
            : base(message)
        {
        }

        public TransactionRequestFailureException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
