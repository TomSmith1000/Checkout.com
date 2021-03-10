using Checkout.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Checkout.Shared.Exceptions;
using System.Reflection;

namespace Checkout.API.Validators
{
    public static class CardPaymentRequestValidator
    {
        public static void Validate(CardPaymentRequest model)
        {
            ValidateNullOrEmpty(model);
            ValidateCurrency(model.Currency);
            ValidateAmount(model.Amount);
            ValidateExpiryMonth(model.CardDetails.ExpiryMonth);
            ValidateExpiryYear(model.CardDetails.ExpiryYear);
            ValidateCVV(model.CardDetails.CVV);
            ValidateCardNumber(model.CardDetails.CardNumber);
        }

        private static void ValidateNullOrEmpty(CardPaymentRequest cardPaymentRequest)
        {
            if(string.IsNullOrEmpty(cardPaymentRequest.MerchantId))
            {
                foreach (PropertyInfo pi in cardPaymentRequest.GetType().GetProperties())
                {
                    if (pi.PropertyType == typeof(string))
                    {
                        string value = (string)pi.GetValue(cardPaymentRequest);
                        if (string.IsNullOrEmpty(value))
                        {
                            throw new InvalidPaymentRequestException("Invalid " + pi.Name);
                        }
                    }
                }
            }
        }

        private static void ValidateCurrency(string currency)
        {
            if(!Constants.Currencies.Contains(currency))
            {
                throw new InvalidPaymentRequestException("Invalid Currency");
            }
        }

        private static void ValidateAmount(int amount)
        {
            if (amount <= 0)
            {
                throw new InvalidPaymentRequestException("Invalid Amount");
            }
        }

        private static void ValidateExpiryMonth(int month)
        {
            if (month < 0 || month > 12)
            {
                throw new InvalidPaymentRequestException("Invalid Expiry Month");
            }
        }

        private static void ValidateExpiryYear(int year)
        {
            if (year < 0 || year > DateTime.Today.Year)
            {
                throw new InvalidPaymentRequestException("Invalid Expiry Year");
            }
        }

        private static void ValidateCVV(int CVV)
        {
            if(CVV < 0 || CVV > 999)
            {
                throw new InvalidPaymentRequestException("Invalid CVV");
            }
        }

        // Validate wth Luhns Algorithm
        private static void ValidateCardNumber(string cardNumber)
        {
            int sum = 0;
            bool alternate = false;
            for (int i = cardNumber.Length - 1; i >= 0; i--)
            {
                char[] nx = cardNumber.ToArray();
                int n = int.Parse(nx[i].ToString());

                if (alternate)
                {
                    n *= 2;

                    if (n > 9)
                    {
                        n = (n % 10) + 1;
                    }
                }
                sum += n;
                alternate = !alternate;
            }
            if (sum % 10 != 0)
            {
                throw new InvalidPaymentRequestException("Invalid Card Number");
            }
        }
    }
}