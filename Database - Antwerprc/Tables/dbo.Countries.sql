CREATE TABLE [dbo].[Countries]
(
[CountryId] [bigint] NOT NULL IDENTITY(1, 1),
[Description] [varchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[AuditDeleted] [bit] NOT NULL CONSTRAINT [DF__Countries__Audit__07F6335A] DEFAULT ((0)),
[AuditCreatedBy] [int] NOT NULL,
[AuditCreatedOn] [datetime] NOT NULL,
[AuditModifiedBy] [int] NULL,
[AuditModifiedOn] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Countries] ADD CONSTRAINT [PK_Countries] PRIMARY KEY CLUSTERED  ([CountryId]) ON [PRIMARY]
GO
