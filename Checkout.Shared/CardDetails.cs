using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.Shared
{
    public class CardDetails
    {
        public string CardNumber { get; set; }
        
        public int ExpiryMonth { get; set; }
        
        public int ExpiryYear { get; set; }
        
        public int CVV { get; set; }
    }
}
