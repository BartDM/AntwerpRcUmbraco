CREATE TABLE [dbo].[umbracoUser2NodePermission]
(
[userId] [int] NOT NULL,
[nodeId] [int] NOT NULL,
[permission] [char] (1) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[umbracoUser2NodePermission] ADD CONSTRAINT [PK_umbracoUser2NodePermission] PRIMARY KEY CLUSTERED  ([userId], [nodeId], [permission]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[umbracoUser2NodePermission] ADD CONSTRAINT [FK_umbracoUser2NodePermission_umbracoNode] FOREIGN KEY ([nodeId]) REFERENCES [dbo].[umbracoNode] ([id])
GO
ALTER TABLE [dbo].[umbracoUser2NodePermission] ADD CONSTRAINT [FK_umbracoUser2NodePermission_umbracoUser] FOREIGN KEY ([userId]) REFERENCES [dbo].[umbracoUser] ([id])
GO
