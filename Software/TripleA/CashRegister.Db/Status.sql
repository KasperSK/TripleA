CREATE TABLE [dbo].[Status]
(
	[StatusId]       BIGINT IDENTITY NOT NULL,
	[StatusName]     NVARCHAR(MAX) NOT NULL,
	CONSTRAINT [pk_Status] PRIMARY KEY CLUSTERED (StatusId)
)

GO