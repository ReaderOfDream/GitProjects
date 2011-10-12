
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 10/12/2011 23:36:47
-- Generated from EDMX file: D:\Фриланс\Отопление\ManagementCompany\Repository\MCDatabaseModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [ManagementCompany];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'BuildingsНабор'
CREATE TABLE [dbo].[BuildingsНабор] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'DateTimeImtervalsНабор'
CREATE TABLE [dbo].[DateTimeImtervalsНабор] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [StartDate] nvarchar(max)  NOT NULL,
    [EndDate] nvarchar(max)  NOT NULL,
    [BuildingsId] int  NOT NULL
);
GO

-- Creating table 'NormativeCalculationНабор'
CREATE TABLE [dbo].[NormativeCalculationНабор] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [TotalArea] nvarchar(max)  NOT NULL,
    [CalculationArea] nvarchar(max)  NOT NULL,
    [StandartOfHeat] nvarchar(max)  NOT NULL,
    [ConsumptionHeatByTotalArea] nvarchar(max)  NOT NULL,
    [ConsumptionHeatByCalculationArea] nvarchar(max)  NOT NULL,
    [TotalNormativeHeat] nvarchar(max)  NOT NULL,
    [BuildingsId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'BuildingsНабор'
ALTER TABLE [dbo].[BuildingsНабор]
ADD CONSTRAINT [PK_BuildingsНабор]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DateTimeImtervalsНабор'
ALTER TABLE [dbo].[DateTimeImtervalsНабор]
ADD CONSTRAINT [PK_DateTimeImtervalsНабор]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'NormativeCalculationНабор'
ALTER TABLE [dbo].[NormativeCalculationНабор]
ADD CONSTRAINT [PK_NormativeCalculationНабор]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [BuildingsId] in table 'DateTimeImtervalsНабор'
ALTER TABLE [dbo].[DateTimeImtervalsНабор]
ADD CONSTRAINT [FK_BuildingsDateTimeImtervals]
    FOREIGN KEY ([BuildingsId])
    REFERENCES [dbo].[BuildingsНабор]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_BuildingsDateTimeImtervals'
CREATE INDEX [IX_FK_BuildingsDateTimeImtervals]
ON [dbo].[DateTimeImtervalsНабор]
    ([BuildingsId]);
GO

-- Creating foreign key on [BuildingsId] in table 'NormativeCalculationНабор'
ALTER TABLE [dbo].[NormativeCalculationНабор]
ADD CONSTRAINT [FK_BuildingsNormativeCalculation]
    FOREIGN KEY ([BuildingsId])
    REFERENCES [dbo].[BuildingsНабор]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_BuildingsNormativeCalculation'
CREATE INDEX [IX_FK_BuildingsNormativeCalculation]
ON [dbo].[NormativeCalculationНабор]
    ([BuildingsId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------