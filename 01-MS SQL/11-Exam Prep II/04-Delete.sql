DELETE StudentsTeachers
WHERE TeacherId IN (SELECT Id FROM Teachers WHERE CHARINDEX('72', Phone) > 0)

DELETE Teachers
WHERE CHARINDEX('72', Phone) > 0