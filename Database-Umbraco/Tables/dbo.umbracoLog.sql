CREATE TABLE [dbo].[umbracoLog]
(
[id] [int] NOT NULL IDENTITY(1, 1),
[userId] [int] NOT NULL,
[NodeId] [int] NOT NULL,
[Datestamp] [datetime] NOT NULL CONSTRAINT [DF_umbracoLog_Datestamp] DEFAULT (getdate()),
[logHeader] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[logComment] [nvarchar] (4000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[umbracoLog] ADD CONSTRAINT [PK_umbracoLog] PRIMARY KEY CLUSTERED  ([id]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_umbracoLog] ON [dbo].[umbracoLog] ([NodeId]) ON [PRIMARY]
GO
