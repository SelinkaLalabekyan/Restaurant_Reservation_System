CREATE TRIGGER trg_NoOverlap
ON Reservation
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (
        SELECT 1
        FROM inserted i
        JOIN Reservation r
            ON r.TableID = i.TableID
            AND r.ReservationDate = i.ReservationDate

           
            AND r.ReservationID <> i.ReservationID

           
            AND i.StartTime < r.EndTime
            AND i.EndTime > r.StartTime
    )
    BEGIN
        THROW 50001, 'Time conflict: Table already reserved for this period.', 1;
    END
END;