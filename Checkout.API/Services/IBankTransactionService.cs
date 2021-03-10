using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Checkout.Shared;

namespace Checkout.API.Services
{
    public interface IBankTransactionService
    {
        Task<TransactionResponse> CreateBankTransaction(CardPaymentRequest transactionRequest);
    }
}