DELETE t FROM Tickets t
JOIN Flights f ON t.FlightId = f.Id
WHERE f.Destination = 'Ayn Halagim'

DELETE Flights
WHERE Destination = 'Ayn Halagim'