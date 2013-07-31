CREATE TABLE [dbo].[CategoryTeams]
(
[CategoryTeamId] [bigint] NOT NULL IDENTITY(1, 1),
[CategoryId] [bigint] NOT NULL,
[TeamId] [bigint] NOT NULL,
[SeasonId] [bigint] NOT NULL,
[AuditDeleted] [bit] NOT NULL CONSTRAINT [DF__CategoryT__Audit__35BCFE0A] DEFAULT ((0)),
[AuditCreatedBy] [int] NOT NULL,
[AuditCreatedOn] [datetime] NOT NULL,
[AuditModifiedBy] [int] NULL,
[AuditModifiedOn] [datetime] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CategoryTeams] ADD CONSTRAINT [PK__Category__DB6FF1A133D4B598] PRIMARY KEY CLUSTERED  ([CategoryTeamId]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CategoryTeams] ADD CONSTRAINT [FK_CategoryTeams_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Categories] ([CategoryId])
GO
ALTER TABLE [dbo].[CategoryTeams] ADD CONSTRAINT [FK_CategoryTeams_Seasons] FOREIGN KEY ([SeasonId]) REFERENCES [dbo].[Seasons] ([SeasonId])
GO
ALTER TABLE [dbo].[CategoryTeams] ADD CONSTRAINT [FK_CategoryTeams_Teams] FOREIGN KEY ([TeamId]) REFERENCES [dbo].[Teams] ([TeamId])
GO
