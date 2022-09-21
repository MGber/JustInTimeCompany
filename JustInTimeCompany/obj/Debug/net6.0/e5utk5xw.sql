IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Airports] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Latitude] float NOT NULL,
    [Longitude] float NOT NULL,
    CONSTRAINT [PK_Airports] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AspNetRoles] (
    [Id] nvarchar(450) NOT NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AspNetUsers] (
    [Id] nvarchar(450) NOT NULL,
    [FirstName] nvarchar(max) NOT NULL,
    [LastName] nvarchar(max) NOT NULL,
    [Gender] int NOT NULL,
    [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Helicopters] (
    [Id] uniqueidentifier NOT NULL,
    [Model] nvarchar(max) NOT NULL,
    [SeatCount] int NOT NULL,
    [Speed] int NOT NULL,
    [Engine] nvarchar(max) NOT NULL,
    [Status] nvarchar(max) NOT NULL,
    [FlightCount] int NOT NULL,
    CONSTRAINT [PK_Helicopters] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserLogins] (
    [LoginProvider] nvarchar(128) NOT NULL,
    [ProviderKey] nvarchar(128) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserRoles] (
    [UserId] nvarchar(450) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserTokens] (
    [UserId] nvarchar(450) NOT NULL,
    [LoginProvider] nvarchar(128) NOT NULL,
    [Name] nvarchar(128) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [UserMessages] (
    [Id] uniqueidentifier NOT NULL,
    [UserId] nvarchar(450) NULL,
    [Message] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_UserMessages] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_UserMessages_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id])
);
GO

CREATE TABLE [Flights] (
    [Id] uniqueidentifier NOT NULL,
    [FromId] uniqueidentifier NOT NULL,
    [ToId] uniqueidentifier NOT NULL,
    [HelicopterId] uniqueidentifier NOT NULL,
    [PilotId] nvarchar(450) NOT NULL,
    [ScheduledDeparture] datetime2 NOT NULL,
    [RealDeparture] datetime2 NULL,
    [ScheduledArrival] datetime2 NULL,
    [RealArrival] datetime2 NULL,
    [WasLate] bit NOT NULL,
    [DelayReason] nvarchar(max) NULL,
    CONSTRAINT [PK_Flights] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Flights_Airports_FromId] FOREIGN KEY ([FromId]) REFERENCES [Airports] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Flights_Airports_ToId] FOREIGN KEY ([ToId]) REFERENCES [Airports] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Flights_AspNetUsers_PilotId] FOREIGN KEY ([PilotId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Flights_Helicopters_HelicopterId] FOREIGN KEY ([HelicopterId]) REFERENCES [Helicopters] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [FlightLog] (
    [Id] uniqueidentifier NOT NULL,
    [FlightId] uniqueidentifier NOT NULL,
    [Date] datetime2 NOT NULL,
    [OldJson] nvarchar(max) NOT NULL,
    [NewJson] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_FlightLog] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_FlightLog_Flights_FlightId] FOREIGN KEY ([FlightId]) REFERENCES [Flights] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Reservations] (
    [Id] uniqueidentifier NOT NULL,
    [UserId] nvarchar(450) NULL,
    [SeatAmount] int NOT NULL,
    [FlightId] uniqueidentifier NULL,
    CONSTRAINT [PK_Reservations] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Reservations_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]),
    CONSTRAINT [FK_Reservations_Flights_FlightId] FOREIGN KEY ([FlightId]) REFERENCES [Flights] ([Id])
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Latitude', N'Longitude', N'Name') AND [object_id] = OBJECT_ID(N'[Airports]'))
    SET IDENTITY_INSERT [Airports] ON;
INSERT INTO [Airports] ([Id], [Latitude], [Longitude], [Name])
VALUES ('7172b844-ebcb-45c3-94bc-34c14dbd7b4f', 50.898339999999997E0, 4.4823700000000004E0, N'Bruxelles'),
('76391ff6-412f-4c1e-b57f-4ccf86648cff', 50.455998176000001E0, 4.4516648600000002E0, N'Charleroi'),
('ac8aa908-1fa9-4abd-ad51-32353a4c4a00', 50.63583079E0, 5.4393315759999998E0, N'Liège'),
('c2af481a-b71d-473c-8082-e1170b1551de', 51.200400000000002E0, 2.8741699999999999E0, N'Oostende');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Latitude', N'Longitude', N'Name') AND [object_id] = OBJECT_ID(N'[Airports]'))
    SET IDENTITY_INSERT [Airports] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ConcurrencyStamp', N'Name', N'NormalizedName') AND [object_id] = OBJECT_ID(N'[AspNetRoles]'))
    SET IDENTITY_INSERT [AspNetRoles] ON;
