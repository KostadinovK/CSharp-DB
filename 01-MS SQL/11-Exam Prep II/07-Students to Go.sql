SELECT DISTINCT
	s.FirstName + ' ' + s.LastName AS [FullName]
FROM Students s
LEFT JOIN StudentsExams se ON s.Id = se.StudentId
WHERE se.ExamId IS NULL;