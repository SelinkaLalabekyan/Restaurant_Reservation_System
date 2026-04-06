BEGIN TRANSACTION;

DECLARE @CustomerId INT = 1;

IF NOT EXISTS (
    SELECT 1
    FROM Reservation
    WHERE CustomerID = @CustomerId
    AND Status IN ('Confirmed', 'Pending')
)
BEGIN
    DELETE FROM Customer
    WHERE CustomerID = @CustomerId;

    COMMIT;
END
ELSE
BEGIN
    ROLLBACK;
    PRINT 'Customer has active reservations';
END