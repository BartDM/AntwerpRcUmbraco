CREATE TABLE [dbo].[cmsStylesheet]
(
[nodeId] [int] NOT NULL,
[filename] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[content] [ntext] COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[cmsStylesheet] ADD CONSTRAINT [PK_cmsStylesheet] PRIMARY KEY CLUSTERED  ([nodeId]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[cmsStylesheet] ADD CONSTRAINT [FK_cmsStylesheet_umbracoNode] FOREIGN KEY ([nodeId]) REFERENCES [dbo].[umbracoNode] ([id])
GO
