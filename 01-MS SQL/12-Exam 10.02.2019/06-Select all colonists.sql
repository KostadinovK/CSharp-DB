SELECT
	Id,
	FirstName + ' ' + LastName AS FullName,
	Ucn
FROM Colonists
ORDER BY FirstName, LastName, Id;