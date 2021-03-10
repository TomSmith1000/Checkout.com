using Checkout.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Checkout.API.Services
{
    public interface IPaymentsRecordService
    {
        Task<int> RecordPayment(PaymentRecord payment);

        Task<PaymentRecord> GetPayment(string paymentId);
    }
}