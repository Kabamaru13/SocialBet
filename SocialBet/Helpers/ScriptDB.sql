SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BetCategories](
	[Id] [int] NOT NULL,
	[Name] [varchar](120) NULL,
	[ParentId] [int] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[BetCategories] ADD PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PrizeCategories](
	[Id] [int] NOT NULL,
	[Name] [varchar](120) NULL,
	[ParentId] [int] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[PrizeCategories] ADD PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[States](
	[Id] [int] NOT NULL,
	[Name] [varchar](120) NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[States] ADD PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] NOT NULL,
	[FirstName] [varchar](250) NULL,
	[LastName] [varchar](250) NULL,
	[Username] [varchar](250) NULL,
	[PasswordHash] [varbinary](max) NULL,
	[PasswordSalt] [varbinary](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Users] ADD PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ('') FOR [FirstName]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ('') FOR [LastName]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ('') FOR [Username]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (NULL) FOR [PasswordHash]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (NULL) FOR [PasswordSalt]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bets](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](1024) NULL,
	[CreatorId] [int] NULL,
	[RivalId] [int] NULL,
	[ReferreeId] [int] NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[BetCategoryId] [int] NULL,
	[BetDescription] [varchar](1024) NULL,
	[PrizeCategoryId] [int] NULL,
	[PrizeDescription] [varchar](1024) NULL,
	[State] [int] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Bets] ADD PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Bets] ADD  DEFAULT (NULL) FOR [Description]
GO
ALTER TABLE [dbo].[Bets] ADD  DEFAULT (NULL) FOR [StartDate]
GO
ALTER TABLE [dbo].[Bets] ADD  DEFAULT (getdate()) FOR [EndDate]
GO
ALTER TABLE [dbo].[Bets] ADD  DEFAULT (NULL) FOR [BetDescription]
GO
ALTER TABLE [dbo].[Bets] ADD  DEFAULT (NULL) FOR [PrizeDescription]
GO
