CREATE TABLE [dbo].[Status]
(
	[StatusId]       BIGINT NOT NULL,
	[StatusName]     NVARCHAR NOT NULL,
	CONSTRAINT [pk_Status] PRIMARY KEY CLUSTERED (StatusId)
)

GO