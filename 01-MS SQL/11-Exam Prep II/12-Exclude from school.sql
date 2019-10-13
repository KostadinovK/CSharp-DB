CREATE PROC usp_ExcludeFromSchool(@studentId INT) AS
BEGIN
	IF(NOT EXISTS(SELECT * FROM Students WHERE Id = @studentId))
	BEGIN
		RAISERROR('This school has no student with the provided id!', 16, 1)
	END

	DELETE FROM StudentsSubjects
	WHERE StudentId = @studentId

	DELETE FROM StudentsExams
	WHERE StudentId = @studentId

	DELETE FROM StudentsTeachers
	WHERE StudentId = @studentId

	DELETE FROM Students
	WHERE Id = @studentId
END;