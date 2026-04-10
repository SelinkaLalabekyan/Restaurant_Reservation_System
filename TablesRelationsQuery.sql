CREATE TABLE [Customer] (
  [CustomerID] int PRIMARY KEY IDENTITY(1, 1),
  [Name] nvarchar(100),
  [Phone] nvarchar(20),
  [Email] nvarchar(100)
)
GO

CREATE TABLE [RestaurantArea] (
  [AreaID] int PRIMARY KEY IDENTITY(1, 1),
  [Name] nvarchar(50)
)
GO

CREATE TABLE [Table] (
  [TableID] int PRIMARY KEY IDENTITY(1, 1),
  [Capacity] int,
  [AreaID] int NOT NULL
)
GO

CREATE TABLE [Staff] (
  [StaffID] int PRIMARY KEY IDENTITY(1, 1),
  [Name] nvarchar(100),
  [Role] nvarchar(50)
)
GO

CREATE TABLE [Reservation] (
  [ReservationID] int PRIMARY KEY IDENTITY(1, 1),
  [CustomerID] int NOT NULL,
  [TableID] int NOT NULL,
  [StaffID] int,
  [ReservationDate] date NOT NULL,
  [StartTime] time NOT NULL,
  [EndTime] time NOT NULL,
  [GuestCount] int NOT NULL,
  [Status] nvarchar(20) NOT NULL
)
GO

ALTER TABLE [Table] ADD FOREIGN KEY ([AreaID]) REFERENCES [RestaurantArea] ([AreaID])
GO

ALTER TABLE [Reservation] ADD FOREIGN KEY ([CustomerID]) REFERENCES [Customer] ([CustomerID])
GO

ALTER TABLE [Reservation] ADD FOREIGN KEY ([TableID]) REFERENCES [Table] ([TableID])
GO

ALTER TABLE [Reservation] ADD FOREIGN KEY ([StaffID]) REFERENCES [Staff] ([StaffID])
GO
