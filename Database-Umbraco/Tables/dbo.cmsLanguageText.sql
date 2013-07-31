CREATE TABLE [dbo].[cmsLanguageText]
(
[pk] [int] NOT NULL IDENTITY(1, 1),
[languageId] [int] NOT NULL,
[UniqueId] [uniqueidentifier] NOT NULL,
[value] [nvarchar] (1000) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[cmsLanguageText] ADD CONSTRAINT [PK_cmsLanguageText] PRIMARY KEY CLUSTERED  ([pk]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[cmsLanguageText] ADD CONSTRAINT [FK_cmsLanguageText_cmsDictionary] FOREIGN KEY ([UniqueId]) REFERENCES [dbo].[cmsDictionary] ([id])
GO
