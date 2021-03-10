using Checkout.API.Helper;
using Checkout.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Checkout.API.Services
{
    public class BankTransactionService : IBankTransactionService
    {
        private readonly BankHelper bankHelper;

        public BankTransactionService()
        {
            bankHelper = new BankHelper();
        }
        
        public async Task<TransactionResponse> CreateBankTransaction(CardPaymentRequest transactionRequest)
        {
            var cardTransactionRequest = new CardTransactionRequest()
            {
                MerchantId = transactionRequest.MerchantId,
                Amount = transactionRequest.Amount,
                TransactionTime = new DateTime(),
                Currency = transactionRequest.Currency,
                Reference = transactionRequest.Reference,
                CardDetails = transactionRequest.CardDetails
            };
            return await bankHelper.PostCardTransaction(cardTransactionRequest);
        }
    }
}