INSERT INTO [AspNetRoles] ([Id], [ConcurrencyStamp], [Name], [NormalizedName])
VALUES (N'4635ec8f-2c34-444e-9bb8-fb646e7bd1da', NULL, N'Pilot', N'PILOT'),
(N'4e50f06a-e14c-4657-ae21-5ba760cd3a51', NULL, N'Administrator', N'ADMINISTRATOR'),
(N'570245bc-2723-4392-9213-68202d71dad4', NULL, N'User', N'USER');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ConcurrencyStamp', N'Name', N'NormalizedName') AND [object_id] = OBJECT_ID(N'[AspNetRoles]'))
    SET IDENTITY_INSERT [AspNetRoles] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccessFailedCount', N'ConcurrencyStamp', N'Email', N'EmailConfirmed', N'FirstName', N'Gender', N'LastName', N'LockoutEnabled', N'LockoutEnd', N'NormalizedEmail', N'NormalizedUserName', N'PasswordHash', N'PhoneNumber', N'PhoneNumberConfirmed', N'SecurityStamp', N'TwoFactorEnabled', N'UserName') AND [object_id] = OBJECT_ID(N'[AspNetUsers]'))
    SET IDENTITY_INSERT [AspNetUsers] ON;
INSERT INTO [AspNetUsers] ([Id], [AccessFailedCount], [ConcurrencyStamp], [Email], [EmailConfirmed], [FirstName], [Gender], [LastName], [LockoutEnabled], [LockoutEnd], [NormalizedEmail], [NormalizedUserName], [PasswordHash], [PhoneNumber], [PhoneNumberConfirmed], [SecurityStamp], [TwoFactorEnabled], [UserName])
VALUES (N'319a3971-483c-4ed9-a0cc-2bec19a07b06', 0, N'ccd6a6ca-ed23-485b-a40d-5e32e0020211', N'M.Ney@jitc.com', CAST(1 AS bit), N'Mo', 1, N'Ney', CAST(0 AS bit), NULL, NULL, N'MONEY', N'AQAAAAEAACcQAAAAEDh9Q61uFWiPuIFN/Ii5iNLuXp+IC8E/vkcQCYcPNsGb82C1kIL41cBBhZYzb4WveQ==', NULL, CAST(0 AS bit), N'c5070b7e-970f-443e-a1fd-9dead3642823', CAST(0 AS bit), N'money'),
(N'3ac33a35-d3d6-4a04-a79b-aded19a205db', 0, N'6808c6c2-881e-44bd-806f-0ed6c4bf19c2', N'T.Sabine@jitc.com', CAST(1 AS bit), N'Thierry', 0, N'Sabine', CAST(0 AS bit), NULL, NULL, N'THISAB', N'AQAAAAEAACcQAAAAEJj9o5t9kTav7VC8tFTDYov43JTH0mAFGTbzGYEi4mA2+YUnEom0NBe9Jv5nfEEK6w==', NULL, CAST(0 AS bit), N'dae3e5ad-5650-442e-a6ae-2d360ed07267', CAST(0 AS bit), N'thisab'),
(N'4b40d3d6-bd20-43f2-a5b8-082009c13682', 0, N'c5ddbe69-9e0e-43ba-89d5-cade6888c492', N'c.gaber@jitc.com', CAST(1 AS bit), N'Christian', 0, N'Gaber', CAST(0 AS bit), NULL, NULL, N'CHRGAB', N'AQAAAAEAACcQAAAAEP2PZpq7VGgsq1xM9KVUtesjwzBrRdN0hwhnak7Jv788fKeduYXY7k2pzY4u3lZB5A==', NULL, CAST(0 AS bit), N'661cb6f2-434f-496f-8916-db09ac22d651', CAST(0 AS bit), N'chrgab'),
(N'51d08f0c-0c50-4e47-8e8b-55f7125019bc', 0, N'6d804c13-17d5-4375-a6f9-c7e6d21040b9', N'f.gaber@jitc.com', CAST(1 AS bit), N'Florence', 1, N'Gaber', CAST(0 AS bit), NULL, NULL, N'FLOGAB', N'AQAAAAEAACcQAAAAENVemRP5y37PloG0ltwC5sWiRVjYJDX9DtUr80xn6OCn7HodvpiR0DmFRRF0OxXjZA==', NULL, CAST(0 AS bit), N'28892ef9-c5d7-4087-977c-5c4c5b7d75fa', CAST(0 AS bit), N'flogab'),
(N'821f98b0-ff1e-458a-b15b-a9ea85750e45', 0, N'821d99eb-2a0e-4f59-b020-ce6c47ad515e', N'D.Balav@jitc.com', CAST(1 AS bit), N'Danièle', 1, N'Balav', CAST(0 AS bit), NULL, NULL, N'DANBAL', N'AQAAAAEAACcQAAAAEEhtx6uwEb6Q0we6drGtoEV35gtjtFHxWLZUUOcm5MZmsNnRyioZJ3wgPSMGwdoXcQ==', NULL, CAST(0 AS bit), N'4f5bc9fb-f05f-4284-a76e-c4e41b16ab82', CAST(0 AS bit), N'danbal'),
(N'bb76dfd3-10d3-4224-b4b3-712aafa7ccaa', 0, N'2a098ed6-6532-4200-b5ae-7b68007d908a', N'm.gaber@jitc.com', CAST(1 AS bit), N'Maxime', 2, N'Gaber', CAST(0 AS bit), NULL, NULL, N'MAXGAB', N'AQAAAAEAACcQAAAAEAv8OI+L0vcDi1cdvOyvcHEMlQb1mk5roSitQZCiI0nw5Lv6Ex4May5FgQuPBhdyRg==', NULL, CAST(0 AS bit), N'41d68c2c-e6c2-4cab-ba42-51fd9047f37b', CAST(0 AS bit), N'maxgab'),
(N'efb6c53f-48d0-4981-a3c7-ef714fa2b508', 0, N'830b388c-79e2-4f4a-85d0-b779c4ed860c', N'E.Coptère@jitc.com', CAST(1 AS bit), N'Eli', 0, N'Coptère', CAST(0 AS bit), NULL, NULL, N'ELICOP', N'AQAAAAEAACcQAAAAEN4N7eJqAxByqlieirXpwNcHK0Shg170vQZf1bzbTwOqF83anvLYx8i97776naAgwg==', NULL, CAST(0 AS bit), N'0e2b0c0c-a0d9-4eaf-a6a7-9047ed75d0d8', CAST(0 AS bit), N'elicop');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccessFailedCount', N'ConcurrencyStamp', N'Email', N'EmailConfirmed', N'FirstName', N'Gender', N'LastName', N'LockoutEnabled', N'LockoutEnd', N'NormalizedEmail', N'NormalizedUserName', N'PasswordHash', N'PhoneNumber', N'PhoneNumberConfirmed', N'SecurityStamp', N'TwoFactorEnabled', N'UserName') AND [object_id] = OBJECT_ID(N'[AspNetUsers]'))
    SET IDENTITY_INSERT [AspNetUsers] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Engine', N'FlightCount', N'Model', N'SeatCount', N'Speed', N'Status') AND [object_id] = OBJECT_ID(N'[Helicopters]'))
    SET IDENTITY_INSERT [Helicopters] ON;
