using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Checkout.Shared;

namespace Checkout.MockBankAPI.Controllers
{
    public class TransactionsController : ApiController
    {
        [HttpPost]
        public IHttpActionResult ProcessTransation(CardTransactionRequest request)
        {
            var transactionResponse = new TransactionResponse();
            switch(request.Amount)
            {
                case 100:
                    transactionResponse.Status = Constants.FAILED;
                    break;
                default:
                    transactionResponse.Status = Constants.SUCCESSFUL;
                    break;
            }
            return Ok(transactionResponse);
        }
    }
}
