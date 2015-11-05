
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/05/2015 16:40:17
-- Generated from EDMX file: C:\Users\George\Dropbox\Itransition\Itra\WebApplication1\WebApplication1\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [aspnet-WebApplication1-20151103122626];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


IF OBJECT_ID(N'[dbo].[FK_DemotivatorRateDemotivator]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DemotivatorRates] DROP CONSTRAINT [FK_DemotivatorRateDemotivator];
GO
IF OBJECT_ID(N'[dbo].[FK_AspNetUserDemotivator]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Demotivators] DROP CONSTRAINT [FK_AspNetUserDemotivator];
GO
IF OBJECT_ID(N'[dbo].[FK_DemotivatorComment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comments] DROP CONSTRAINT [FK_DemotivatorComment];
GO
IF OBJECT_ID(N'[dbo].[FK_AspNetUserComment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comments] DROP CONSTRAINT [FK_AspNetUserComment];
GO
IF OBJECT_ID(N'[dbo].[FK_AspNetUserDemotivatorRate]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DemotivatorRates] DROP CONSTRAINT [FK_AspNetUserDemotivatorRate];
GO
IF OBJECT_ID(N'[dbo].[FK_CommentLike]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Likes] DROP CONSTRAINT [FK_CommentLike];
GO
IF OBJECT_ID(N'[dbo].[FK_AspNetUserLike]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Likes] DROP CONSTRAINT [FK_AspNetUserLike];
GO
IF OBJECT_ID(N'[dbo].[FK_tag_to_demDemotivator]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tag_to_dem] DROP CONSTRAINT [FK_tag_to_demDemotivator];
GO
IF OBJECT_ID(N'[dbo].[FK_tag_to_demtag]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tag_to_dem] DROP CONSTRAINT [FK_tag_to_demtag];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


IF OBJECT_ID(N'[dbo].[DemotivatorRates]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DemotivatorRates];
GO
IF OBJECT_ID(N'[dbo].[Demotivators]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Demotivators];
GO
IF OBJECT_ID(N'[dbo].[Comments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Comments];
GO
IF OBJECT_ID(N'[dbo].[Likes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Likes];
GO
IF OBJECT_ID(N'[dbo].[tag_to_dem]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tag_to_dem];
GO
IF OBJECT_ID(N'[dbo].[tags]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tags];
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

-- Creating table 'DemotivatorRates'
CREATE TABLE [dbo].[DemotivatorRates] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Mark] nvarchar(max)  NOT NULL,
    [DemotivatorId] int  NOT NULL,
    [AspNetUserId] nvarchar(128)  NOT NULL
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
    [AspNetUserId] nvarchar(128)  NOT NULL,
    [CommentId] int  NOT NULL
);
GO

-- Creating table 'Comments'
CREATE TABLE [dbo].[Comments] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PublicationDate] datetime  NOT NULL,
    [CommentText] nvarchar(max)  NOT NULL,
    [CommentLikes] nvarchar(max)  NULL,
    [DemotivatorId] int  NOT NULL,
    [AspNetUserId] nvarchar(128)  NOT NULL
);
GO

-- Creating table 'Likes'
CREATE TABLE [dbo].[Likes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [IsLiked] bit  NOT NULL,
    [CommentId] int  NOT NULL,
    [AspNetUserId] nvarchar(128)  NOT NULL
);
GO

-- Creating table 'tag_to_dem'
CREATE TABLE [dbo].[tag_to_dem] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DemotivatorId] int  NOT NULL,
    [tagId] int  NOT NULL
);
GO

-- Creating table 'tags'
CREATE TABLE [dbo].[tags] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Count] nvarchar(max)  NOT NULL,
    [Name] nvarchar(max)  NOT NULL
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

-- Creating primary key on [Id] in table 'DemotivatorRates'
ALTER TABLE [dbo].[DemotivatorRates]
ADD CONSTRAINT [PK_DemotivatorRates]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Demotivators'
ALTER TABLE [dbo].[Demotivators]
ADD CONSTRAINT [PK_Demotivators]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [PK_Comments]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Likes'
ALTER TABLE [dbo].[Likes]
ADD CONSTRAINT [PK_Likes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'tag_to_dem'
ALTER TABLE [dbo].[tag_to_dem]
ADD CONSTRAINT [PK_tag_to_dem]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'tags'
ALTER TABLE [dbo].[tags]
ADD CONSTRAINT [PK_tags]
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

-- Creating foreign key on [DemotivatorId] in table 'DemotivatorRates'
ALTER TABLE [dbo].[DemotivatorRates]
ADD CONSTRAINT [FK_DemotivatorRateDemotivator]
    FOREIGN KEY ([DemotivatorId])
    REFERENCES [dbo].[Demotivators]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DemotivatorRateDemotivator'
CREATE INDEX [IX_FK_DemotivatorRateDemotivator]
ON [dbo].[DemotivatorRates]
    ([DemotivatorId]);
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

-- Creating foreign key on [DemotivatorId] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [FK_DemotivatorComment]
    FOREIGN KEY ([DemotivatorId])
    REFERENCES [dbo].[Demotivators]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DemotivatorComment'
CREATE INDEX [IX_FK_DemotivatorComment]
ON [dbo].[Comments]
    ([DemotivatorId]);
GO

-- Creating foreign key on [AspNetUserId] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [FK_AspNetUserComment]
    FOREIGN KEY ([AspNetUserId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AspNetUserComment'
CREATE INDEX [IX_FK_AspNetUserComment]
ON [dbo].[Comments]
    ([AspNetUserId]);
GO

-- Creating foreign key on [AspNetUserId] in table 'DemotivatorRates'
ALTER TABLE [dbo].[DemotivatorRates]
ADD CONSTRAINT [FK_AspNetUserDemotivatorRate]
    FOREIGN KEY ([AspNetUserId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AspNetUserDemotivatorRate'
CREATE INDEX [IX_FK_AspNetUserDemotivatorRate]
ON [dbo].[DemotivatorRates]
    ([AspNetUserId]);
GO

-- Creating foreign key on [CommentId] in table 'Likes'
ALTER TABLE [dbo].[Likes]
ADD CONSTRAINT [FK_CommentLike]
    FOREIGN KEY ([CommentId])
    REFERENCES [dbo].[Comments]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CommentLike'
CREATE INDEX [IX_FK_CommentLike]
ON [dbo].[Likes]
    ([CommentId]);
GO

-- Creating foreign key on [AspNetUserId] in table 'Likes'
ALTER TABLE [dbo].[Likes]
ADD CONSTRAINT [FK_AspNetUserLike]
    FOREIGN KEY ([AspNetUserId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AspNetUserLike'
CREATE INDEX [IX_FK_AspNetUserLike]
ON [dbo].[Likes]
    ([AspNetUserId]);
GO

-- Creating foreign key on [DemotivatorId] in table 'tag_to_dem'
ALTER TABLE [dbo].[tag_to_dem]
ADD CONSTRAINT [FK_tag_to_demDemotivator]
    FOREIGN KEY ([DemotivatorId])
    REFERENCES [dbo].[Demotivators]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_tag_to_demDemotivator'
CREATE INDEX [IX_FK_tag_to_demDemotivator]
ON [dbo].[tag_to_dem]
    ([DemotivatorId]);
GO

-- Creating foreign key on [tagId] in table 'tag_to_dem'
ALTER TABLE [dbo].[tag_to_dem]
ADD CONSTRAINT [FK_tag_to_demtag]
    FOREIGN KEY ([tagId])
    REFERENCES [dbo].[tags]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_tag_to_demtag'
CREATE INDEX [IX_FK_tag_to_demtag]
ON [dbo].[tag_to_dem]
    ([tagId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------