CREATE VIEW ActiveReservations AS
SELECT r.ReservationID, c.Name, t.TableID, r.ReservationDate
FROM Reservation r
JOIN Customer c ON r.CustomerID = c.CustomerID
JOIN [Table] t ON r.TableID = t.TableID
WHERE r.Status = 'Confirmed';