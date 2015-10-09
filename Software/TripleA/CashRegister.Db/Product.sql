CREATE TABLE [dbo].[Product]
(
	[ProductId]		BIGINT NOT NULL, 
    [ProductName]	NVARCHAR(MAX) NOT NULL, 
	[DiscountId]	BIGINT,
	CONSTRAINT [pk_Product] PRIMARY KEY (ProductId),
	CONSTRAINT [fk_Product] FOREIGN KEY (DiscountId) REFERENCES [Discount] (DiscountId)
)

GO