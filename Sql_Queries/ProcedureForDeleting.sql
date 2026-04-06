CREATE PROCEDURE DeleteCustomerSafe
    @CustomerId INT
AS
BEGIN
    BEGIN TRANSACTION;

    IF EXISTS (
        SELECT 1
        FROM Reservation
        WHERE CustomerID = @CustomerId
        AND Status IN ('Confirmed', 'Pending')
    )
    BEGIN
        ROLLBACK;
        THROW 50001, 'Customer has active reservations!', 1;
    END

    DELETE FROM Customer
    WHERE CustomerID = @CustomerId;

    COMMIT;
END;


/* EXEC DeleteCustomerSafe @CustomerId = 1; for call