

SELECT Student.Id, Student.FirstName, Student.LastName, Student.SlackHandle, Student.CohortId, Cohort.Id, Cohort.Name
FROM Student 
LEFT JOIN  StudentExercise ON Student.Id = StudentExercise.Id
RIGHT JOIN Cohort On Student.CohortId = Cohort.Id