INSERT INTO [Helicopters] ([Id], [Engine], [FlightCount], [Model], [SeatCount], [Speed], [Status])
VALUES ('05e1ae09-0a16-4e76-8b2d-a70fcdd1c7e3', N'Une turbine du type Rolls Royce 250-C20B', 5, N'Bell 206 JetRanger', 4, 190, N'WARNING'),
('479e5015-b520-4307-bf25-b75c873f8975', N'Un piston du type Lycoming modèle IO-540', 3, N'Robinson R44 Raven II', 3, 190, N'OK'),
('f7ad5264-3054-4889-9b9a-a1b598e579d7', N'Deux turbines du modèle de Rolls Royce 250-C20F', 6, N'Eurocopter AS 355 F1/F2 Ecureuil III', 6, 220, N'DANGER');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Engine', N'FlightCount', N'Model', N'SeatCount', N'Speed', N'Status') AND [object_id] = OBJECT_ID(N'[Helicopters]'))
    SET IDENTITY_INSERT [Helicopters] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'RoleId', N'UserId') AND [object_id] = OBJECT_ID(N'[AspNetUserRoles]'))
    SET IDENTITY_INSERT [AspNetUserRoles] ON;
INSERT INTO [AspNetUserRoles] ([RoleId], [UserId])
VALUES (N'4e50f06a-e14c-4657-ae21-5ba760cd3a51', N'319a3971-483c-4ed9-a0cc-2bec19a07b06'),
(N'4635ec8f-2c34-444e-9bb8-fb646e7bd1da', N'3ac33a35-d3d6-4a04-a79b-aded19a205db'),
(N'570245bc-2723-4392-9213-68202d71dad4', N'4b40d3d6-bd20-43f2-a5b8-082009c13682'),
(N'570245bc-2723-4392-9213-68202d71dad4', N'51d08f0c-0c50-4e47-8e8b-55f7125019bc'),
(N'4635ec8f-2c34-444e-9bb8-fb646e7bd1da', N'821f98b0-ff1e-458a-b15b-a9ea85750e45'),
(N'570245bc-2723-4392-9213-68202d71dad4', N'bb76dfd3-10d3-4224-b4b3-712aafa7ccaa'),
(N'4635ec8f-2c34-444e-9bb8-fb646e7bd1da', N'efb6c53f-48d0-4981-a3c7-ef714fa2b508');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'RoleId', N'UserId') AND [object_id] = OBJECT_ID(N'[AspNetUserRoles]'))
    SET IDENTITY_INSERT [AspNetUserRoles] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'DelayReason', N'FromId', N'HelicopterId', N'PilotId', N'RealArrival', N'RealDeparture', N'ScheduledArrival', N'ScheduledDeparture', N'ToId', N'WasLate') AND [object_id] = OBJECT_ID(N'[Flights]'))
    SET IDENTITY_INSERT [Flights] ON;
