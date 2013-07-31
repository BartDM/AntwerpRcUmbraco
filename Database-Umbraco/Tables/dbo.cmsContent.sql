CREATE TABLE [dbo].[cmsContent]
(
[pk] [int] NOT NULL IDENTITY(1, 1),
[nodeId] [int] NOT NULL,
[contentType] [int] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[cmsContent] ADD CONSTRAINT [PK_cmsContent] PRIMARY KEY CLUSTERED  ([pk]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[cmsContent] ADD CONSTRAINT [IX_cmsContent] UNIQUE NONCLUSTERED  ([nodeId]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[cmsContent] ADD CONSTRAINT [FK_cmsContent_umbracoNode] FOREIGN KEY ([nodeId]) REFERENCES [dbo].[umbracoNode] ([id])
GO
