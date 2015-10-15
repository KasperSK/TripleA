CREATE TABLE [dbo].[Discount]
(
	[DiscountId]     Bigint IDENTITY NOT NULL,
	[stk]            Bigint NULL,
	CONSTRAINT pk_Discount PRIMARY KEY (DiscountId)
)

GO