CREATE TABLE [dbo].[CalenderItems]
(
[CalenderItemId] [bigint] NOT NULL IDENTITY(1, 1),
[CategoryId] [bigint] NOT NULL,
[SeasonId] [bigint] NULL,
[StartDate] [datetime] NOT NULL,
[EndDate] [datetime] NOT NULL,
[FullDays] [bit] NOT NULL CONSTRAINT [DF__CalenderI__FullD__3D5E1FD2] DEFAULT ((0)),
[HomeTeamId] [bigint] NULL,
[HomeTeamDescription] [varchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[AwayTeamId] [bigint] NULL,
[AwayTeamDescription] [varchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Cancelled] [bit] NOT NULL CONSTRAINT [DF__CalenderI__Cance__3E52440B] DEFAULT ((0)),
[CancellationReason] [varchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[VisibleForPublic] [bit] NOT NULL CONSTRAINT [DF__CalenderI__Visib__3F466844] DEFAULT ((1)),
[AuditDeleted] [bit] NOT NULL CONSTRAINT [DF__CalenderI__Audit__403A8C7D] DEFAULT ((0)),
[AuditCreatedBy] [int] NOT NULL,
[AuditCreatedOn] [datetime] NOT NULL,
[AuditModifiedBy] [int] NULL,
[AuditModifiedOn] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[CalenderItems] ADD CONSTRAINT [PK__Calender__241D7DFB3B75D760] PRIMARY KEY CLUSTERED  ([CalenderItemId]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CalenderItems] ADD CONSTRAINT [FK_CalenderItem_Away_Teams] FOREIGN KEY ([AwayTeamId]) REFERENCES [dbo].[Teams] ([TeamId])
GO
ALTER TABLE [dbo].[CalenderItems] ADD CONSTRAINT [FK_CalenderItem_Category] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Categories] ([CategoryId])
GO
ALTER TABLE [dbo].[CalenderItems] ADD CONSTRAINT [FK_CalenderItem_Home_Teams] FOREIGN KEY ([HomeTeamId]) REFERENCES [dbo].[Teams] ([TeamId])
GO
ALTER TABLE [dbo].[CalenderItems] ADD CONSTRAINT [FK_CalenderItem_Season] FOREIGN KEY ([SeasonId]) REFERENCES [dbo].[Seasons] ([SeasonId])
GO
