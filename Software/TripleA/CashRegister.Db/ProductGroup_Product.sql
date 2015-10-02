CREATE TABLE [dbo].[ProductGroup_Product]
(
	[ProductId]			BIGINT NOT NULL,
	[ProductGroupId]	BIGINT NOT NULL,
	CONSTRAINT pk_ProductGroup_Product PRIMARY KEY CLUSTERED (ProductId,ProductGroupId),
	CONSTRAINT fk_ProductGroup_Product FOREIGN KEY (ProductId) REFERENCES Product (ProductId)
		ON DELETE NO ACTION
		ON UPDATE CASCADE,
	CONSTRAINT fk_ProductGroup_Product2 FOREIGN KEY (ProductGroupId) REFERENCES ProductGroup (ProductGroupId)
		ON DELETE NO ACTION
		ON UPDATE CASCADE
)

GO