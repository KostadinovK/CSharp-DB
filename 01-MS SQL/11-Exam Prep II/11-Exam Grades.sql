CREATE FUNCTION udf_ExamGradesToUpdate(@studentId INT, @grade DECIMAL(18, 2))
RETURNS VARCHAR(100) AS
BEGIN
	IF(@grade > 6.00)
	BEGIN
		RETURN 'Grade cannot be above 6.00!'
	END

	IF(NOT EXISTS(SELECT * FROM Students WHERE Id = @studentId))
	BEGIN
		RETURN 'The student with provided id does not exist in the school!'
	END

	DECLARE @grades INT = 
	(
		SELECT 
			COUNT(se.Grade) 
		FROM Students s
		JOIN StudentsExams se ON s.Id = se.StudentId
		WHERE se.Grade >= @grade AND se.Grade <= (@grade + 0.5)
		GROUP BY s.Id
		HAVING s.Id = @studentId 
	);

	DECLARE @name NVARCHAR(40) = (SELECT FirstName FROM Students WHERE Id = @studentId)

	RETURN 'You have to update ' + CAST(@grades AS VARCHAR(10)) + ' grades for the student ' + @name
END;