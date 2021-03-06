USE [MovieDB]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Genre](
	[GenreID] [int] IDENTITY(1,1) NOT NULL,
	[GenreName] [varchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[GenreID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Movie](
	[MovieID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](100) NOT NULL,
	[Overview] [varchar](1024) NOT NULL,
	[Genre] [varchar](20) NOT NULL,
	[Language] [varchar](20) NOT NULL,
	[Duration] [int] NOT NULL,
	[Rating] [decimal](2, 1) NULL,
	[PosterPath] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[MovieID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserMaster](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](20) NOT NULL,
	[LastName] [varchar](20) NOT NULL,
	[Username] [varchar](20) NOT NULL,
	[Password] [varchar](40) NOT NULL,
	[Gender] [varchar](6) NOT NULL,
	[UserTypeName] [varchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserType](
	[UserTypeID] [int] IDENTITY(1,1) NOT NULL,
	[UserTypeName] [varchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Watchlist](
	[WatchlistId] [varchar](36) NOT NULL,
	[UserID] [int] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[WatchlistId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WatchlistItems](
	[WatchlistItemId] [int] IDENTITY(1,1) NOT NULL,
	[WatchlistId] [varchar](36) NOT NULL,
	[MovieId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[WatchlistItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Genre] ON 

INSERT [dbo].[Genre] ([GenreID], [GenreName]) VALUES (1, N'Action')
INSERT [dbo].[Genre] ([GenreID], [GenreName]) VALUES (2, N'Animation')
INSERT [dbo].[Genre] ([GenreID], [GenreName]) VALUES (3, N'Comedy')
INSERT [dbo].[Genre] ([GenreID], [GenreName]) VALUES (4, N'Drama')
INSERT [dbo].[Genre] ([GenreID], [GenreName]) VALUES (5, N'Mystery')
INSERT [dbo].[Genre] ([GenreID], [GenreName]) VALUES (6, N'Science Fiction')
SET IDENTITY_INSERT [dbo].[Genre] OFF
GO
SET IDENTITY_INSERT [dbo].[Movie] ON 

INSERT [dbo].[Movie] ([MovieID], [Title], [Overview], [Genre], [Language], [Duration], [Rating], [PosterPath]) VALUES (1, N'Cosmoball', N'Cosmoball is a mesmerizing intergalactic game of future played between humans and aliens at the giant extraterrestrial ship hovering in the sky over Earth. A young man with enormous power of an unknown nature joins the team of hot-headed superheroes in exchange for a cure for his mother’s deadly illness. The Four from Earth will fight not only to defend the honor of their home planet in the spectacular game, but to face the unprecedented threat to the Galaxy and embrace their own destiny.', N'Action', N'Russian', 123, CAST(4.8 AS Decimal(2, 1)), N'72d23bc9-4e58-42cf-9b93-8acd4eebe5b9.jpg')
INSERT [dbo].[Movie] ([MovieID], [Title], [Overview], [Genre], [Language], [Duration], [Rating], [PosterPath]) VALUES (2, N'Fatman', N'A rowdy, unorthodox Santa Claus is fighting to save his declining business. Meanwhile, Billy, a neglected and precocious 12 year old, hires a hit man to kill Santa after receiving a lump of coal in his stocking.', N'Animation', N'French', 90, CAST(5.7 AS Decimal(2, 1)), N'a87ac02e-9d9c-46c7-8097-b37d4418e8ce.jpg')
INSERT [dbo].[Movie] ([MovieID], [Title], [Overview], [Genre], [Language], [Duration], [Rating], [PosterPath]) VALUES (3, N'Soul', N'Joe Gardner is a middle school teacher with a love for jazz music. After a successful gig at the Half Note Club, he suddenly gets into an accident that separates his soul from his body and is transported to the You Seminar, a center in which souls develop and gain passions before being transported to a newborn child. Joe must enlist help from the other souls-in-training, like 22, a soul who has spent eons in the You Seminar, in order to get back to Earth.', N'Comedy', N'English', 156, CAST(8.4 AS Decimal(2, 1)), N'8b1c11fe-da6b-4b4f-a2d1-eee115585e89.jpg')
INSERT [dbo].[Movie] ([MovieID], [Title], [Overview], [Genre], [Language], [Duration], [Rating], [PosterPath]) VALUES (4, N'Tenet', N'Armed with only one word - Tenet - and fighting for the survival of the entire world, the Protagonist journeys through a twilight world of international espionage on a mission that will unfold in something beyond real time.', N'Mystery', N'English', 150, CAST(7.3 AS Decimal(2, 1)), N'75457f0f-8bdc-414b-81e9-7fa5d37d13a4.jpg')
INSERT [dbo].[Movie] ([MovieID], [Title], [Overview], [Genre], [Language], [Duration], [Rating], [PosterPath]) VALUES (5, N'The Croods: A New Age', N'Searching for a safer habitat, the prehistoric Crood family discovers an idyllic, walled-in paradise that meets all of its needs. Unfortunately, they must also learn to live with the Bettermans -- a family that''s a couple of steps above the Croods on the evolutionary ladder. As tensions between the new neighbors start to rise, a new threat soon propels both clans on an epic adventure that forces them to embrace their differences, draw strength from one another, and survive together.', N'Animation', N'English', 95, CAST(7.8 AS Decimal(2, 1)), N'ac780ac1-081d-4d78-8bfa-c358a0a97a26.jpg')
INSERT [dbo].[Movie] ([MovieID], [Title], [Overview], [Genre], [Language], [Duration], [Rating], [PosterPath]) VALUES (6, N'WW84', N'Wonder Woman comes into conflict with the Soviet Union during the Cold War in the 1980s and finds a formidable foe by the name of the Cheetah.', N'Action', N'English', 152, CAST(7.2 AS Decimal(2, 1)), N'8c93580c-1e5c-45c0-a1b7-84cb706bc367.jpg')
INSERT [dbo].[Movie] ([MovieID], [Title], [Overview], [Genre], [Language], [Duration], [Rating], [PosterPath]) VALUES (7, N'Wander', N'After getting hired to probe a suspicious death in the small town of Wander, a mentally unstable private investigator becomes convinced the case is linked to the same ''conspiracy cover up'' that caused the death of his daughter.', N'Drama', N'English', 95, CAST(5.2 AS Decimal(2, 1)), N'd1d3885d-8970-49e9-9048-5c6b0e178b6c.jpg')
INSERT [dbo].[Movie] ([MovieID], [Title], [Overview], [Genre], [Language], [Duration], [Rating], [PosterPath]) VALUES (10, N'The Doorman', N'A former Marine turned doorman at a luxury New York City high-rise must outsmart and battle a group of art thieves and their ruthless leader — while struggling to protect her sister''s family. As the thieves become increasingly desperate and violent, the doorman calls upon her deadly fighting skills to end the showdown.', N'Action', N'English', 97, CAST(6.0 AS Decimal(2, 1)), N'ef36ce15-f73f-4485-8e6e-8c010d5a7c58.jpg')
INSERT [dbo].[Movie] ([MovieID], [Title], [Overview], [Genre], [Language], [Duration], [Rating], [PosterPath]) VALUES (11, N'The Midnight Sky', N'A lone scientist in the Arctic races to contact a crew of astronauts returning home to a mysterious global catastrophe.', N'Science Fiction', N'English', 118, CAST(5.6 AS Decimal(2, 1)), N'9ef9d5a6-3ebb-4bf5-8be6-5c225644e39a.jpg')
INSERT [dbo].[Movie] ([MovieID], [Title], [Overview], [Genre], [Language], [Duration], [Rating], [PosterPath]) VALUES (12, N'Jiu Jitsu', N'Every six years, an ancient order of jiu-jitsu fighters joins forces to battle a vicious race of alien invaders. But when a celebrated war hero goes down in defeat, the fate of the planet and mankind hangs in the balance.', N'Science Fiction', N'English', 102, CAST(4.9 AS Decimal(2, 1)), N'0de54a0b-6fa5-4a50-b10c-3e8ca36581d9.jpg')
INSERT [dbo].[Movie] ([MovieID], [Title], [Overview], [Genre], [Language], [Duration], [Rating], [PosterPath]) VALUES (13, N'The Girl on the Train', N'Rachel Watson, devastated by her recent divorce, spends her daily commute fantasizing about the seemingly perfect couple who live in a house that her train passes every day, until one morning she sees something shocking happen there and becomes entangled in the mystery that unfolds.', N'Mystery', N'English', 112, CAST(6.4 AS Decimal(2, 1)), N'130361e0-669f-4f25-b608-6613c51e33b6.jpg')
INSERT [dbo].[Movie] ([MovieID], [Title], [Overview], [Genre], [Language], [Duration], [Rating], [PosterPath]) VALUES (14, N'Despicable Me ', N'Villainous Gru lives up to his reputation as a despicable, deplorable and downright unlikable guy when he hatches a plan to steal the moon from the sky. But he has a tough time staying on task after three orphans land in his care.', N'Comedy', N'English', 120, CAST(7.0 AS Decimal(2, 1)), N'469a72b1-02bf-496d-8e0c-86166f1b2584.jpg')
SET IDENTITY_INSERT [dbo].[Movie] OFF
GO
SET IDENTITY_INSERT [dbo].[UserMaster] ON 

INSERT [dbo].[UserMaster] ([UserID], [FirstName], [LastName], [Username], [Password], [Gender], [UserTypeName]) VALUES (1, N'Admin', N'sharma', N'admin', N'Aa123456', N'Male', N'Admin')
INSERT [dbo].[UserMaster] ([UserID], [FirstName], [LastName], [Username], [Password], [Gender], [UserTypeName]) VALUES (2, N'ankit', N'sharma', N'ankit', N'Aa123456', N'Male', N'User')
SET IDENTITY_INSERT [dbo].[UserMaster] OFF
GO
SET IDENTITY_INSERT [dbo].[UserType] ON 

INSERT [dbo].[UserType] ([UserTypeID], [UserTypeName]) VALUES (1, N'Admin')
INSERT [dbo].[UserType] ([UserTypeID], [UserTypeName]) VALUES (2, N'User')
SET IDENTITY_INSERT [dbo].[UserType] OFF
GO

