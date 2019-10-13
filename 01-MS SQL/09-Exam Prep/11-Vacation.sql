CREATE FUNCTION udf_CalculateTickets(@origin VARCHAR(40), @destination VARCHAR(40), @peopleCount INT) 
RETURNS VARCHAR(40) AS
BEGIN
	IF(@peopleCount <= 0)
	BEGIN
		RETURN 'Invalid people count!'
	END

	IF(NOT EXISTS(SELECT * FROM Flights WHERE Destination = @destination AND Origin = @origin))
	BEGIN
		RETURN 'Invalid flight!'
	END

	DECLARE @ticketPrice DECIMAL(18, 2) = 
	(
		SELECT t.Price 
		FROM Flights f
		JOIN Tickets t ON f.Id = t.FlightId
		WHERE Destination = @destination AND Origin = @origin
	);

	DECLARE @totalPrice DECIMAL(18, 2) = @ticketPrice * @peopleCount;

	RETURN 'Total price ' + CAST(@totalPrice AS VARCHAR(30))
END