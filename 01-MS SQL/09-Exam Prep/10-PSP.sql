SELECT
	p.[Name], p.Seats, COUNT(t.PassengerId) AS [Passengers Count]
FROM Planes p
LEFT JOIN Flights f ON p.Id = f.PlaneId
LEFT JOIN Tickets t ON f.Id = t.FlightId
GROUP BY p.[Name], p.Seats
ORDER BY COUNT(t.PassengerId) DESC, p.[Name], p.Seats;