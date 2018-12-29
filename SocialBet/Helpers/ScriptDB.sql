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

INSERT INTO [dbo].[Users] VALUES ('1', 'Sotiris', 'Giagkiozis', 'Kabamaru13', 'ce7166ab0e395131b20eba011e8dcbef07c0cf95b8939932203a9a7fe407cc674e643f28e75859e24c67403aba6781d62a4392ff8dd1994924606a92d153a105', '87709df0c1f18f4e8da9ffd6157a28c4adedd2042dfd08771aabdfb1afdebe589f2b20b5d9cecdaaf6e74801b0374a172ef4124809998a4df975e385366c021970b5214a0aa0171c668aeeb4d8bf2a47af711ca577a807a15efb6dac7e5d25e468fd345a2658a7b7c1125cf7f43e4848023af0ad0c95ad81a222d1be7d1ae4a9')
INSERT INTO [dbo].[Users] VALUES ('2', 'Eirini', 'Papamichail', 'Julia', 'b77720228848dfde4b11f189e0cbabcf08f07412b44846c52609fc861cf1261509c79100eb0c3fd8f21a2ec7d31b065d0136d7dea8d4d6f5081f05f3740341cb', 'bc9e5b58e9c58deb068e790db773fac875762ea68f47001390e76a6304b68ca060cda801e07eb335b752791bfe653ed22439ab4f142aa73993d94e8db8051e70cd3863e73c7ab204b875b27fe0ea58943c82679947dd85ee9ed079c8327218eb4acc0a1e8b9b43b97ebb9b9fef8185f0d36c2d53dee9e115a09039411421dfe6')
INSERT INTO [dbo].[Users] VALUES ('3', 'Aggelos', 'Papamichail', 'Linos', '32755a7f2f4072c79a54a6899f9ee26600269bca3a0642151047c1bdf2e031847626b9449a2aa20466e2f5d260a4866a4f2c4a71de142eda6860115aefc09a84', '6e93c5f5e04e726085b5dcee765d3d849808d787dd9d628af495b336693f11604218d55d5aa145fe3c2190320651c2b26571d0d15710aa1a1a5f28b5b336ed193549200999237f0900d9c9aa3183c91bf6be7832839f4a7616abe9079ad281a43b90c37557f4a16f543bf6ba496da8925d3576945d179d86225b53018e794a2a')
INSERT INTO [dbo].[Users] VALUES ('1337', 'aggelos', 'papam', 'wiosif', 'ca222301c72d6ef2fa8e82f5fff2ffa0d01315bf38d480626bf78833a889eaebff0f28e92229d897be38550bb900e2abb192d82fe20a880f1d3a69fe6f1ee555', '386375974adbe90cb6ea5fc7a092311efd964f8bfa56a06e0f2a6b6bfea48df0bff69d76358171a8a81449693ba5b7c094c8c4c64be4eb55f0a35ea0ac508c15185047ee9964a4e3ab916f5a1594b3281ee9a70945d6a5b71501a0cbd9b0df32a4abd1967041aa95b9f0deaed704d24703d595baa91b59dfac97d1f138445864')

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

insert into dbo.UserStats values ('1', 0, 0, 0, 0, 0, 0, 0, 1, 0, 'Newbie')
insert into dbo.UserStats values ('2', 0, 0, 0, 0, 0, 0, 0, 1, 0, 'Newbie')
insert into dbo.UserStats values ('3', 0, 0, 0, 0, 0, 0, 0, 1, 0, 'Newbie')
insert into dbo.UserStats values ('1337', 0, 0, 0, 0, 0, 0, 0, 1, 0, 'Newbie')

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
