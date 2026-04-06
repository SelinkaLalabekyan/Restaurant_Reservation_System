CREATE TABLE [Table] (
    TableID INT PRIMARY KEY IDENTITY,
    Capacity INT NOT NULL,
    AreaID INT,
    FOREIGN KEY (AreaID) REFERENCES RestaurantArea(AreaID)
);

