CREATE TABLE Reservation (
    ReservationID INT PRIMARY KEY IDENTITY,
    CustomerID INT,
    TableID INT,
    StaffID INT,
    ReservationDate DATE,
    StartTime TIME,
    EndTime TIME,
    GuestCount INT,
    Status NVARCHAR(20),

    FOREIGN KEY (CustomerID) REFERENCES Customer(CustomerID),
    FOREIGN KEY (TableID) REFERENCES [Table](TableID),
    FOREIGN KEY (StaffID) REFERENCES Staff(StaffID)
);