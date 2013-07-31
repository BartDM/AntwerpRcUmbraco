CREATE TABLE [dbo].[umbracoDomains]
(
[id] [int] NOT NULL IDENTITY(1, 1),
[domainDefaultLanguage] [int] NULL,
[domainRootStructureID] [int] NULL,
[domainName] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[umbracoDomains] ADD CONSTRAINT [PK_domains] PRIMARY KEY CLUSTERED  ([id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[umbracoDomains] ADD CONSTRAINT [FK_umbracoDomains_umbracoNode] FOREIGN KEY ([domainRootStructureID]) REFERENCES [dbo].[umbracoNode] ([id])
GO
