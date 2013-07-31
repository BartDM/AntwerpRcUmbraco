CREATE TABLE [dbo].[cmsContentTypeAllowedContentType]
(
[Id] [int] NOT NULL,
[AllowedId] [int] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[cmsContentTypeAllowedContentType] ADD CONSTRAINT [PK_cmsContentTypeAllowedContentType] PRIMARY KEY CLUSTERED  ([Id], [AllowedId]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[cmsContentTypeAllowedContentType] ADD CONSTRAINT [FK_cmsContentTypeAllowedContentType_cmsContentType1] FOREIGN KEY ([AllowedId]) REFERENCES [dbo].[cmsContentType] ([nodeId])
GO
ALTER TABLE [dbo].[cmsContentTypeAllowedContentType] ADD CONSTRAINT [FK_cmsContentTypeAllowedContentType_cmsContentType] FOREIGN KEY ([Id]) REFERENCES [dbo].[cmsContentType] ([nodeId])
GO
