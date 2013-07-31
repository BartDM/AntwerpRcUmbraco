CREATE TABLE [dbo].[umbracoUserLogins]
(
[contextID] [uniqueidentifier] NOT NULL,
[userID] [int] NOT NULL,
[timeout] [bigint] NOT NULL
) ON [PRIMARY]
GO
CREATE CLUSTERED INDEX [umbracoUserLogins_Index] ON [dbo].[umbracoUserLogins] ([contextID]) ON [PRIMARY]
GO
