CREATE TABLE [dbo].[TeamAddresses]
(
[TeamAddressId] [bigint] NOT NULL IDENTITY(1, 1),
[TeamId] [bigint] NOT NULL,
[AddressId] [bigint] NOT NULL,
[AuditDeleted] [bit] NOT NULL CONSTRAINT [DF__TeamAddre__Audit__1FCDBCEB] DEFAULT ((0)),
[AuditCreatedBy] [int] NOT NULL,
[AuditCreatedOn] [datetime] NOT NULL,
[AuditModifiedBy] [int] NULL,
[AuditModifiedOn] [datetime] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TeamAddresses] ADD CONSTRAINT [PK__TeamAddr__D393D92F1DE57479] PRIMARY KEY CLUSTERED  ([TeamAddressId]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TeamAddresses] ADD CONSTRAINT [FK_TeamAddresses_Addresses] FOREIGN KEY ([AddressId]) REFERENCES [dbo].[Address] ([AddressId])
GO
ALTER TABLE [dbo].[TeamAddresses] ADD CONSTRAINT [FK_TeamAddresses_Teams] FOREIGN KEY ([TeamId]) REFERENCES [dbo].[Teams] ([TeamId])
GO
