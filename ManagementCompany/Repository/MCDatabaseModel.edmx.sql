
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 10/16/2011 14:24:02
-- Generated from EDMX file: D:\Фриланс\Управляющая компания\GitProjects\ManagementCompany\Repository\MCDatabaseModel.edmx
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

IF OBJECT_ID(N'[dbo].[FK_BuildingsDateTimeImtervals]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DateTimeImtervalsНабор] DROP CONSTRAINT [FK_BuildingsDateTimeImtervals];
GO
IF OBJECT_ID(N'[dbo].[FK_BuildingsContractConsumptionHeat]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ContractConsumptionHeatTable] DROP CONSTRAINT [FK_BuildingsContractConsumptionHeat];
GO
IF OBJECT_ID(N'[dbo].[FK_BuildingsNormativeCalculation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[NormativeCalculationНабор] DROP CONSTRAINT [FK_BuildingsNormativeCalculation];
GO
IF OBJECT_ID(N'[dbo].[FK_ContractConsumptionHeatDateTimeImtervals]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ContractConsumptionHeatTable] DROP CONSTRAINT [FK_ContractConsumptionHeatDateTimeImtervals];
GO
IF OBJECT_ID(N'[dbo].[FK_NormativeCalculationDateTimeImtervals]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[NormativeCalculationНабор] DROP CONSTRAINT [FK_NormativeCalculationDateTimeImtervals];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[BuildingsНабор]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BuildingsНабор];
GO
IF OBJECT_ID(N'[dbo].[DateTimeImtervalsНабор]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DateTimeImtervalsНабор];
GO
IF OBJECT_ID(N'[dbo].[NormativeCalculationНабор]', 'U') IS NOT NULL
    DROP TABLE [dbo].[NormativeCalculationНабор];
GO
IF OBJECT_ID(N'[dbo].[ContractConsumptionHeatTable]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ContractConsumptionHeatTable];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'BuildingsНабор'
CREATE TABLE [dbo].[BuildingsНабор] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [EstimateConsumptionHeat] nvarchar(max)  NOT NULL
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
    [BuildingsId] int  NOT NULL,
    [DateTimeImtervals_Id] int  NOT NULL
);
GO

-- Creating table 'ContractConsumptionHeatTable'
CREATE TABLE [dbo].[ContractConsumptionHeatTable] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [AirTemperature] nvarchar(max)  NOT NULL,
    [HeatByLoading] nvarchar(max)  NOT NULL,
    [PeopleCount] nvarchar(max)  NOT NULL,
    [HotWaterByNorm] nvarchar(max)  NOT NULL,
    [TotalHeatConsumption] nvarchar(max)  NOT NULL,
    [BuildingsId] int  NOT NULL,
    [DateTimeImtervals_Id] int  NOT NULL
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

-- Creating primary key on [ID] in table 'ContractConsumptionHeatTable'
ALTER TABLE [dbo].[ContractConsumptionHeatTable]
ADD CONSTRAINT [PK_ContractConsumptionHeatTable]
    PRIMARY KEY CLUSTERED ([ID] ASC);
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

-- Creating foreign key on [BuildingsId] in table 'ContractConsumptionHeatTable'
ALTER TABLE [dbo].[ContractConsumptionHeatTable]
ADD CONSTRAINT [FK_BuildingsContractConsumptionHeat]
    FOREIGN KEY ([BuildingsId])
    REFERENCES [dbo].[BuildingsНабор]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_BuildingsContractConsumptionHeat'
CREATE INDEX [IX_FK_BuildingsContractConsumptionHeat]
ON [dbo].[ContractConsumptionHeatTable]
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

-- Creating foreign key on [DateTimeImtervals_Id] in table 'ContractConsumptionHeatTable'
ALTER TABLE [dbo].[ContractConsumptionHeatTable]
ADD CONSTRAINT [FK_ContractConsumptionHeatDateTimeImtervals]
    FOREIGN KEY ([DateTimeImtervals_Id])
    REFERENCES [dbo].[DateTimeImtervalsНабор]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ContractConsumptionHeatDateTimeImtervals'
CREATE INDEX [IX_FK_ContractConsumptionHeatDateTimeImtervals]
ON [dbo].[ContractConsumptionHeatTable]
    ([DateTimeImtervals_Id]);
GO

-- Creating foreign key on [DateTimeImtervals_Id] in table 'NormativeCalculationНабор'
ALTER TABLE [dbo].[NormativeCalculationНабор]
ADD CONSTRAINT [FK_NormativeCalculationDateTimeImtervals]
    FOREIGN KEY ([DateTimeImtervals_Id])
    REFERENCES [dbo].[DateTimeImtervalsНабор]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_NormativeCalculationDateTimeImtervals'
CREATE INDEX [IX_FK_NormativeCalculationDateTimeImtervals]
ON [dbo].[NormativeCalculationНабор]
    ([DateTimeImtervals_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------