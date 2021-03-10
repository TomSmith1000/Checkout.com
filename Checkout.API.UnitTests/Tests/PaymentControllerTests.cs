using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Checkout.Shared;
using Checkout.API.Services;
using Moq;
using Checkout.API.Controllers;
using System.Web.Http;
using System.Web.Http.Results;
using System.Collections.Generic;
using System.Threading.Tasks;
using Checkout.Shared.Exceptions;

namespace Checkout.API.UnitTests.Tests
{
    [TestClass]
    public class PaymentControllerTests
    {

        private readonly string PaymentId = "11111";
        private readonly string TransactionId = "22222";
        private readonly string Currency = "GBP";
        private readonly string Reference = "Shoes";
        private readonly string MerchantId = "12345";
        private readonly int Amount = 1000;
        private readonly string CardNumber = "4716483443976548";
        private readonly int ExpiryMonth = 10;
        private readonly int ExpiryYear = 11;
        private readonly int CVV = 555;
        private readonly string TransactionTime = DateTime.Now.ToString();
        private readonly string Status = "Success";


#region Tests
        [TestMethod]
        public async Task GetPayment_Should_Return_Payment_When_PaymentId_Exists_Async()
        {
            // Arrange
            var mockBankTransactionService = new Mock<IBankTransactionService>();
            
            var mockPaymentsRecordService = new Mock<IPaymentsRecordService>();
            var paymentResponse = CreatePaymentResponse();
            var paymentRecord = CreatePaymentRecord();
            mockPaymentsRecordService.Setup(service => service.GetPayment(paymentResponse.PaymentId)).ReturnsAsync(paymentRecord);

            var paymentController = new PaymentController(mockBankTransactionService.Object, mockPaymentsRecordService.Object);

            // Act
            var result = await paymentController.GetPaymentDetails(paymentRecord.PaymentId) as OkNegotiatedContentResult<PaymentRecord>;
            // Assert
            Assert.AreEqual(paymentRecord, result.Content);
        }


        [TestMethod]
        public async Task GetPayment_Should_Return_BadRequest_When_PaymentId_Doesnt_Exists_Async()
        {
            // Arrange
            var mockBankTransactionService = new Mock<IBankTransactionService>();
            var mockPaymentsRecordService = new Mock<IPaymentsRecordService>();
            mockPaymentsRecordService.Setup(service => service.GetPayment(It.IsAny<string>())).ThrowsAsync(new PaymentNotFoundException("Payment could not be found"));

            var paymentController = new PaymentController(mockBankTransactionService.Object, mockPaymentsRecordService.Object);

            // Act
            var result = await paymentController.GetPaymentDetails("1236543") as BadRequestErrorMessageResult;

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestErrorMessageResult));
            Assert.AreEqual("Payment could not be found", result.Message);
        }

        [TestMethod]
        public async Task ProcessCardPayment_Should_Return_Invalid_Field_Name_When_Field_Is_Missing_From_Input_Async()
        {
            // Arrange
            var mockBankTransactionService = new Mock<IBankTransactionService>();
            mockBankTransactionService.Setup(service => service.CreateBankTransaction(CreateCardPaymentRequest())).ReturnsAsync(CreateTransactionResponse());

            var mockPaymentsRecordService = new Mock<IPaymentsRecordService>();

            var paymentRecord = CreatePaymentRecord();
            mockPaymentsRecordService.Setup(service => service.RecordPayment(paymentRecord)).ReturnsAsync(1);

            var paymentController = new PaymentController(mockBankTransactionService.Object, mockPaymentsRecordService.Object);
            var cardPaymentRequest = CreateCardPaymentRequest();
            cardPaymentRequest.MerchantId = null;
            // Act
            var result = await paymentController.ProcessCardPayment(cardPaymentRequest) as BadRequestErrorMessageResult;

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestErrorMessageResult));
            Assert.AreEqual("Invalid MerchantId", result.Message);
        }

        [TestMethod]
        public async Task ProcessCardPayment_Should_Return_TransactionId_And_Status_From_Bank_Async()
        {
            // Arrange
            var cardPaymentRequest = CreateCardPaymentRequest();
            var transactionResponse = CreateTransactionResponse();
            var mockBankTransactionService = new Mock<IBankTransactionService>();
            mockBankTransactionService.Setup(service => service.CreateBankTransaction(cardPaymentRequest)).ReturnsAsync(transactionResponse);

            var mockPaymentsRecordService = new Mock<IPaymentsRecordService>();

            var paymentRecord = CreatePaymentRecord();
            mockPaymentsRecordService.Setup(service => service.RecordPayment(paymentRecord)).ReturnsAsync(1);

            var paymentController = new PaymentController(mockBankTransactionService.Object, mockPaymentsRecordService.Object);
            var paymentResponse = CreatePaymentResponse();

            // Act
            var result = await paymentController.ProcessCardPayment(cardPaymentRequest) as OkNegotiatedContentResult<PaymentResponse>;

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<PaymentResponse>));
            Assert.AreEqual(paymentResponse.Status, result.Content.Status);
            Assert.AreEqual(paymentResponse.TransactionId, result.Content.TransactionId);
        }

        #endregion

        public CardPaymentRequest CreateCardPaymentRequest()
        {
            return new CardPaymentRequest()
            {
                Currency = this.Currency,
                Reference = this.Reference,
                MerchantId = this.MerchantId,
                Amount = this.Amount,
                CardDetails = new CardDetails()
                {
                    CardNumber = this.CardNumber,
                    ExpiryMonth = this.ExpiryMonth,
                    ExpiryYear = this.ExpiryYear,
                    CVV = this.CVV
                }
            };
        }

        public TransactionResponse CreateTransactionResponse()
        {
            return new TransactionResponse()
            {
                Id = this.TransactionId,
                Status = this.Status
            };
        }

        public PaymentResponse CreatePaymentResponse()
        {
            return new PaymentResponse()
            {
                PaymentId = this.PaymentId,
                TransactionId = this.TransactionId,
                Status = this.Status
            };
        }
        public PaymentRecord CreatePaymentRecord()
        {
            return new PaymentRecord()
            {
                PaymentId = this.PaymentId,
                TransactionId = this.TransactionId,
                MerchantId = this.MerchantId,
                TransactionTime = this.TransactionTime,
                Amount = this.Amount,
                Currency = this.Currency,
                Reference = this.Reference,
                Status = this.Status,
                CardNumber = this.CardNumber,
                ExpiryMonth = this.ExpiryMonth,
                ExpiryYear = this.ExpiryYear

            };
        }



    }
}
