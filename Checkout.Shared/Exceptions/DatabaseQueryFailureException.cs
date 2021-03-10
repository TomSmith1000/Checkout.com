using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.Shared.Exceptions
{
    public class DatabaseQueryFailureException : Exception
    {
        public DatabaseQueryFailureException()
        {
        }

        public DatabaseQueryFailureException(string message)
            : base(message)
        {
        }

        public DatabaseQueryFailureException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
