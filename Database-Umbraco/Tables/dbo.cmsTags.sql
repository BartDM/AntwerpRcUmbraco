CREATE TABLE [dbo].[cmsTags]
(
[id] [int] NOT NULL IDENTITY(1, 1),
[tag] [varchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[parentId] [int] NULL,
[group] [varchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[cmsTags] ADD CONSTRAINT [PK_cmsTags] PRIMARY KEY CLUSTERED  ([id]) ON [PRIMARY]
GO
