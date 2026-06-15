USE [master]
GO IF NOT EXISTS (
		SELECT name
		FROM sys.databases
		WHERE name = N'SpareWMS'
	) BEGIN CREATE DATABASE [SpareWMS]
END
GO USE [SpareWMS]
GO
	/****** 1. DROP CONSTRAINTS AND TABLES IF THEY EXIST (For clean setup) ******/
	IF EXISTS (
		SELECT *
		FROM sys.foreign_keys
		WHERE name = 'FK_InventoryLogs_RackBins'
	)
ALTER TABLE [dbo].[InventoryLogs] DROP CONSTRAINT [FK_InventoryLogs_RackBins] IF EXISTS (
		SELECT *
		FROM sys.foreign_keys
		WHERE name = 'FK_InventoryLogs_SpareParts'
	)
ALTER TABLE [dbo].[InventoryLogs] DROP CONSTRAINT [FK_InventoryLogs_SpareParts] IF EXISTS (
		SELECT *
		FROM sys.foreign_keys
		WHERE name = 'FK_InventoryLogs_Users'
	)
ALTER TABLE [dbo].[InventoryLogs] DROP CONSTRAINT [FK_InventoryLogs_Users] IF EXISTS (
		SELECT *
		FROM sys.foreign_keys
		WHERE name = 'FK_RackBins_RackRows'
	)
ALTER TABLE [dbo].[RackBins] DROP CONSTRAINT [FK_RackBins_RackRows] IF EXISTS (
		SELECT *
		FROM sys.foreign_keys
		WHERE name = 'FK_RackBins_SpareParts'
	)
ALTER TABLE [dbo].[RackBins] DROP CONSTRAINT [FK_RackBins_SpareParts] IF EXISTS (
		SELECT *
		FROM sys.foreign_keys
		WHERE name = 'FK_RackRows_Racks'
	)
ALTER TABLE [dbo].[RackRows] DROP CONSTRAINT [FK_RackRows_Racks] IF EXISTS (
		SELECT *
		FROM sys.foreign_keys
		WHERE name = 'FK_Racks_Categories'
	)
ALTER TABLE [dbo].[Racks] DROP CONSTRAINT [FK_Racks_Categories] IF EXISTS (
		SELECT *
		FROM sys.foreign_keys
		WHERE name = 'FK_SpareParts_Categories'
	)
ALTER TABLE [dbo].[SpareParts] DROP CONSTRAINT [FK_SpareParts_Categories] IF EXISTS (
		SELECT *
		FROM sys.foreign_keys
		WHERE name = 'FK_SpareParts_Users_Created'
	)
ALTER TABLE [dbo].[SpareParts] DROP CONSTRAINT [FK_SpareParts_Users_Created] IF EXISTS (
		SELECT *
		FROM sys.foreign_keys
		WHERE name = 'FK_SpareParts_Users_Modified'
	)
ALTER TABLE [dbo].[SpareParts] DROP CONSTRAINT [FK_SpareParts_Users_Modified] IF EXISTS (
		SELECT *
		FROM sys.foreign_keys
		WHERE name = 'FK_SpareParts_Users_Deleted'
	)
ALTER TABLE [dbo].[SpareParts] DROP CONSTRAINT [FK_SpareParts_Users_Deleted] DROP TABLE IF EXISTS [dbo].[InventoryLogs] DROP TABLE IF EXISTS [dbo].[RackBins] DROP TABLE IF EXISTS [dbo].[RackRows] DROP TABLE IF EXISTS [dbo].[Racks] DROP TABLE IF EXISTS [dbo].[SpareParts] DROP TABLE IF EXISTS [dbo].[Categories] DROP TABLE IF EXISTS [dbo].[Users]
GO
	/****** 2. CREATE TABLES ******/
	CREATE TABLE [dbo].[Users](
		[Id] [int] IDENTITY(1, 1) NOT NULL PRIMARY KEY,
		[FullName] [varchar](100) NOT NULL,
		[Username] [varchar](100) NOT NULL UNIQUE,
		[Password] [varchar](100) NOT NULL,
		[Role] [varchar](100) NOT NULL
	)
GO CREATE TABLE [dbo].[Categories](
		[Id] [int] IDENTITY(1, 1) NOT NULL PRIMARY KEY,
		[Name] [varchar](100) NOT NULL
	)
GO CREATE TABLE [dbo].[Racks](
		[Id] [int] IDENTITY(1, 1) NOT NULL PRIMARY KEY,
		[RackCode] [varchar](100) NOT NULL,
		[CategoryId] [int] NOT NULL,
		[Description] [varchar](200) NOT NULL,
		CONSTRAINT [FK_Racks_Categories] FOREIGN KEY([CategoryId]) REFERENCES [dbo].[Categories] ([Id])
	)
