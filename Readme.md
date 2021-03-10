# Checkout.com Payment API Interview Task

This solution consists of three main parts.
- A Payments gateway api that allows a merchant to:
    - process payments
    - retrieve previously processed payments
- A mock aquiring bank api that allows for testing of transaction requests
- A SQL database that stores records of processed payments

## Assumptions made
- I have made the assumption that there is a one to one relationship between the payment gateway and the aquiring bank.
- I have made the assumption that all payment requests received will be for credit cards only.


## Future Development

To develop the project further I would:
- Implement enpoint authentication. The current system has no form of authentication so it allows anyone to query any payment given they know the payment ID. It also allows anyone to make payments.
- Finish unit testing the application and implement intergration tests.
- Implement logging of when certain processes are being run and when exceptions are thrown.
-  If the system were to be scaled up there would be significant value in implementing performance logging and developing a load/stress testing suite to see how well it can cope with an increased number of users.
- Integrate this project into the cloud using a hosting service like azure devops and setup features like continuos integration. 


# Installation

You will need:
- Microsoft Visual studio 2017+
- SQL Server Managment Tools
- An HTTP api client, I recommend Postman

1. Pull the repository onto your local machine
2. Open the Checkout.sln file with Microsoft Visual Studio
3. To setup the database you need to right click the database project and select publish. This will create a new database with a payments table.
4. Update the web.config file within the Checkout.API project with the connection string to the database that has been created. This should look similar to 
<connectionStrings>
    <add name="CheckoutDatabase" connectionString="Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Payments;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False" />
</connectionStrings>


# Running the Payments API

Now everything is installed, you should be able to run the payments api by going to Debug -> start debugging. Make sure you have selected both the Checkout.API and Checkout.MockBankApi to run on start. Both are required for the application to run successfully.

Once the application is running, you can send requests to both the payment gateway api and the mock bank api.

# Payment Gateway API

The payment gateway api has two endpoints you can use.

## Process payments

To use the process payments endpoint you should target the url following url with POST requests. http://localhost:55283/api/Payment 

Requests to this endpoint will require a json in the body of the request in the format: 

{
    "MerchantId": "123",
    "Amount": 1000,
    "Currency": "GBP",
    "Reference": "XXX-YYY",
    "CardDetails": {
        "CardNumber": "4716483443976548",
        "ExpiryMonth": 01,
        "ExpiryYear": 21,
        "CVV": 579
    }
}

If the request is successful, you should see a response with status code 200 and a new entry should be inserted into the local database. Example successful response:

{
    "PaymentId": "667b5037-f116-4919-9a14-90a23f5e50a9",
    "TransactionId": "678a7921-4d33-4c9c-9b11-4fd3303c24f2",
    "Status": "Successful"
}

If there is an error you should see a 400 status code with details of the error encountered.

## Get Payments

To use the get payments endpoint you should target the url following url with GET requests and the payment id appended to the url. 
http://localhost:55283/api/Payment?paymentId=

If the request is successful, you should see a response with status code 200. Example successful response:

{
    "PaymentId": "667b5037-f116-4919-9a14-90a23f5e50a9",
    "TransactionId": "678a7921-4d33-4c9c-9b11-4fd3303c24f2",
    "MerchantId": "123",
    "TransactionTime": "07/03/2021 15:11:18",
    "Amount": 1000,
    "Currency": "GBP",
    "Reference": "XXX-YYY",
    "Status": "Successful",
    "CardNumber": "6548",
    "ExpiryMonth": 1,
    "ExpiryYear": 21
}

If there is an error you should see a 400 status code with details of the error encountered.


## Mock Bank API

The mock bank api is designed to be swapped with a live banking api once in production

## Create Transaction

To create a new transaction using the mock bank api you should target the following url with a GET request. 
http://localhost:57453/api/Transactions

This endpoint will require a json in the request body, for example:
{
    "MerchantId": "123",
    "TransactionTime": "01/01/0001 00:00:00",
    "Amount": 1000,
    "Currency": "GBP",
    "Reference": "XXX-YYY",
    "CardDetails": {
        "CardNumber": "4716483443976548",
        "ExpiryMonth": "01",
        "ExpiryYear": "21",
        "CVV": "579"
    }
}

Example successful response:

{
    "Id": "f2248371-e129-4799-b47a-f7b71f913efa",
    "Status": "Successful"
}





