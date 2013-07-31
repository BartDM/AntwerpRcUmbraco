CREATE TABLE [dbo].[cmsDictionary]
(
[pk] [int] NOT NULL IDENTITY(1, 1),
[id] [uniqueidentifier] NOT NULL,
[parent] [uniqueidentifier] NOT NULL,
[key] [nvarchar] (1000) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[cmsDictionary] ADD CONSTRAINT [PK_cmsDictionary] PRIMARY KEY CLUSTERED  ([pk]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[cmsDictionary] ADD CONSTRAINT [IX_cmsDictionary] UNIQUE NONCLUSTERED  ([id]) ON [PRIMARY]
GO
