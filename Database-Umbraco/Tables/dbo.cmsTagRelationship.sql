CREATE TABLE [dbo].[cmsTagRelationship]
(
[nodeId] [int] NOT NULL,
[tagId] [int] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[cmsTagRelationship] ADD CONSTRAINT [PK_cmsTagRelationship] PRIMARY KEY CLUSTERED  ([nodeId], [tagId]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[cmsTagRelationship] ADD CONSTRAINT [umbracoNode_cmsTagRelationship] FOREIGN KEY ([nodeId]) REFERENCES [dbo].[umbracoNode] ([id]) ON DELETE CASCADE
GO
ALTER TABLE [dbo].[cmsTagRelationship] ADD CONSTRAINT [cmsTags_cmsTagRelationship] FOREIGN KEY ([tagId]) REFERENCES [dbo].[cmsTags] ([id]) ON DELETE CASCADE
GO