INSERT INTO [Flights] ([Id], [DelayReason], [FromId], [HelicopterId], [PilotId], [RealArrival], [RealDeparture], [ScheduledArrival], [ScheduledDeparture], [ToId], [WasLate])
VALUES ('138d28a4-5cb2-4792-8aaa-2357310cfcc4', N'The pilot again overslept.', '76391ff6-412f-4c1e-b57f-4ccf86648cff', '05e1ae09-0a16-4e76-8b2d-a70fcdd1c7e3', N'3ac33a35-d3d6-4a04-a79b-aded19a205db', '2022-08-02T20:29:00.0000000', '2022-08-02T18:29:00.0000000', '2022-08-02T20:23:00.0000000', '2022-08-02T18:23:00.0000000', '7172b844-ebcb-45c3-94bc-34c14dbd7b4f', CAST(1 AS bit)),
('44198f59-813b-4a96-a930-84f7b77a4eb6', NULL, 'c2af481a-b71d-473c-8082-e1170b1551de', 'f7ad5264-3054-4889-9b9a-a1b598e579d7', N'821f98b0-ff1e-458a-b15b-a9ea85750e45', NULL, NULL, '2022-08-11T20:23:00.0000000', '2022-08-11T18:23:00.0000000', 'ac8aa908-1fa9-4abd-ad51-32353a4c4a00', CAST(0 AS bit)),
('6809c4c1-3714-4037-8d3d-cf06b3b6555c', N'The pilot overslept.', 'ac8aa908-1fa9-4abd-ad51-32353a4c4a00', 'f7ad5264-3054-4889-9b9a-a1b598e579d7', N'821f98b0-ff1e-458a-b15b-a9ea85750e45', '2022-08-01T20:29:00.0000000', '2022-08-01T18:29:00.0000000', '2022-08-01T20:23:00.0000000', '2022-08-01T18:23:00.0000000', '76391ff6-412f-4c1e-b57f-4ccf86648cff', CAST(1 AS bit)),
('97550023-4dd5-425b-8285-420b3bd52ad1', NULL, '7172b844-ebcb-45c3-94bc-34c14dbd7b4f', '479e5015-b520-4307-bf25-b75c873f8975', N'efb6c53f-48d0-4981-a3c7-ef714fa2b508', NULL, NULL, '2022-08-10T20:23:00.0000000', '2022-08-10T18:23:00.0000000', 'c2af481a-b71d-473c-8082-e1170b1551de', CAST(0 AS bit)),
('c7bdc7a7-8a10-46e2-8058-3f9b645662c6', NULL, 'ac8aa908-1fa9-4abd-ad51-32353a4c4a00', '05e1ae09-0a16-4e76-8b2d-a70fcdd1c7e3', N'3ac33a35-d3d6-4a04-a79b-aded19a205db', NULL, NULL, '2022-08-12T20:23:00.0000000', '2022-08-12T18:23:00.0000000', 'c2af481a-b71d-473c-8082-e1170b1551de', CAST(0 AS bit));
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'DelayReason', N'FromId', N'HelicopterId', N'PilotId', N'RealArrival', N'RealDeparture', N'ScheduledArrival', N'ScheduledDeparture', N'ToId', N'WasLate') AND [object_id] = OBJECT_ID(N'[Flights]'))
    SET IDENTITY_INSERT [Flights] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Message', N'UserId') AND [object_id] = OBJECT_ID(N'[UserMessages]'))
    SET IDENTITY_INSERT [UserMessages] ON;
