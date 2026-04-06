CREATE PROCEDURE CreateReservation
    @CustomerID INT,
    @TableID INT,
    @StaffID INT,
    @Date DATE,
    @Start TIME,
    @End TIME,
    @Guests INT
AS
BEGIN
    INSERT INTO Reservation
    VALUES (@CustomerID, @TableID, @StaffID, @Date, @Start, @End, @Guests, 'Confirmed');
END;