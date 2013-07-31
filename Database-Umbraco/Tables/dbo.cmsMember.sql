CREATE TABLE [dbo].[cmsMember]
(
[nodeId] [int] NOT NULL,
[Email] [nvarchar] (1000) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_cmsMember_Email] DEFAULT (''),
[LoginName] [nvarchar] (1000) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_cmsMember_LoginName] DEFAULT (''),
[Password] [nvarchar] (1000) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_cmsMember_Password] DEFAULT ('')
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[cmsMember] ADD CONSTRAINT [PK_cmsMember] PRIMARY KEY CLUSTERED  ([nodeId]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[cmsMember] ADD CONSTRAINT [FK_cmsMember_cmsContent] FOREIGN KEY ([nodeId]) REFERENCES [dbo].[cmsContent] ([nodeId])
GO
ALTER TABLE [dbo].[cmsMember] ADD CONSTRAINT [FK_cmsMember_umbracoNode] FOREIGN KEY ([nodeId]) REFERENCES [dbo].[umbracoNode] ([id])
GO