INSERT INTO [UserMessages] ([Id], [Message], [UserId])
VALUES ('2e27db70-be58-4c64-9c85-17320d79d1ce', N'This is a testMessage3', N'4b40d3d6-bd20-43f2-a5b8-082009c13682'),
('2fba385c-5aac-4d98-8b2e-b3c3c23b6acc', N'This is a testMessage2', N'51d08f0c-0c50-4e47-8e8b-55f7125019bc'),
('46314476-0cbd-4535-b257-1ab51b582ec2', N'This is a testMessage5', N'51d08f0c-0c50-4e47-8e8b-55f7125019bc'),
('59b2ecaa-b258-4e87-9d0c-33d87c17cfd9', N'This is a testMessage', N'bb76dfd3-10d3-4224-b4b3-712aafa7ccaa'),
('d4812a5f-54dc-4ab6-8e0a-7018744f2722', N'This is a testMessage6', N'4b40d3d6-bd20-43f2-a5b8-082009c13682'),
('d64d8d14-2e2f-4044-995e-3c28a66b1d0e', N'This is a testMessage4', N'bb76dfd3-10d3-4224-b4b3-712aafa7ccaa');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Message', N'UserId') AND [object_id] = OBJECT_ID(N'[UserMessages]'))
    SET IDENTITY_INSERT [UserMessages] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'FlightId', N'SeatAmount', N'UserId') AND [object_id] = OBJECT_ID(N'[Reservations]'))
    SET IDENTITY_INSERT [Reservations] ON;
INSERT INTO [Reservations] ([Id], [FlightId], [SeatAmount], [UserId])
VALUES ('5b36ccee-3b31-49cb-ab02-3e021779dac4', '44198f59-813b-4a96-a930-84f7b77a4eb6', 1, N'51d08f0c-0c50-4e47-8e8b-55f7125019bc'),
('6ae64930-1ba7-4933-96cf-d89fdd4353bd', '44198f59-813b-4a96-a930-84f7b77a4eb6', 3, N'bb76dfd3-10d3-4224-b4b3-712aafa7ccaa'),
('79a77425-9216-40c3-b6c9-39d1dc097909', '6809c4c1-3714-4037-8d3d-cf06b3b6555c', 5, N'bb76dfd3-10d3-4224-b4b3-712aafa7ccaa'),
('bb207b88-d785-4e2b-820d-0c68f27739f5', '138d28a4-5cb2-4792-8aaa-2357310cfcc4', 3, N'51d08f0c-0c50-4e47-8e8b-55f7125019bc'),
('d58210ef-c2b6-4413-934c-2ae16eab260e', '97550023-4dd5-425b-8285-420b3bd52ad1', 3, N'4b40d3d6-bd20-43f2-a5b8-082009c13682');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'FlightId', N'SeatAmount', N'UserId') AND [object_id] = OBJECT_ID(N'[Reservations]'))
    SET IDENTITY_INSERT [Reservations] OFF;
GO

CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
GO

CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
GO

CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
GO

CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
GO

CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
GO

CREATE INDEX [IX_FlightLog_FlightId] ON [FlightLog] ([FlightId]);
GO

CREATE INDEX [IX_Flights_FromId] ON [Flights] ([FromId]);
GO

CREATE INDEX [IX_Flights_HelicopterId] ON [Flights] ([HelicopterId]);
GO

CREATE INDEX [IX_Flights_PilotId] ON [Flights] ([PilotId]);
GO

CREATE INDEX [IX_Flights_ToId] ON [Flights] ([ToId]);
GO

CREATE INDEX [IX_Reservations_FlightId] ON [Reservations] ([FlightId]);
GO

CREATE INDEX [IX_Reservations_UserId] ON [Reservations] ([UserId]);
GO

