CREATE TABLE [dbo].[Categories]
(
[CategoryId] [bigint] NOT NULL IDENTITY(1, 1),
[Description] [varchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[DefaultGameTime] [int] NULL,
[DefaultStartTime] [time] NULL,
[AuditDeleted] [bit] NOT NULL CONSTRAINT [DF__Categorie__Audit__30F848ED] DEFAULT ((0)),
[AuditCreatedBy] [int] NOT NULL,
[AuditCreatedOn] [datetime] NOT NULL,
[AuditModifiedBy] [int] NULL,
[AuditModifiedOn] [datetime] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Categories] ADD CONSTRAINT [PK__Categori__19093A0B2F10007B] PRIMARY KEY CLUSTERED  ([CategoryId]) ON [PRIMARY]
GO
