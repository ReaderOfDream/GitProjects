
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 01/12/2012 23:55:25
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

IF OBJECT_ID(N'[dbo].[FK_BuildingsContractConsumptionHeat]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ContractConsumptionHeatTable] DROP CONSTRAINT [FK_BuildingsContractConsumptionHeat];
GO
IF OBJECT_ID(N'[dbo].[FK_BuildingsNormativeCalculation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[NormativeCalculations] DROP CONSTRAINT [FK_BuildingsNormativeCalculation];
GO
IF OBJECT_ID(N'[dbo].[FK_ContractConsumptionHeatDateTimeImtervals]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ContractConsumptionHeatTable] DROP CONSTRAINT [FK_ContractConsumptionHeatDateTimeImtervals];
GO
IF OBJECT_ID(N'[dbo].[FK_NormativeCalculationDateTimeImtervals]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[NormativeCalculations] DROP CONSTRAINT [FK_NormativeCalculationDateTimeImtervals];
GO
IF OBJECT_ID(N'[dbo].[FK_BuildingsMeterReadings]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MeterReadingsTable] DROP CONSTRAINT [FK_BuildingsMeterReadings];
GO
IF OBJECT_ID(N'[dbo].[FK_MeterReadingsDateTimeImtervals]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MeterReadingsTable] DROP CONSTRAINT [FK_MeterReadingsDateTimeImtervals];
GO
IF OBJECT_ID(N'[dbo].[FK_BuildingsClearing]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ClearingTable] DROP CONSTRAINT [FK_BuildingsClearing];
GO
IF OBJECT_ID(N'[dbo].[FK_DateTimeImtervalsClearing]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ClearingTable] DROP CONSTRAINT [FK_DateTimeImtervalsClearing];
GO
IF OBJECT_ID(N'[dbo].[FK_BuildingsHeatSupplier]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Buildings] DROP CONSTRAINT [FK_BuildingsHeatSupplier];
GO
IF OBJECT_ID(N'[dbo].[FK_DateTimeImtervalsHeatSupplier]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DateTimeImtervals] DROP CONSTRAINT [FK_DateTimeImtervalsHeatSupplier];
GO
IF OBJECT_ID(N'[dbo].[FK_ContractConsumptionHeatThermometerReading]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ContractConsumptionHeatTable] DROP CONSTRAINT [FK_ContractConsumptionHeatThermometerReading];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Buildings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Buildings];
GO
IF OBJECT_ID(N'[dbo].[DateTimeImtervals]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DateTimeImtervals];
GO
IF OBJECT_ID(N'[dbo].[NormativeCalculations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[NormativeCalculations];
GO
IF OBJECT_ID(N'[dbo].[ContractConsumptionHeatTable]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ContractConsumptionHeatTable];
GO
IF OBJECT_ID(N'[dbo].[MeterReadingsTable]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MeterReadingsTable];
GO
IF OBJECT_ID(N'[dbo].[ClearingTable]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ClearingTable];
GO
IF OBJECT_ID(N'[dbo].[HeatSuppliers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[HeatSuppliers];
GO
IF OBJECT_ID(N'[dbo].[ThermometerReadings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ThermometerReadings];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Buildings'
CREATE TABLE [dbo].[Buildings] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NULL,
    [StandartOfHeat] float  NOT NULL,
    [TotalArea] nvarchar(max)  NOT NULL,
    [HeatSupplier_Id] int  NOT NULL
);
GO

-- Creating table 'DateTimeImtervals'
CREATE TABLE [dbo].[DateTimeImtervals] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [StartDate] datetime  NOT NULL,
    [EndDate] datetime  NOT NULL,
    [HeatSupplierId] int  NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'NormativeCalculations'
CREATE TABLE [dbo].[NormativeCalculations] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CalculationArea] float  NOT NULL,
    [EstimateConsumptionHeat] float  NOT NULL,
    [ConsumptionHeatByTotalArea] float  NOT NULL,
    [ConsumptionHeatByCalculationArea] float  NOT NULL,
    [BuildingsId] int  NOT NULL,
    [DateTimeImtervals_Id] int  NOT NULL
);
GO

-- Creating table 'ContractConsumptionHeatTable'
CREATE TABLE [dbo].[ContractConsumptionHeatTable] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [HeatByLoading] float  NOT NULL,
    [PeopleCount] int  NOT NULL,
    [HotWaterByNorm] float  NOT NULL,
    [TotalHeatConsumption] float  NOT NULL,
    [BuildingsId] int  NOT NULL,
    [DateTimeImtervals_Id] int  NOT NULL,
    [ThermometerReading_Id] int  NOT NULL
);
GO

-- Creating table 'MeterReadingsTable'
CREATE TABLE [dbo].[MeterReadingsTable] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [CurrentHeatMeterReader] float  NOT NULL,
    [CurrentWaterHeatReader] float  NOT NULL,
    [BuildingsId] int  NOT NULL,
    [DateTimeImtervals_Id] int  NOT NULL
);
GO

-- Creating table 'ClearingTable'
CREATE TABLE [dbo].[ClearingTable] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Requirements] float  NOT NULL,
    [CalculationHotWater] float  NOT NULL,
    [CalculationHot] float  NOT NULL,
    [BuildingsId] int  NOT NULL,
    [DateTimeImtervals_Id] int  NOT NULL
);
GO

-- Creating table 'HeatSuppliers'
CREATE TABLE [dbo].[HeatSuppliers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ThermometerReadings'
CREATE TABLE [dbo].[ThermometerReadings] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Month] nvarchar(max)  NOT NULL,
    [Year] nvarchar(max)  NOT NULL,
    [AirTemperature] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Buildings'
ALTER TABLE [dbo].[Buildings]
ADD CONSTRAINT [PK_Buildings]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DateTimeImtervals'
ALTER TABLE [dbo].[DateTimeImtervals]
ADD CONSTRAINT [PK_DateTimeImtervals]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'NormativeCalculations'
ALTER TABLE [dbo].[NormativeCalculations]
ADD CONSTRAINT [PK_NormativeCalculations]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [ID] in table 'ContractConsumptionHeatTable'
ALTER TABLE [dbo].[ContractConsumptionHeatTable]
ADD CONSTRAINT [PK_ContractConsumptionHeatTable]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'MeterReadingsTable'
ALTER TABLE [dbo].[MeterReadingsTable]
ADD CONSTRAINT [PK_MeterReadingsTable]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [Id] in table 'ClearingTable'
ALTER TABLE [dbo].[ClearingTable]
ADD CONSTRAINT [PK_ClearingTable]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'HeatSuppliers'
ALTER TABLE [dbo].[HeatSuppliers]
ADD CONSTRAINT [PK_HeatSuppliers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ThermometerReadings'
ALTER TABLE [dbo].[ThermometerReadings]
ADD CONSTRAINT [PK_ThermometerReadings]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [BuildingsId] in table 'ContractConsumptionHeatTable'
ALTER TABLE [dbo].[ContractConsumptionHeatTable]
ADD CONSTRAINT [FK_BuildingsContractConsumptionHeat]
    FOREIGN KEY ([BuildingsId])
    REFERENCES [dbo].[Buildings]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_BuildingsContractConsumptionHeat'
CREATE INDEX [IX_FK_BuildingsContractConsumptionHeat]
ON [dbo].[ContractConsumptionHeatTable]
    ([BuildingsId]);
GO

-- Creating foreign key on [BuildingsId] in table 'NormativeCalculations'
ALTER TABLE [dbo].[NormativeCalculations]
ADD CONSTRAINT [FK_BuildingsNormativeCalculation]
    FOREIGN KEY ([BuildingsId])
    REFERENCES [dbo].[Buildings]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_BuildingsNormativeCalculation'
CREATE INDEX [IX_FK_BuildingsNormativeCalculation]
ON [dbo].[NormativeCalculations]
    ([BuildingsId]);
GO

-- Creating foreign key on [DateTimeImtervals_Id] in table 'ContractConsumptionHeatTable'
ALTER TABLE [dbo].[ContractConsumptionHeatTable]
ADD CONSTRAINT [FK_ContractConsumptionHeatDateTimeImtervals]
    FOREIGN KEY ([DateTimeImtervals_Id])
    REFERENCES [dbo].[DateTimeImtervals]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ContractConsumptionHeatDateTimeImtervals'
CREATE INDEX [IX_FK_ContractConsumptionHeatDateTimeImtervals]
ON [dbo].[ContractConsumptionHeatTable]
    ([DateTimeImtervals_Id]);
GO

-- Creating foreign key on [DateTimeImtervals_Id] in table 'NormativeCalculations'
ALTER TABLE [dbo].[NormativeCalculations]
ADD CONSTRAINT [FK_NormativeCalculationDateTimeImtervals]
    FOREIGN KEY ([DateTimeImtervals_Id])
    REFERENCES [dbo].[DateTimeImtervals]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_NormativeCalculationDateTimeImtervals'
CREATE INDEX [IX_FK_NormativeCalculationDateTimeImtervals]
ON [dbo].[NormativeCalculations]
    ([DateTimeImtervals_Id]);
GO

-- Creating foreign key on [BuildingsId] in table 'MeterReadingsTable'
ALTER TABLE [dbo].[MeterReadingsTable]
ADD CONSTRAINT [FK_BuildingsMeterReadings]
    FOREIGN KEY ([BuildingsId])
    REFERENCES [dbo].[Buildings]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_BuildingsMeterReadings'
CREATE INDEX [IX_FK_BuildingsMeterReadings]
ON [dbo].[MeterReadingsTable]
    ([BuildingsId]);
GO

-- Creating foreign key on [DateTimeImtervals_Id] in table 'MeterReadingsTable'
ALTER TABLE [dbo].[MeterReadingsTable]
ADD CONSTRAINT [FK_MeterReadingsDateTimeImtervals]
    FOREIGN KEY ([DateTimeImtervals_Id])
    REFERENCES [dbo].[DateTimeImtervals]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MeterReadingsDateTimeImtervals'
CREATE INDEX [IX_FK_MeterReadingsDateTimeImtervals]
ON [dbo].[MeterReadingsTable]
    ([DateTimeImtervals_Id]);
GO

-- Creating foreign key on [BuildingsId] in table 'ClearingTable'
ALTER TABLE [dbo].[ClearingTable]
ADD CONSTRAINT [FK_BuildingsClearing]
    FOREIGN KEY ([BuildingsId])
    REFERENCES [dbo].[Buildings]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_BuildingsClearing'
CREATE INDEX [IX_FK_BuildingsClearing]
ON [dbo].[ClearingTable]
    ([BuildingsId]);
GO

-- Creating foreign key on [DateTimeImtervals_Id] in table 'ClearingTable'
ALTER TABLE [dbo].[ClearingTable]
ADD CONSTRAINT [FK_DateTimeImtervalsClearing]
    FOREIGN KEY ([DateTimeImtervals_Id])
    REFERENCES [dbo].[DateTimeImtervals]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DateTimeImtervalsClearing'
CREATE INDEX [IX_FK_DateTimeImtervalsClearing]
ON [dbo].[ClearingTable]
    ([DateTimeImtervals_Id]);
GO

-- Creating foreign key on [HeatSupplier_Id] in table 'Buildings'
ALTER TABLE [dbo].[Buildings]
ADD CONSTRAINT [FK_BuildingsHeatSupplier]
    FOREIGN KEY ([HeatSupplier_Id])
    REFERENCES [dbo].[HeatSuppliers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_BuildingsHeatSupplier'
CREATE INDEX [IX_FK_BuildingsHeatSupplier]
ON [dbo].[Buildings]
    ([HeatSupplier_Id]);
GO

-- Creating foreign key on [HeatSupplierId] in table 'DateTimeImtervals'
ALTER TABLE [dbo].[DateTimeImtervals]
ADD CONSTRAINT [FK_DateTimeImtervalsHeatSupplier]
    FOREIGN KEY ([HeatSupplierId])
    REFERENCES [dbo].[HeatSuppliers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DateTimeImtervalsHeatSupplier'
CREATE INDEX [IX_FK_DateTimeImtervalsHeatSupplier]
ON [dbo].[DateTimeImtervals]
    ([HeatSupplierId]);
GO

-- Creating foreign key on [ThermometerReading_Id] in table 'ContractConsumptionHeatTable'
ALTER TABLE [dbo].[ContractConsumptionHeatTable]
ADD CONSTRAINT [FK_ContractConsumptionHeatThermometerReading]
    FOREIGN KEY ([ThermometerReading_Id])
    REFERENCES [dbo].[ThermometerReadings]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ContractConsumptionHeatThermometerReading'
CREATE INDEX [IX_FK_ContractConsumptionHeatThermometerReading]
ON [dbo].[ContractConsumptionHeatTable]
    ([ThermometerReading_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------