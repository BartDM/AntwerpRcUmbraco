CREATE TABLE [dbo].[Seasons]
(
[SeasonId] [bigint] NOT NULL IDENTITY(1, 1),
[Description] [varchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[StartDate] [datetime] NULL,
[EndDate] [datetime] NULL,
[AuditDeleted] [bit] NOT NULL CONSTRAINT [DF__Seasons__AuditDe__267ABA7A] DEFAULT ((0)),
[AuditCreatedBy] [int] NOT NULL,
[AuditCreatedOn] [datetime] NOT NULL,
[AuditModifiedBy] [int] NULL,
[AuditModifiedOn] [datetime] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Seasons] ADD CONSTRAINT [PK__Seasons__C1814E3824927208] PRIMARY KEY CLUSTERED  ([SeasonId]) ON [PRIMARY]
GO
