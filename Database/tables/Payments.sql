CREATE TABLE [dbo].[Payments]
(
	[PaymentId] VARCHAR(255) NOT NULL PRIMARY KEY, 
    [TransactionId] VARCHAR(255) NULL, 
    [MerchantId] VARCHAR(255) NOT NULL, 
    [TransactionTime] VARCHAR(255) NOT NULL, 
    [Amount] INT NOT NULL, 
    [Currency] VARCHAR(3) NOT NULL, 
    [Reference] VARCHAR(255) NULL, 
    [Status] VARCHAR(255) NULL, 
    [CardNumber] VARCHAR(255) NOT NULL, 
    [ExpiryMonth] INT NOT NULL, 
    [ExpiryYear] INT NOT NULL
)