GO CREATE TABLE [dbo].[RackRows](
		[Id] [int] IDENTITY(1, 1) NOT NULL PRIMARY KEY,
		[RackId] [int] NOT NULL,
		[RowNumber] [int] NOT NULL,
		[RowName] [varchar](100) NOT NULL,
		CONSTRAINT [FK_RackRows_Racks] FOREIGN KEY([RackId]) REFERENCES [dbo].[Racks] ([Id])
	)
GO CREATE TABLE [dbo].[SpareParts](
		[Id] [int] IDENTITY(1, 1) NOT NULL PRIMARY KEY,
		[PartNumber] [varchar](100) NOT NULL,
		[CategoryId] [int] NOT NULL,
		[Name] [varchar](200) NOT NULL,
		[IsActive] [bit] NOT NULL DEFAULT (1),
		[MaxCapacityInRack] [int] NOT NULL,
		[CreatedAt] [datetime] NOT NULL DEFAULT (getdate()),
		[CreatedByUserId] [int] NOT NULL,
		[ModifiedAt] [datetime] NULL,
		[ModifiedByUserId] [int] NULL,
		[DeletedAt] [datetime] NULL,
		[DeletedByUserId] [int] NULL,
		CONSTRAINT [FK_SpareParts_Categories] FOREIGN KEY([CategoryId]) REFERENCES [dbo].[Categories] ([Id]),
		CONSTRAINT [FK_SpareParts_Users_Created] FOREIGN KEY([CreatedByUserId]) REFERENCES [dbo].[Users] ([Id]),
		CONSTRAINT [FK_SpareParts_Users_Modified] FOREIGN KEY([ModifiedByUserId]) REFERENCES [dbo].[Users] ([Id]),
		CONSTRAINT [FK_SpareParts_Users_Deleted] FOREIGN KEY([DeletedByUserId]) REFERENCES [dbo].[Users] ([Id])
	)
GO CREATE TABLE [dbo].[RackBins](
		[Id] [int] IDENTITY(1, 1) NOT NULL PRIMARY KEY,
		[RackRowId] [int] NOT NULL,
		[BinNumber] [int] NOT NULL,
		[BinCode] [varchar](100) NOT NULL,
		[SparePartId] [int] NULL,
		[Capacity] [int] NULL,
		[EntryDate] [datetime] NULL,
		[ModifiedAt] [datetime] NULL,
		CONSTRAINT [FK_RackBins_RackRows] FOREIGN KEY([RackRowId]) REFERENCES [dbo].[RackRows] ([Id]),
		CONSTRAINT [FK_RackBins_SpareParts] FOREIGN KEY([SparePartId]) REFERENCES [dbo].[SpareParts] ([Id])
	)
GO CREATE TABLE [dbo].[InventoryLogs](
		[Id] [int] IDENTITY(1, 1) NOT NULL PRIMARY KEY,
		[BinId] [int] NOT NULL,
		[SparePartId] [int] NOT NULL,
		[TransactionType] [varchar](100) NOT NULL,
		-- 'IN' or 'OUT'
		[Quantity] [int] NOT NULL,
		[TransactionDate] [datetime] NOT NULL DEFAULT (getdate()),
		[UserId] [int] NOT NULL,
		CONSTRAINT [FK_InventoryLogs_RackBins] FOREIGN KEY([BinId]) REFERENCES [dbo].[RackBins] ([Id]),
		CONSTRAINT [FK_InventoryLogs_SpareParts] FOREIGN KEY([SparePartId]) REFERENCES [dbo].[SpareParts] ([Id]),
		CONSTRAINT [FK_InventoryLogs_Users] FOREIGN KEY([UserId]) REFERENCES [dbo].[Users] ([Id])
	)
GO
	/****** 3. SEED DATA ******/
SET IDENTITY_INSERT [dbo].[Users] ON
INSERT [dbo].[Users] ([Id], [FullName], [Username], [Password], [Role])
VALUES (
		1,
		N'Budi Santoso',
		N'budi.sup',
		N'Pass123!',
		N'Supervisor'
	),
	(
		2,
		N'Siti Aminah',
		N'siti.sup',
		N'Pass123!',
		N'Supervisor'
	),
	(
		3,
		N'Andi Wijaya',
		N'andi.op',
		N'Op12345',
		N'Operator'
	),
	(
		4,
		N'Rina Permata',
		N'rina.op',
		N'Op12345',
		N'Operator'
	),
	(
		5,
		N'Fajar Ramadhan',
		N'fajar.op',
		N'Op12345',
		N'Operator'
	)
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET IDENTITY_INSERT [dbo].[Categories] ON
INSERT [dbo].[Categories] ([Id], [Name])
VALUES (1, N'Finished Goods'),
	(2, N'Raw Materials')
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
SET IDENTITY_INSERT [dbo].[Racks] ON
INSERT [dbo].[Racks] ([Id], [RackCode], [CategoryId], [Description])
VALUES (1, N'FG-01', 1, N'Finish Goods 01'),
	(2, N'FG-02', 1, N'Finish Goods 02'),
	(3, N'RM-01', 2, N'Raw Materials 01'),
	(4, N'RM-02', 2, N'Raw Materials 02'),
	(5, N'RM-03', 2, N'Raw Materials 03')
