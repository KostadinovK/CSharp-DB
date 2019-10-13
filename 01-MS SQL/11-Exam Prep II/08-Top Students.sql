SELECT TOP(10)
	s.FirstName, s.LastName, CAST(AVG(se.Grade) AS DECIMAL(18, 2)) AS Grade
FROM Students s
JOIN StudentsExams se ON s.Id = se.StudentId
GROUP BY s.FirstName, s.LastName
ORDER BY AVG(se.Grade) DESC, s.FirstName, s.LastName