CREATE INDEX [IX_UserMessages_UserId] ON [UserMessages] ([UserId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220805121420_init', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'c67635db-5fe0-4dc9-a541-83a29c75c08b', [PasswordHash] = N'AQAAAAEAACcQAAAAEMUR2gpDgnyOpH2iNDeVX2H/5/MBEagR805PGoLuLHu5Bt0TpDvprQa1IB0rZFpOuA==', [SecurityStamp] = N'8acc1ab1-5c5f-433c-91fe-72bceaf25962'
WHERE [Id] = N'319a3971-483c-4ed9-a0cc-2bec19a07b06';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'4a2fe6a2-c4aa-48f6-9189-444bb2163c98', [PasswordHash] = N'AQAAAAEAACcQAAAAEH6uK1ofa6oOHE93YtEVagCNQNsTsvP88trmCS0rwKDshxpD/mz7jUHlnDDMnzeLgA==', [SecurityStamp] = N'd007a34e-d070-4a4d-a96c-e6cff53cc7ec'
WHERE [Id] = N'3ac33a35-d3d6-4a04-a79b-aded19a205db';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'31d42741-a33e-4167-a307-7a7002371e36', [PasswordHash] = N'AQAAAAEAACcQAAAAELKJT/nSsskhtMATaRjSOjtKBZVD7MMc913LLVSAS0UN//mtxHY/xz7zuI+7br0srw==', [SecurityStamp] = N'56a8beac-f8e5-4042-980a-0a0ea555c276'
WHERE [Id] = N'4b40d3d6-bd20-43f2-a5b8-082009c13682';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'17c5742a-f6eb-4868-8ea9-913df64f11cc', [PasswordHash] = N'AQAAAAEAACcQAAAAEIr9Ak4TUPmiENghfrySE47V7D3e0gR8NzrY+/nxMsueE946WXmBesj7jOzAwKodMg==', [SecurityStamp] = N'541533c3-05a6-4129-bc17-087f652c6f3b'
WHERE [Id] = N'51d08f0c-0c50-4e47-8e8b-55f7125019bc';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'a2d1d1e9-596f-4007-b0ae-a2e2afd2e921', [PasswordHash] = N'AQAAAAEAACcQAAAAEFB5EFJai/Fv9XNlW0jJYFapeLZ+7QoPEYLcuxw9wbGfP7hQ11gCc028VmfgmzpoWA==', [SecurityStamp] = N'd8a128df-365b-4669-9f07-7b7fdd894132'
WHERE [Id] = N'821f98b0-ff1e-458a-b15b-a9ea85750e45';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'b7a77c91-dedc-432c-8a54-c2f28010814d', [PasswordHash] = N'AQAAAAEAACcQAAAAEE6yUWIMK2hm6Sw651J/44DbjsgMufrpkvWAv59Bc86LEmhq2RxU/J14N4SHWz1Vpw==', [SecurityStamp] = N'6cc88c4d-f17d-4202-a3e5-3ac2afd67560'
WHERE [Id] = N'bb76dfd3-10d3-4224-b4b3-712aafa7ccaa';
SELECT @@ROWCOUNT;

GO

UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N'e47b2f78-59d6-4085-a587-0abf906c6205', [PasswordHash] = N'AQAAAAEAACcQAAAAENp8wFk/2GlLW0YpqaT4dCnXweO07Z6Ih3Whm2doL5Fo3CEK3xUSiF0glZMKxsBLlw==', [SecurityStamp] = N'd3645e5e-7ba9-47ae-937d-61f901f02e2d'
WHERE [Id] = N'efb6c53f-48d0-4981-a3c7-ef714fa2b508';
SELECT @@ROWCOUNT;

GO

UPDATE [Flights] SET [ScheduledArrival] = '2022-09-11T20:23:00.0000000', [ScheduledDeparture] = '2022-09-11T18:23:00.0000000'
WHERE [Id] = '44198f59-813b-4a96-a930-84f7b77a4eb6';
SELECT @@ROWCOUNT;

GO

UPDATE [Flights] SET [ScheduledArrival] = '2022-09-10T20:23:00.0000000', [ScheduledDeparture] = '2022-09-10T18:23:00.0000000'
WHERE [Id] = '97550023-4dd5-425b-8285-420b3bd52ad1';
SELECT @@ROWCOUNT;

GO

UPDATE [Flights] SET [ScheduledArrival] = '2022-09-12T20:23:00.0000000', [ScheduledDeparture] = '2022-09-12T18:23:00.0000000'
WHERE [Id] = 'c7bdc7a7-8a10-46e2-8058-3f9b645662c6';
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220807121716_modifiedseed', N'6.0.7');
GO

COMMIT;
GO

