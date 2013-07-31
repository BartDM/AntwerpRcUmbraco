CREATE TABLE [dbo].[umbracoUser2NodeNotify]
(
[userId] [int] NOT NULL,
[nodeId] [int] NOT NULL,
[action] [char] (1) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[umbracoUser2NodeNotify] ADD CONSTRAINT [PK_umbracoUser2NodeNotify] PRIMARY KEY CLUSTERED  ([userId], [nodeId], [action]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[umbracoUser2NodeNotify] ADD CONSTRAINT [FK_umbracoUser2NodeNotify_umbracoNode] FOREIGN KEY ([nodeId]) REFERENCES [dbo].[umbracoNode] ([id])
GO
ALTER TABLE [dbo].[umbracoUser2NodeNotify] ADD CONSTRAINT [FK_umbracoUser2NodeNotify_umbracoUser] FOREIGN KEY ([userId]) REFERENCES [dbo].[umbracoUser] ([id])
GO
