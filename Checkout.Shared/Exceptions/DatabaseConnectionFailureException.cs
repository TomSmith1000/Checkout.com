using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.Shared.Exceptions
{
    public class DatabaseConnectionFailureException : Exception
    {
        public DatabaseConnectionFailureException()
        {
        }

        public DatabaseConnectionFailureException(string message)
            : base(message)
        {
        }

        public DatabaseConnectionFailureException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