SET IDENTITY_INSERT [dbo].[Racks] OFF
GO
SET IDENTITY_INSERT [dbo].[RackRows] ON
INSERT [dbo].[RackRows] ([Id], [RackId], [RowNumber], [RowName])
VALUES (1, 1, 1, N'Level 1'),
	(2, 1, 2, N'Level 2'),
	(3, 1, 3, N'Level 3'),
	(4, 2, 1, N'Level 1'),
	(5, 2, 2, N'Level 2'),
	(6, 2, 3, N'Level 3'),
	(7, 3, 1, N'Level 1'),
	(8, 3, 2, N'Level 2'),
	(9, 3, 3, N'Level 3'),
	(10, 4, 1, N'Level 1'),
	(11, 4, 2, N'Level 2'),
	(12, 4, 3, N'Level 3'),
	(13, 5, 1, N'Level 1'),
	(14, 5, 2, N'Level 2'),
	(15, 5, 3, N'Level 3')
SET IDENTITY_INSERT [dbo].[RackRows] OFF
GO
SET IDENTITY_INSERT [dbo].[SpareParts] ON
INSERT [dbo].[SpareParts] (
		[Id],
		[PartNumber],
		[CategoryId],
		[Name],
		[IsActive],
		[MaxCapacityInRack],
		[CreatedAt],
		[CreatedByUserId]
	)
VALUES (
		1,
		N 'FG-ENG-001',
		1,
		N'Complete Engine Block V125',
		1,
		5,
		'2026-04-22',
		1
	),
	(
		2,
		N 'FG-ENG-002',
		1,
		N'Transmission Set Manual 5-Speed',
		1,
		10,
		'2026-04-22',
		1
	),
	(
		3,
		N'FG-ELC-001',
		1,
		N'Full Wiring Harness Assembly',
		1,
		20,
		'2026-04-22',
		1
	),
	(
		4,
		N'FG-ELC-002',
		1,
		N'LED Headlight Unit Projector',
		1,
		30,
		'2026-04-22',
		1
	),
	(
		5,
		N'FG-BOD-001',
		1,
		N'Fuel Tank Assembly Black Matte',
		1,
		15,
		'2026-04-22',
		1
	),
	(
		6,
		N'FG-BOD-002',
		1,
		N'Front Fork Suspension Set',
		1,
		20,
		'2026-04-22',
		1
	),
	(
		7,
		N'FG-BRK-001',
		1,
		N'ABS Brake System Module',
		1,
		25,
		'2026-04-22',
		1
	),
	(
		8,
		N'FG-WHL-001',
		1,
		N'Alloy Wheel Set 14 Inch Gold',
		1,
		10,
		'2026-04-22',
		1
	),
	(
		9,
		N'FG-EXH-001',
		1,
		N'Full System Exhaust Racing',
		1,
		12,
		'2026-04-22',
		1
	),
	(
		10,
		N 'FG-INS-001',
		1,
		N'Digital Instrument Cluster Unit',
		1,
		40,
		'2026-04-22',
		1
	),
	(
		11,
		N'RM-ALU-001',
		2,
		N'Aluminum Ingot Grade A (1kg)',
		1,
		500,
		'2026-04-22',
		1
	),
	(
		12,
		N'RM-STL-001',
		2,
		N'Steel Plate 2mm x 1m x 1m',
		1,
		100,
		'2026-04-22',
		1
	),
	(
		13,
		N'RM-RUB-001',
		2,
		N 'Natural Rubber Compound (5kg)',
		1,
		50,
		'2026-04-22',
		1
	),
	(
		14,
		N'RM-PLA-001',
		2,
		N'Plastic Pellets ABS High Impact',
		1,
		200,
		'2026-04-22',
		1
	),
	(
		15,
		N'RM-COP-001',
		2,
		N'Copper Wire Spool 0.5mm',
		1,
		80,
		'2026-04-22',
		1
	),
	(
		16,
		N'RM-CHE-001',
		2,
		N'Industrial Paint Coating Black',
		1,
		40,
		'2026-04-22',
		1
	),
	(
		17,
		N'RM-FAB-001',
		2,
		N'Synthetic Leather Fabric Roll',
		1,
		25,
		'2026-04-22',
		1
	),
	(
		18,
		N'RM-GLS-001',
		2,
		N'Tempered Glass Sheet 50x50',
		1,
		60,
		'2026-04-22',
		1
	),
	(
		19,
		N'RM-FOA-001',
		2,
		N'Polyurethane Foam Block',
		1,
		30,
		'2026-04-22',
		1
	),
	(
		20,
		N'RM-OIL-001',
		2,
		N'Raw Lubricant Base Oil (20L)',
		1,
		20,
		'2026-04-22',
		1
	)
