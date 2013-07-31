CREATE TABLE [dbo].[cmsTemplate]
(
[pk] [int] NOT NULL IDENTITY(1, 1),
[nodeId] [int] NOT NULL,
[master] [int] NULL,
[alias] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[design] [ntext] COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[cmsTemplate] ADD CONSTRAINT [PK_templates] PRIMARY KEY CLUSTERED  ([pk]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[cmsTemplate] ADD CONSTRAINT [IX_cmsTemplate] UNIQUE NONCLUSTERED  ([nodeId]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[cmsTemplate] ADD CONSTRAINT [FK_cmsTemplate_cmsTemplate] FOREIGN KEY ([master]) REFERENCES [dbo].[cmsTemplate] ([nodeId])
GO
ALTER TABLE [dbo].[cmsTemplate] ADD CONSTRAINT [FK_cmsTemplate_umbracoNode] FOREIGN KEY ([nodeId]) REFERENCES [dbo].[umbracoNode] ([id])
GO
