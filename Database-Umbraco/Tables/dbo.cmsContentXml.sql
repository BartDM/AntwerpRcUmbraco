CREATE TABLE [dbo].[cmsContentXml]
(
[nodeId] [int] NOT NULL,
[xml] [ntext] COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[cmsContentXml] ADD CONSTRAINT [PK_cmsContentXml] PRIMARY KEY CLUSTERED  ([nodeId]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[cmsContentXml] ADD CONSTRAINT [FK_cmsContentXml_cmsContent] FOREIGN KEY ([nodeId]) REFERENCES [dbo].[cmsContent] ([nodeId])
GO
