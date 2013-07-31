CREATE TABLE [dbo].[cmsTaskType]
(
[id] [tinyint] NOT NULL IDENTITY(1, 1),
[alias] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[cmsTaskType] ADD CONSTRAINT [PK_cmsTaskType] PRIMARY KEY CLUSTERED  ([id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[cmsTaskType] ADD CONSTRAINT [IX_cmsTaskType] UNIQUE NONCLUSTERED  ([alias]) ON [PRIMARY]
GO