SET IDENTITY_INSERT [dbo].[SpareParts] OFF
GO
SET IDENTITY_INSERT [dbo].[RackBins] ON
INSERT [dbo].[RackBins] (
		[Id],
		[RackRowId],
		[BinNumber],
		[BinCode],
		[SparePartId],
		[Capacity],
		[EntryDate],
		[ModifiedAt]
	)
VALUES (
		1,
		1,
		1,
		N'FG-01-L1-S01',
		1,
		5,
		'2026-04-22 09:00:00',
		'2026-04-22 09:00:00'
	),
	(
		2,
		1,
		2,
		N'FG-01-L1-S02',
		2,
		8,
		'2026-04-22 10:00:00',
		'2026-04-22 15:30:00'
	),
	(
		3,
		1,
		3,
		N'FG-01-L1-S03',
		3,
		15,
		'2026-04-22 10:00:00',
		'2026-04-22 10:00:00'
	),
	(
		13,
		2,
		1,
		N'FG-01-L2-S01',
		4,
		15,
		'2026-04-22 10:00:00',
		'2026-04-22 15:30:00'
	),
	(
		37,
		4,
		1,
		N'FG-02-L1-S01',
		6,
		8,
		'2026-04-22 10:00:00',
		'2026-04-22 10:00:00'
	),
	(
		100,
		9,
		4,
		N'RM-01-L3-S04',
		11,
		150,
		'2026-04-22 09:00:00',
		'2026-04-22 09:00:00'
	),
	(
		101,
		9,
		5,
		N'RM-01-L3-S05',
		13,
		30,
		'2026-04-22 10:00:00',
		'2026-04-22 15:30:00'
	),
	(
		110,
		10,
		2,
		N'RM-02-L1-S02',
		14,
		50,
		'2026-04-22 10:00:00',
		'2026-04-22 15:30:00'
	),
	(
		125,
		11,
		5,
		N'RM-02-L2-S05',
		15,
		30,
		'2026-04-22 10:00:00',
		'2026-04-22 10:00:00'
	),
	(
		140,
		12,
		8,
		N'RM-02-L3-S08',
		16,
		20,
		'2026-04-22 10:00:00',
		'2026-04-22 10:00:00'
	),
	(
		150,
		13,
		6,
		N'RM-03-L1-S06',
		12,
		50,
		'2026-04-22 09:00:00',
		'2026-04-22 09:00:00'
	),
	(
		160,
		14,
		4,
		N'RM-03-L2-S04',
		17,
		10,
		'2026-04-22 10:00:00',
		'2026-04-22 10:00:00'
	)
SET IDENTITY_INSERT [dbo].[RackBins] OFF
GO -- Adding empty bins
INSERT [dbo].[RackBins] ([RackRowId], [BinNumber], [BinCode])
VALUES (1, 4, N'FG-01-L1-S04'),
	(1, 5, N'FG-01-L1-S05'),
	(1, 6, N'FG-01-L1-S06'),
	(1, 7, N'FG-01-L1-S07'),
	(1, 8, N'FG-01-L1-S08'),
	(1, 9, N'FG-01-L1-S09'),
	(1, 10, N'FG-01-L1-S10'),
	(1, 11, N'FG-01-L1-S11'),
	(1, 12, N'FG-01-L1-S12')
GO
SET IDENTITY_INSERT [dbo].[InventoryLogs] ON
INSERT [dbo].[InventoryLogs] (
		[Id],
		[BinId],
		[SparePartId],
		[TransactionType],
		[Quantity],
		[TransactionDate],
		[UserId]
	)
VALUES (1, 1, 1, N 'IN', 5, '2026-04-22 09:00:00', 3),
	(2, 100, 11, N 'IN', 150, '2026-04-22 09:00:00', 4),
	(3, 150, 12, N 'IN', 50, '2026-04-22 09:00:00', 5),
	(4, 2, 2, N 'IN', 10, '2026-04-22 10:00:00', 3),
	(5, 3, 3, N 'IN', 15, '2026-04-22 10:00:00', 3),
	(6, 13, 4, N 'IN', 20, '2026-04-22 10:00:00', 4),
	(14, 2, 2, N'OUT', 2, '2026-04-22 15:30:00', 3),
	(15, 13, 4, N'OUT', 5, '2026-04-22 15:30:00', 4),
	(
		16,
		101,
		13,
		N'OUT',
		10,
		'2026-04-22 15:30:00',
		5
	)
SET IDENTITY_INSERT [dbo].[InventoryLogs] OFF
GO