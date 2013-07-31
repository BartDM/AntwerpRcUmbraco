CREATE TABLE [dbo].[cmsTask]
(
[closed] [bit] NOT NULL CONSTRAINT [DF__cmsTask__closed__04E4BC85] DEFAULT ((0)),
[id] [int] NOT NULL IDENTITY(1, 1),
[taskTypeId] [tinyint] NOT NULL,
[nodeId] [int] NOT NULL,
[parentUserId] [int] NOT NULL,
[userId] [int] NOT NULL,
[DateTime] [datetime] NOT NULL CONSTRAINT [DF__cmsTask__DateTim__05D8E0BE] DEFAULT (getdate()),
[Comment] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[cmsTask] ADD CONSTRAINT [PK_cmsTask] PRIMARY KEY CLUSTERED  ([id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[cmsTask] ADD CONSTRAINT [FK_cmsTask_umbracoNode] FOREIGN KEY ([nodeId]) REFERENCES [dbo].[umbracoNode] ([id])
GO
ALTER TABLE [dbo].[cmsTask] ADD CONSTRAINT [FK_cmsTask_umbracoUser] FOREIGN KEY ([parentUserId]) REFERENCES [dbo].[umbracoUser] ([id])
GO
ALTER TABLE [dbo].[cmsTask] ADD CONSTRAINT [FK_cmsTask_cmsTaskType] FOREIGN KEY ([taskTypeId]) REFERENCES [dbo].[cmsTaskType] ([id])
GO
ALTER TABLE [dbo].[cmsTask] ADD CONSTRAINT [FK_cmsTask_umbracoUser1] FOREIGN KEY ([userId]) REFERENCES [dbo].[umbracoUser] ([id])
GO
