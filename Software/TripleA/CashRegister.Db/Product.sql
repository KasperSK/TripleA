CREATE TABLE [dbo].[Product]
(
	[ProductId]	 BIGINT NOT NULL, 
    [ProductName] NVARCHAR(MAX) NOT NULL, 
	[DiscountID]  BIGINT,
CONSTRAINT [pk_Product] PRIMARY KEY (ProductID),
CONSTRAINT [fk_Product] FOREIGN KEY (DiscountID)
	REFERENCES [Discount] (DiscountID)
)

GO


