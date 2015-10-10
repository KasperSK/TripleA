CREATE TABLE ProductGroup (
	[ProductGroupId]	BIGINT IDENTITY NOT NULL,
	[GroupName]			NVARCHAR(MAX) NOT NULL,
	[ProductSubGroupId]	BIGINT,
	CONSTRAINT [pk_ProductGroup] PRIMARY KEY (ProductGroupId),
)

GO