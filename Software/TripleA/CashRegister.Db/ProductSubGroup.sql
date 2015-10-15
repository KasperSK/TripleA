CREATE TABLE [dbo].[ProductSubGroup]
(
	[ProductSubGroupId] BIGINT IDENTITY NOT NULL, 
    [SubGroupName]		NVARCHAR(MAX) NOT NULL,
	[ProductGroupId]	BIGINT NULL,
	CONSTRAINT [pk_ProductSubGroup] PRIMARY KEY (ProductSubGroupId),
	CONSTRAINT fk_ProductSubGroup FOREIGN KEY (ProductGroupId) REFERENCES ProductGroup (ProductGroupId)
    ON UPDATE CASCADE
)

GO