CREATE TABLE [dbo].[Price]
(
	[PriceId]	BIGINT IDENTITY	NOT NULL,
	[Price]		FLOAT	NOT NULL,
	[StartDate]	DATE	NOT NULL,
	[EndDate]	DATE	NOT NULL,
	[ProductId]	BIGINT	NOT NULL,
	CONSTRAINT pk_Price PRIMARY KEY (PriceId),
	CONSTRAINT fk_Price FOREIGN KEY (ProductId) REFERENCES Product (ProductId)
)

GO

    