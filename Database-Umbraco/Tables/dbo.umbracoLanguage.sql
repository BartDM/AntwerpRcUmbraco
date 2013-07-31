CREATE TABLE [dbo].[umbracoLanguage]
(
[id] [smallint] NOT NULL IDENTITY(1, 1),
[languageISOCode] [nvarchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[languageCultureName] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[umbracoLanguage] ADD CONSTRAINT [PK_language] PRIMARY KEY CLUSTERED  ([id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[umbracoLanguage] ADD CONSTRAINT [IX_umbracoLanguage] UNIQUE NONCLUSTERED  ([languageISOCode]) ON [PRIMARY]
GO
