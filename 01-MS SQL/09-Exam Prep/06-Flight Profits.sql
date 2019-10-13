SELECT
	t.FlightId, SUM(t.Price) AS Price
FROM Tickets t
GROUP BY t.FlightId
ORDER BY SUM(t.Price) DESC, t.FlightId;