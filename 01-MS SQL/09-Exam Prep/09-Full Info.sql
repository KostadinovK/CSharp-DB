SELECT
	p.FirstName + ' ' + p.LastName AS [Full Name],
	pl.[Name] AS [Plane Name],
	f.Origin + ' - ' + f.Destination AS Trip,
	lt.[Type] AS [Luggage Type]
FROM Passengers p
JOIN Tickets t ON p.Id = t.PassengerId
JOIN Flights f ON t.FlightId = f.Id
JOIN Planes pl ON f.PlaneId = pl.Id
JOIN Luggages l ON l.Id = t.LuggageId
JOIN LuggageTypes lt ON lt.Id = l.LuggageTypeId
ORDER BY [Full Name], pl.[Name], f.Origin, f.Destination, lt.[Type];