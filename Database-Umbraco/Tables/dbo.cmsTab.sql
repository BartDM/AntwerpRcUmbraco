CREATE TABLE [dbo].[cmsTab]
(
[id] [int] NOT NULL IDENTITY(1, 1),
[contenttypeNodeId] [int] NOT NULL,
[text] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[sortorder] [int] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[cmsTab] ADD CONSTRAINT [PK_cmsTab] PRIMARY KEY CLUSTERED  ([id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[cmsTab] ADD CONSTRAINT [FK_cmsTab_cmsContentType] FOREIGN KEY ([contenttypeNodeId]) REFERENCES [dbo].[cmsContentType] ([nodeId])
GO
