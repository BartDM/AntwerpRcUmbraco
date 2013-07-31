CREATE TABLE [dbo].[Teams]
(
[TeamId] [bigint] NOT NULL IDENTITY(1, 1),
[TeamName] [varchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[AuditDeleted] [bit] NOT NULL CONSTRAINT [DF__Teams__AuditDele__060DEAE8] DEFAULT ((0)),
[AuditCreatedBy] [int] NOT NULL,
[AuditCreatedOn] [datetime] NOT NULL,
[AuditModifiedBy] [int] NULL,
[AuditModifiedOn] [datetime] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Teams] ADD CONSTRAINT [PK__Teams__123AE7990425A276] PRIMARY KEY CLUSTERED  ([TeamId]) ON [PRIMARY]
GO
