using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Checkout.Shared;
using Checkout.API.Services;
using Checkout.API.Validators;
using Checkout.Shared.Exceptions;

namespace Checkout.API.Controllers
{
    public class PaymentController : ApiController
    {
        private readonly IBankTransactionService _bankService;
        private readonly IPaymentsRecordService _paymentService;

        public PaymentController(IBankTransactionService bankService, IPaymentsRecordService paymentService)
        {
            _bankService = bankService;
            _paymentService = paymentService;
        }

        [HttpPost]
        public async Task<IHttpActionResult> ProcessCardPayment([FromBody] CardPaymentRequest cardPayment)
        {
            try
            {
                CardPaymentRequestValidator.Validate(cardPayment);

                // Send Transaction request to aquiring bank 
                var paymentTime = DateTime.UtcNow.ToString();
                var bankRequest = await _bankService.CreateBankTransaction(cardPayment);
                
                // Record payment details in database
                var payment = new PaymentRecord()
                {
                    TransactionId = bankRequest.Id,
                    MerchantId = cardPayment.MerchantId,
                    TransactionTime = paymentTime,
                    Amount = cardPayment.Amount,
                    Currency = cardPayment.Currency,
                    Reference = cardPayment.Reference,
                    Status = bankRequest.Status,
                    CardNumber = cardPayment.CardDetails.CardNumber,
                    ExpiryMonth = cardPayment.CardDetails.ExpiryMonth,
                    ExpiryYear = cardPayment.CardDetails.ExpiryYear
                };
                await _paymentService.RecordPayment(payment);

                // Create payment response
                var paymentResponse = new PaymentResponse(payment);

                return Ok(paymentResponse);

            }
            catch (InvalidPaymentRequestException exception)
            {
                return BadRequest(exception.Message);
            }
            catch
            {
                return BadRequest("An error occured whilst creating new transaction");
            }
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetPaymentDetails(string paymentID)
        {
            try
            {
                var paymentDetails = await _paymentService.GetPayment(paymentID);

                return Ok(paymentDetails);
            }
            catch (PaymentNotFoundException exception)
            {
                return BadRequest(exception.Message);
            }
            catch
            {
                return BadRequest("An error occured whilst getting payment details");
            }
        }
    }
}
