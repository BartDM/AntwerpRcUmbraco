CREATE TABLE [dbo].[cmsContentType]
(
[pk] [int] NOT NULL IDENTITY(1, 1),
[nodeId] [int] NOT NULL,
[alias] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[icon] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[thumbnail] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_cmsContentType_thumbnail] DEFAULT ('folder.png'),
[description] [nvarchar] (1500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[masterContentType] [int] NULL CONSTRAINT [DF_cmsContentType_masterContentType] DEFAULT ((0))
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[cmsContentType] ADD CONSTRAINT [PK_cmsContentType] PRIMARY KEY CLUSTERED  ([pk]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[cmsContentType] ADD CONSTRAINT [IX_cmsContentType] UNIQUE NONCLUSTERED  ([nodeId]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_Icon] ON [dbo].[cmsContentType] ([nodeId], [icon]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[cmsContentType] ADD CONSTRAINT [FK_cmsContentType_umbracoNode] FOREIGN KEY ([nodeId]) REFERENCES [dbo].[umbracoNode] ([id])
GO
