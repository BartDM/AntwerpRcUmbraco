CREATE TABLE [dbo].[cmsMemberType]
(
[pk] [int] NOT NULL IDENTITY(1, 1),
[NodeId] [int] NOT NULL,
[propertytypeId] [int] NOT NULL,
[memberCanEdit] [bit] NOT NULL CONSTRAINT [DF_cmsMemberType_memberCanEdit] DEFAULT ((0)),
[viewOnProfile] [bit] NOT NULL CONSTRAINT [DF_cmsMemberType_viewOnProfile] DEFAULT ((0))
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[cmsMemberType] ADD CONSTRAINT [PK_cmsMemberType] PRIMARY KEY CLUSTERED  ([pk]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[cmsMemberType] ADD CONSTRAINT [FK_cmsMemberType_cmsContentType] FOREIGN KEY ([NodeId]) REFERENCES [dbo].[cmsContentType] ([nodeId])
GO
ALTER TABLE [dbo].[cmsMemberType] ADD CONSTRAINT [FK_cmsMemberType_umbracoNode] FOREIGN KEY ([NodeId]) REFERENCES [dbo].[umbracoNode] ([id])
GO
