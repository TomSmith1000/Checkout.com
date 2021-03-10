using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Checkout.Shared;
using Checkout.Shared.Exceptions;

namespace Checkout.API.Helper
{
    public class BankHelper
    {
        
        public async Task<TransactionResponse> PostCardTransaction(CardTransactionRequest request)
        {

            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:57453/");
            
            var response = await client.PostAsJsonAsync("api/Transactions", request);

            if(!response.IsSuccessStatusCode)
            {
                throw new TransactionRequestFailureException();
            }

            TransactionResponse transactionResponse = await response.Content.ReadAsAsync<TransactionResponse>();
            return transactionResponse;

        }
    }
}