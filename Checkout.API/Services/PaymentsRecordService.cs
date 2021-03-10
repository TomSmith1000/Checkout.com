using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Checkout.API.Helper;
using Checkout.Shared;
using Checkout.Shared.Exceptions;

namespace Checkout.API.Services
{
    public class PaymentsRecordService : IPaymentsRecordService
    {
        private readonly DatabaseHelper databaseHelper;

        public PaymentsRecordService()
        {
            databaseHelper = new DatabaseHelper();
        }

        public async Task<PaymentRecord> GetPayment(string paymentId)
        {
            var result = await databaseHelper.GetPayment(paymentId);
            if (result == null)
            {
                throw new PaymentNotFoundException("Payment could not be found");
            }
            result.CardNumber = MaskCardNumber(result.CardNumber);
            return result;
        }

        public async Task<int> RecordPayment(PaymentRecord payment)
        {
            var result = await databaseHelper.CreatePaymentRecord(payment);
            return result;

        }

        private string MaskCardNumber(string cardNumber)
        {
            return cardNumber.Substring(cardNumber.Length - 4);
        }
    }
}