CREATE TABLE [dbo].[OrderList_Product]
(
	[OrderId]        BIGINT NOT NULL,
    [ProductId]      BIGINT NOT NULL,
	CONSTRAINT [pk_OrderList_Product] PRIMARY KEY CLUSTERED (OrderId,ProductId),
	CONSTRAINT [fk_OrderList_Product] FOREIGN KEY (OrderId) REFERENCES OrderList (OrderId)
		ON DELETE NO ACTION
		ON UPDATE CASCADE,
	CONSTRAINT fk_OrderList_Product2 FOREIGN KEY (ProductId) REFERENCES Product (ProductId)
		ON DELETE NO ACTION
		ON UPDATE CASCADE
)


