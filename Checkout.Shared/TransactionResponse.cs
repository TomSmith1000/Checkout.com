using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.Shared
{
    public class TransactionResponse
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Status { get; set; }
        
    }
}
