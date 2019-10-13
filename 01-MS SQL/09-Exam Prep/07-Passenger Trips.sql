SELECT
	p.FirstName + ' ' + p.LastName AS [Full Name],
	f.Origin,
	f.Destination
FROM Passengers p
JOIN Tickets t ON p.Id = t.PassengerId
JOIN Flights f ON t.FlightId = f.Id
ORDER BY [Full Name], f.Origin, f.Destination;