CREATE TABLE [dbo].[Transaktion]
(
    [TransaktionId]		BIGINT  NOT NULL,
    [TransaktionDate]	DATETIME NOT NULL,
    [TransaktionPrice]	REAL NOT NULL,
    [OrderId]			BIGINT NOT NULL,
    [PaymentTypeId]		BIGINT NOT NULL,
CONSTRAINT [pk_Transaktion] PRIMARY KEY CLUSTERED (TransaktionId),
CONSTRAINT [fk_Transaktion] FOREIGN KEY (OrderId)
    REFERENCES [OrderList] (OrderId)
    ON DELETE NO ACTION
    ON UPDATE CASCADE,
CONSTRAINT [fk_Transaktion2] FOREIGN KEY (PaymentTypeId)
    REFERENCES [PaymentType] (PaymentTypeId)
    ON DELETE NO ACTION
    ON UPDATE CASCADE
)

GO
