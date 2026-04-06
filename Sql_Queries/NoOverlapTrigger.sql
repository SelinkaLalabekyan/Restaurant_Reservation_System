CREATE TRIGGER trg_NoOverlap
ON Reservation
FOR INSERT
AS
BEGIN
    IF EXISTS (
        SELECT 1
        FROM Reservation r
        JOIN inserted i
        ON r.TableID = i.TableID
        AND r.ReservationDate = i.ReservationDate
        AND (i.StartTime < r.EndTime AND i.EndTime > r.StartTime)
    )
    BEGIN
        RAISERROR ('Time conflict!', 16, 1);
        ROLLBACK;
    END
END;