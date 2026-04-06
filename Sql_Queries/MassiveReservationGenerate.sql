DECLARE @i INT = 0;

WHILE @i < 100
BEGIN
    BEGIN TRY

        DECLARE @customerId INT =
            (SELECT TOP 1 CustomerID FROM Customer ORDER BY NEWID());

        DECLARE @tableId INT =
            (SELECT TOP 1 TableID FROM [Table] ORDER BY NEWID());

        DECLARE @staffId INT =
            (SELECT TOP 1 StaffID FROM Staff ORDER BY NEWID());

        DECLARE @date DATE = DATEADD(DAY, ABS(CHECKSUM(NEWID())) % 10, '2026-04-10');

        DECLARE @startHour INT = 12 + (ABS(CHECKSUM(NEWID())) % 8);
        DECLARE @duration INT = 1 + (ABS(CHECKSUM(NEWID())) % 3);

        DECLARE @start TIME = TIMEFROMPARTS(@startHour, 0, 0, 0, 0);
        DECLARE @end TIME = TIMEFROMPARTS(@startHour + @duration, 0, 0, 0, 0);

        DECLARE @guests INT = 1 + (ABS(CHECKSUM(NEWID())) % 6);

        INSERT INTO Reservation (
            CustomerID,
            TableID,
            StaffID,
            ReservationDate,
            StartTime,
            EndTime,
            GuestCount,
            Status
        )
        VALUES (
            @customerId,
            @tableId,
            @staffId,
            @date,
            @start,
            @end,
            @guests,
            'Confirmed'
        );

        SET @i = @i + 1;

    END TRY
    BEGIN CATCH
    END CATCH
END