﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.Shared
{
    public class PaymentRequest
    {
        public string Currency { get; set; }
        
        public string Reference { get; set; }
        
        public string MerchantId { get; set; }
        
        public int Amount { get; set; }
    }
}
