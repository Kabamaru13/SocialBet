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
ALTER TABLE [dbo].[BetCategories]  WITH CHECK ADD FOREIGN KEY([ParentId])
REFERENCES [dbo].[BetCategories] ([Id])
GO

insert into dbo.BetCategories values (1, 'Sports', null)
insert into dbo.BetCategories values (2, 'Football', 1)
insert into dbo.BetCategories values (3, 'Basketball', 1)
insert into dbo.BetCategories values (4, 'Skills', null)
insert into dbo.BetCategories values (5, 'Fun', null)
insert into dbo.BetCategories values (6, 'Family', null)
insert into dbo.BetCategories values (7, 'Random', null)

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
ALTER TABLE [dbo].[PrizeCategories]  WITH CHECK ADD FOREIGN KEY([ParentId])
REFERENCES [dbo].[PrizeCategories] ([Id])
GO

insert into dbo.PrizeCategories values (1, 'Money', null)
insert into dbo.PrizeCategories values (2, 'Food', null)
insert into dbo.PrizeCategories values (3, 'Drinks', null)
insert into dbo.PrizeCategories values (4, 'Personal Favors', null)
insert into dbo.PrizeCategories values (5, 'Random', null)

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

insert into dbo.States values (1, 'Pending')
insert into dbo.States values (2, 'InProgress')
insert into dbo.States values (3, 'Cancelled')
insert into dbo.States values (4, 'Completed')

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
    [Id] [varchar](50) NOT NULL,
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

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserStats](
    [Id] [varchar](50) NOT NULL,
    [NumOfBets] [int] NOT NULL,
    [NumOfWins] [int] NOT NULL,
    [NumOfLosses] [int] NOT NULL,
    [NumOfDraws] [int] NOT NULL,
    [NumOfReferreed] [int] NOT NULL,
    [NumOfCancelled] [int] NOT NULL,
    [BadgesEarned] [int] NOT NULL,
    [Level] [int] NOT NULL,
    [Experience] [int] NOT NULL,
    [Title] [varchar](120) NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[UserStats] ADD PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[UserStats]  WITH CHECK ADD FOREIGN KEY([Id])
REFERENCES [dbo].[Users] ([Id])
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bets](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [Description] [varchar](1024) NULL,
    [CreatorId] [varchar](50) NOT NULL,
    [RivalId] [varchar](50) NOT NULL,
    [ReferreeId] [varchar](50) NOT NULL,
    [StartDate] [datetime] NULL,
    [EndDate] [datetime] NULL,
    [BetCategoryId] [int] NOT NULL,
    [BetDescription] [varchar](1024) NULL,
    [PrizeCategoryId] [int] NOT NULL,
    [PrizeDescription] [varchar](1024) NULL,
    [State] [int] NOT NULL,
    [WinnerId] [varchar](50) NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Bets] ADD PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Bets]  WITH CHECK ADD FOREIGN KEY([BetCategoryId])
REFERENCES [dbo].[BetCategories] ([Id])
GO
ALTER TABLE [dbo].[Bets]  WITH CHECK ADD FOREIGN KEY([CreatorId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Bets]  WITH CHECK ADD FOREIGN KEY([PrizeCategoryId])
REFERENCES [dbo].[PrizeCategories] ([Id])
GO
ALTER TABLE [dbo].[Bets]  WITH CHECK ADD FOREIGN KEY([ReferreeId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Bets]  WITH CHECK ADD FOREIGN KEY([RivalId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Bets]  WITH CHECK ADD FOREIGN KEY([State])
REFERENCES [dbo].[States] ([Id])
GO
ALTER TABLE [dbo].[Bets]  WITH CHECK ADD FOREIGN KEY([WinnerId])
REFERENCES [dbo].[Users] ([Id])
GO
