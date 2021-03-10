using System;
using Checkout.API.Validators;
using Checkout.Shared;
using NUnit.Framework;

namespace Checkout.API.UnitTests.Tests
{

    public class CardPaymentsRequestValidatorTests
    {

        private readonly string Currency = "GBP";
        private readonly string Reference = "Shoes";
        private readonly string MerchantId = "12345";
        private readonly int Amount = 1000;
        private readonly string CardNumber = "4716483443976548";
        private readonly int ExpiryMonth = 10;
        private readonly int ExpiryYear = 11;
        private readonly int CVV = 555;
        
        [Test]
        public void Validate_Should_Not_Throw_Excpetion_If_All_Fields_Are_Valid()
        {
            var cardPaymentRequest = CreateCardPaymentRequest();
            try
            {
                CardPaymentRequestValidator.Validate(cardPaymentRequest);
            }
            catch
            {
                NUnit.Framework.Assert.Fail();
            }
        }
        

        [TestCase(-100)]
        [TestCase(-1)]
        public void Validate_Should_Throw_Exception_For_Negative_Amounts(int amount)
        {
            var cardPaymentRequest = CreateCardPaymentRequest();
            cardPaymentRequest.Amount = amount;
            try
            {
                CardPaymentRequestValidator.Validate(cardPaymentRequest);
            }
            catch
            {
                Assert.Pass();
            }
        }

        [TestCase("")]
        [TestCase(null)]
        public void Validate_Should_Throw_Exception_For_Invalid_Reference(string reference)
        {
            var cardPaymentRequest = CreateCardPaymentRequest();
            cardPaymentRequest.Reference = reference;
            try
            {
                CardPaymentRequestValidator.Validate(cardPaymentRequest);
            }
            catch
            {
                Assert.Pass();
            }
        }

        [TestCase("")]
        [TestCase(null)]
        [TestCase("AAA")]
        public void Validate_Should_Throw_Exception_For_Invalid_Currency(string currency)
        {
            var cardPaymentRequest = CreateCardPaymentRequest();
            cardPaymentRequest.Currency = currency;
            try
            {
                CardPaymentRequestValidator.Validate(cardPaymentRequest);
            }
            catch
            {
                Assert.Pass();
            }
        }

        [TestCase("")]
        [TestCase(null)]
        [TestCase("123")]
        [TestCase("3842156837-09")]
        public void Validate_Should_Throw_Exception_For_Invalid_CardNumber(string cardNumber)
        {
            var cardPaymentRequest = CreateCardPaymentRequest();
            cardPaymentRequest.CardDetails.CardNumber = cardNumber;
            try
            {
                CardPaymentRequestValidator.Validate(cardPaymentRequest);
            }
            catch
            {
                Assert.Pass();
            }
        }

        [TestCase("5512004553726680")]
        [TestCase("4556342229342361")]
        [TestCase("5161863544767366")]
        [TestCase("4024007181230391")]
        public void Validate_Should_Not_Throw_Exception_For_Valid_CardNumber(string cardNumber)
        {
            var cardPaymentRequest = CreateCardPaymentRequest();
            cardPaymentRequest.CardDetails.CardNumber = cardNumber;
            try
            {
                CardPaymentRequestValidator.Validate(cardPaymentRequest);
            }
            catch
            {
                Assert.Fail();
            }
        }
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
    }
}
