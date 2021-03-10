using Checkout.Shared;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;
using Dapper.Contrib.Extensions;
using Checkout.Shared.Exceptions;
using System.Threading.Tasks;

namespace Checkout.API.Helper
{
    public class DatabaseHelper
    {    
        public async Task<PaymentRecord> GetPayment(string paymentId)
        {
           using (IDbConnection connection = new SqlConnection(connectionString()))
            {
                string query = $"SELECT * FROM Payments WHERE PaymentId = '{ paymentId }'";
                try
                {
                    var output = await connection.QueryFirstOrDefaultAsync<PaymentRecord>(query);
                    return output;
                }
                catch
                {
                    throw new DatabaseQueryFailureException();
                }
            }
        }

        public async Task<int> CreatePaymentRecord(PaymentRecord payment)
        {
            var query = $"INSERT INTO Payments (PaymentId, TransactionId, MerchantId, TransactionTime, Amount, Currency, Reference, Status, CardNumber, ExpiryMonth, ExpiryYear) " +
                $"VALUES (@PaymentId, @TransactionId, @MerchantId, @TransactionTime, @Amount, @Currency, @Reference, @Status, @CardNumber, @ExpiryMonth, @ExpiryYear)";

            using (IDbConnection connection = new SqlConnection(connectionString()))
            {
                try
                {
                    var output = await connection.ExecuteAsync(query,payment);
                    return output;
                                       
                }
                catch
                {
                    throw new DatabaseQueryFailureException();
                }
            }
        }

        public string connectionString()
        {
            return ConfigurationManager.ConnectionStrings["CheckoutDatabase"].ConnectionString;
        }
        
    }
}