ALTER TABLE Reservation
ADD CONSTRAINT FK_Customer_Reservation
FOREIGN KEY (CustomerID)
REFERENCES Customer(CustomerID)
ON DELETE CASCADE;