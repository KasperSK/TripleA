CREATE TABLE [dbo].[PaymentType]
(
    [PaymentTypeId]			 BIGINT IDENTITY NOT NULL,
    [PaymentTypeDescription] BIGINT NOT NULL,
CONSTRAINT [pk_PaymentType] PRIMARY KEY CLUSTERED (PaymentTypeId)
)

GO
