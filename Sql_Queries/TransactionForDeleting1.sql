BEGIN TRANSACTION;

DECLARE @CustomerId INT = 1;


IF EXISTS (
    SELECT 1
    FROM Reservation
    WHERE CustomerID = @CustomerId
    AND Status IN ('Confirmed', 'Pending')
)
BEGIN
    ROLLBACK;
    THROW 50001, 'Cannot delete customer with active reservations!', 1;
END


DELETE FROM Customer
WHERE CustomerID = @CustomerId;

COMMIT;