

SELECT s.Id, s.FirstName, s.LastName, s.SlackHandle, s.CohortId, c.Name AS CohortName, +
e.Id AS ExerciseId, e.Language, e.Name
FROM Student s
INNER JOIN Cohort c On s.CohortId = c.Id
INNER JOIN StudentExercise se ON se.StudentId = s.Id
INNER JOIN Exercise e ON se.ExerciseId = e.Id




