
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/31/2015 01:13:13
-- Generated from EDMX file: D:\finalTask\Itra\WebSite\WebApplication\WebApplication\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [aspnet-WebApplication-20151012042100];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


IF OBJECT_ID(N'[dbo].[FK_AspNetUserDemotivator]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Demotivators] DROP CONSTRAINT [FK_AspNetUserDemotivator];
GO
IF OBJECT_ID(N'[dbo].[FK_DemotivatorRatingDemotivator]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DemotivatorRatings] DROP CONSTRAINT [FK_DemotivatorRatingDemotivator];
GO
IF OBJECT_ID(N'[dbo].[FK_DemotivatorRatingAspNetUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DemotivatorRatings] DROP CONSTRAINT [FK_DemotivatorRatingAspNetUser];
GO
IF OBJECT_ID(N'[dbo].[FK_CommentAspNetUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comments] DROP CONSTRAINT [FK_CommentAspNetUser];
GO
IF OBJECT_ID(N'[dbo].[FK_CommentDemotivator]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comments] DROP CONSTRAINT [FK_CommentDemotivator];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

GO
IF OBJECT_ID(N'[dbo].[Demotivators]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Demotivators];
GO
IF OBJECT_ID(N'[dbo].[DemotivatorRatings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DemotivatorRatings];
GO
IF OBJECT_ID(N'[dbo].[Comments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Comments];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserRoles];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'C__MigrationHistory'
CREATE TABLE [dbo].[C__MigrationHistory] (
    [MigrationId] nvarchar(150)  NOT NULL,
    [ContextKey] nvarchar(300)  NOT NULL,
    [Model] varbinary(max)  NOT NULL,
    [ProductVersion] nvarchar(32)  NOT NULL
);
GO

-- Creating table 'AspNetRoles'
CREATE TABLE [dbo].[AspNetRoles] (
    [Id] nvarchar(128)  NOT NULL,
    [Name] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'AspNetUserClaims'
CREATE TABLE [dbo].[AspNetUserClaims] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] nvarchar(128)  NOT NULL,
    [ClaimType] nvarchar(max)  NULL,
    [ClaimValue] nvarchar(max)  NULL
);
GO

-- Creating table 'AspNetUserLogins'
CREATE TABLE [dbo].[AspNetUserLogins] (
    [LoginProvider] nvarchar(128)  NOT NULL,
    [ProviderKey] nvarchar(128)  NOT NULL,
    [UserId] nvarchar(128)  NOT NULL
);
GO

-- Creating table 'AspNetUsers'
CREATE TABLE [dbo].[AspNetUsers] (
    [Id] nvarchar(128)  NOT NULL,
    [Email] nvarchar(256)  NULL,
    [EmailConfirmed] bit  NOT NULL,
    [PasswordHash] nvarchar(max)  NULL,
    [SecurityStamp] nvarchar(max)  NULL,
    [PhoneNumber] nvarchar(max)  NULL,
    [PhoneNumberConfirmed] bit  NOT NULL,
    [TwoFactorEnabled] bit  NOT NULL,
    [LockoutEndDateUtc] datetime  NULL,
    [LockoutEnabled] bit  NOT NULL,
    [AccessFailedCount] int  NOT NULL,
    [UserName] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'Demotivators'
CREATE TABLE [dbo].[Demotivators] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DemotivatorName] nvarchar(max)  NOT NULL,
    [Rate] nvarchar(max)  NOT NULL,
    [Date] datetime  NOT NULL,
    [CreatorName] nvarchar(max)  NOT NULL,
    [DemotivatorUrl] nvarchar(max)  NOT NULL,
    [OriginalImageUrl] nvarchar(max)  NOT NULL,
    [TopLine] nvarchar(max)  NOT NULL,
    [BottomLine] nvarchar(max)  NOT NULL,
    [AspNetUserId] nvarchar(128)  NOT NULL
);
GO

-- Creating table 'DemotivatorRatings'
CREATE TABLE [dbo].[DemotivatorRatings] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Mark] int  NOT NULL,
    [Demotivator_Id] int  NOT NULL,
    [AspNetUser_Id] nvarchar(128)  NOT NULL
);
GO

-- Creating table 'Comments'
CREATE TABLE [dbo].[Comments] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PublicationDate] datetime  NOT NULL,
    [CommentText] nvarchar(max)  NOT NULL,
    [AspNetUser_Id] nvarchar(128)  NOT NULL,
    [Demotivator_Id] int  NOT NULL
);
GO

-- Creating table 'AspNetUserRoles'
CREATE TABLE [dbo].[AspNetUserRoles] (
    [AspNetRoles_Id] nvarchar(128)  NOT NULL,
    [AspNetUsers_Id] nvarchar(128)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [MigrationId], [ContextKey] in table 'C__MigrationHistory'
ALTER TABLE [dbo].[C__MigrationHistory]
ADD CONSTRAINT [PK_C__MigrationHistory]
    PRIMARY KEY CLUSTERED ([MigrationId], [ContextKey] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetRoles'
ALTER TABLE [dbo].[AspNetRoles]
ADD CONSTRAINT [PK_AspNetRoles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetUserClaims'
ALTER TABLE [dbo].[AspNetUserClaims]
ADD CONSTRAINT [PK_AspNetUserClaims]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [LoginProvider], [ProviderKey], [UserId] in table 'AspNetUserLogins'
ALTER TABLE [dbo].[AspNetUserLogins]
ADD CONSTRAINT [PK_AspNetUserLogins]
    PRIMARY KEY CLUSTERED ([LoginProvider], [ProviderKey], [UserId] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetUsers'
ALTER TABLE [dbo].[AspNetUsers]
ADD CONSTRAINT [PK_AspNetUsers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Demotivators'
ALTER TABLE [dbo].[Demotivators]
ADD CONSTRAINT [PK_Demotivators]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DemotivatorRatings'
ALTER TABLE [dbo].[DemotivatorRatings]
ADD CONSTRAINT [PK_DemotivatorRatings]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [PK_Comments]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [AspNetRoles_Id], [AspNetUsers_Id] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [PK_AspNetUserRoles]
    PRIMARY KEY CLUSTERED ([AspNetRoles_Id], [AspNetUsers_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UserId] in table 'AspNetUserClaims'
ALTER TABLE [dbo].[AspNetUserClaims]
ADD CONSTRAINT [FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId'
CREATE INDEX [IX_FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]
ON [dbo].[AspNetUserClaims]
    ([UserId]);
GO

-- Creating foreign key on [UserId] in table 'AspNetUserLogins'
ALTER TABLE [dbo].[AspNetUserLogins]
ADD CONSTRAINT [FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId'
CREATE INDEX [IX_FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]
ON [dbo].[AspNetUserLogins]
    ([UserId]);
GO

-- Creating foreign key on [AspNetRoles_Id] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [FK_AspNetUserRoles_AspNetRoles]
    FOREIGN KEY ([AspNetRoles_Id])
    REFERENCES [dbo].[AspNetRoles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [AspNetUsers_Id] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [FK_AspNetUserRoles_AspNetUsers]
    FOREIGN KEY ([AspNetUsers_Id])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AspNetUserRoles_AspNetUsers'
CREATE INDEX [IX_FK_AspNetUserRoles_AspNetUsers]
ON [dbo].[AspNetUserRoles]
    ([AspNetUsers_Id]);
GO

-- Creating foreign key on [AspNetUserId] in table 'Demotivators'
ALTER TABLE [dbo].[Demotivators]
ADD CONSTRAINT [FK_AspNetUserDemotivator]
    FOREIGN KEY ([AspNetUserId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AspNetUserDemotivator'
CREATE INDEX [IX_FK_AspNetUserDemotivator]
ON [dbo].[Demotivators]
    ([AspNetUserId]);
GO

-- Creating foreign key on [Demotivator_Id] in table 'DemotivatorRatings'
ALTER TABLE [dbo].[DemotivatorRatings]
ADD CONSTRAINT [FK_DemotivatorRatingDemotivator]
    FOREIGN KEY ([Demotivator_Id])
    REFERENCES [dbo].[Demotivators]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DemotivatorRatingDemotivator'
CREATE INDEX [IX_FK_DemotivatorRatingDemotivator]
ON [dbo].[DemotivatorRatings]
    ([Demotivator_Id]);
GO

-- Creating foreign key on [AspNetUser_Id] in table 'DemotivatorRatings'
ALTER TABLE [dbo].[DemotivatorRatings]
ADD CONSTRAINT [FK_DemotivatorRatingAspNetUser]
    FOREIGN KEY ([AspNetUser_Id])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DemotivatorRatingAspNetUser'
CREATE INDEX [IX_FK_DemotivatorRatingAspNetUser]
ON [dbo].[DemotivatorRatings]
    ([AspNetUser_Id]);
GO

-- Creating foreign key on [AspNetUser_Id] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [FK_CommentAspNetUser]
    FOREIGN KEY ([AspNetUser_Id])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CommentAspNetUser'
CREATE INDEX [IX_FK_CommentAspNetUser]
ON [dbo].[Comments]
    ([AspNetUser_Id]);
GO

-- Creating foreign key on [Demotivator_Id] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [FK_CommentDemotivator]
    FOREIGN KEY ([Demotivator_Id])
    REFERENCES [dbo].[Demotivators]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CommentDemotivator'
CREATE INDEX [IX_FK_CommentDemotivator]
ON [dbo].[Comments]
    ([Demotivator_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------