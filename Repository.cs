using StudentExercise.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace StudentExercise
{
    class Repository
    {
        public SqlConnection Connection
        {
            get
            {
                // This is "address" of the database
                string _connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=StudentExercise;Integrated Security=True";
                return new SqlConnection(_connectionString);
            }
        }


        public List<Exercise> getAllExercises()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {

                    cmd.CommandText = "SELECT Id, Name, Language FROM Exercise";


                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Exercise> exercises = new List<Exercise>();

                    while (reader.Read())
                    {

                        int idColumnPosition = reader.GetOrdinal("Id");


                        int idValue = reader.GetInt32(idColumnPosition);

                        int nameColumnPosition = reader.GetOrdinal("Name");
                        string nameValue = reader.GetString(nameColumnPosition);
                        int languageColumnPosition = reader.GetOrdinal("Language");
                        string languageNameValue = reader.GetString(languageColumnPosition);

                        Exercise exercise = new Exercise(idValue, nameValue, languageNameValue);



                        exercises.Add(exercise);
                    }


                    reader.Close();


                    return exercises;
                }
            }
        }

        public List<Exercise> getAllJavaScriptExercises(string language)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, Name, Language FROM Exercise WHERE Language = @Javascript";
                    cmd.Parameters.Add(new SqlParameter("@Javascript", language));
                    SqlDataReader sqlDataReader = cmd.ExecuteReader();
                    SqlDataReader reader = sqlDataReader;

                    List<Exercise> exercises = new List<Exercise>();

                    while (reader.Read())
                    {
                        int idColumnPosition = reader.GetOrdinal("Id");
                        int idValue = reader.GetInt32(idColumnPosition);

                        int nameColumnPosition = reader.GetOrdinal("Name");
                        string nameValue = reader.GetString(nameColumnPosition);
                        int languageColumnPosition = reader.GetOrdinal("Language");
                        string languageNameValue = reader.GetString(languageColumnPosition);

                        Exercise exercise = new Exercise(idValue, nameValue, languageNameValue);


                        exercises.Add(exercise);
                    }
                    reader.Close();

                    return exercises;
                }
            }
        }

        public void AddExercise(Exercise exercise)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {

                    cmd.CommandText = $"INSERT INTO Exercise (Name, Language) OUTPUT INSERTED.Id Values (@Name, @Language)";
                    cmd.Parameters.Add(new SqlParameter("@Name", exercise.Name));
                    cmd.Parameters.Add(new SqlParameter("@Language", exercise.Language));
                    int id = (int)cmd.ExecuteScalar();

                    exercise.Id = id;

                }
            }

            // when this method is finished we can look in the database and see the new department.
        }

        public List<Instructor> GetAllInstructors()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {

                    cmd.CommandText = "SELECT" +
                        " Instructor.Id," +
                        " Instructor.FirstName," +
                        " Instructor.LastName, " +
                        " Instructor.SlackHandle," +
                        " Instructor.Speciality," +
                        " Instructor.CohortId," +
                        " Cohort.Name " +
                        " FROM Instructor " +
                        " LEFT JOIN  Cohort ON Instructor.CohortId = Cohort.Id";


                    SqlDataReader reader = cmd.ExecuteReader();


                    List<Instructor> instructors = new List<Instructor>();


                    while (reader.Read())
                    {

                        int idColumnPosition = reader.GetOrdinal("Id");
                        int idValue = reader.GetInt32(idColumnPosition);

                        int firstNameColumnPosition = reader.GetOrdinal("FirstName");
                        string firstNameValue = reader.GetString(firstNameColumnPosition);
                        int lastNameColumnPosition = reader.GetOrdinal("LastName");
                        string lastNameValue = reader.GetString(lastNameColumnPosition);
                        int slackHandleColumnPosition = reader.GetOrdinal("SlackHandle");
                        string slackHandleValue = reader.GetString(slackHandleColumnPosition);
                        int specialityColumnPosition = reader.GetOrdinal("Speciality");
                        string specialityValue = reader.GetString(specialityColumnPosition);
                        int cohortIdColumnPosition = reader.GetOrdinal("Name");
                        string cohortValue = reader.GetString(cohortIdColumnPosition);


                        Instructor instructor = new Instructor
                        {
                            Id = idValue,
                            FirstName = firstNameValue,
                            LastName = lastNameValue,
                            SlackHandle = slackHandleValue,
                            Specialty = specialityValue,
                            CohortName = cohortValue
                        };


                        instructors.Add(instructor);
                    }


                    reader.Close();


                    return instructors;
                }
            }
        }

        public void AddInstructor(Instructor instructor)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {

                    cmd.CommandText = "INSERT INTO Instructor (FirstName, LastName, SlackHandle, Speciality, CohortId) OUTPUT INSERTED.Id Values (@firstName, @lastName, @slackHandle, @spec, @cohortId)";
                    cmd.Parameters.Add(new SqlParameter("@firstName", instructor.FirstName));
                    cmd.Parameters.Add(new SqlParameter("@lastName", instructor.LastName));
                    cmd.Parameters.Add(new SqlParameter("@cohortId", instructor.CohortId));
                    cmd.Parameters.Add(new SqlParameter("@spec", instructor.Specialty));
                    cmd.Parameters.Add(new SqlParameter("@slackHandle", instructor.SlackHandle));
                    int id = (int)cmd.ExecuteScalar();

                    instructor.Id = id;

                }
            }
        }

        public void AssignStudentExercise(StudentExercises studentExercises)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {

                    cmd.CommandText = "INSERT INTO StudentExercise (StudentId, ExerciseId) OUTPUT INSERTED.Id Values (@studentId, @exerciseId)";
                    cmd.Parameters.Add(new SqlParameter("@studentId", studentExercises.StudentId));
                    cmd.Parameters.Add(new SqlParameter("@exerciseId", studentExercises.ExerciseId));

                    int id = (int)cmd.ExecuteScalar();

                    studentExercises.Id = id;

                }
            }
        }

        public List<Student> GetAllStudents()
        {

            List<Student> students = new List<Student>();

            using (SqlConnection conn = Connection)
            {

                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {


                    cmd.CommandText = @"SELECT s.Id, s.FirstName, s.LastName, s.SlackHandle, s.CohortId, c.Name AS CohortName, +
                                        e.Id AS ExerciseId, e.Language, e.Name
                                        FROM Student s
                                        INNER JOIN Cohort c On s.CohortId = c.Id
                                        INNER JOIN StudentExercise se ON se.StudentId = s.Id
                                        INNER JOIN Exercise e ON se.ExerciseId = e.Id";



                    SqlDataReader reader = cmd.ExecuteReader();



                    while (reader.Read())
                    {

                        int idColumnPosition = reader.GetOrdinal("Id");
                        int idValue = reader.GetInt32(idColumnPosition);

                        int firstNameColumnPosition = reader.GetOrdinal("FirstName");
                        string firstNameValue = reader.GetString(firstNameColumnPosition);
                        int lastNameColumnPosition = reader.GetOrdinal("LastName");
                        string lastNameValue = reader.GetString(lastNameColumnPosition);
                        int slackHandleColumnPosition = reader.GetOrdinal("SlackHandle");
                        string slackHandleValue = reader.GetString(slackHandleColumnPosition);
                        int cohortsIdColumnPosition = reader.GetOrdinal("CohortId");
                        int cohortsId = reader.GetInt32(cohortsIdColumnPosition);
                        int cohortsNameColumnPosition = reader.GetOrdinal("CohortName");
                        string cohortsName = reader.GetString(cohortsNameColumnPosition);
                        int exerciseIdColumnPosition = reader.GetOrdinal("ExerciseId");
                        int exerciseId = reader.GetInt32(exerciseIdColumnPosition);
                        int exerciseNameColumnPosition = reader.GetOrdinal("Name");
                        string exerciseName = reader.GetString(exerciseNameColumnPosition);
                        int exerciseLanguageColumnPosition = reader.GetOrdinal("Language");
                        string exerciseLanguage = reader.GetString(exerciseLanguageColumnPosition);

                        Cohort newCohort = new Cohort(cohortsId, cohortsName);
                        Student newStudent = new Student(idValue, firstNameValue, lastNameValue, slackHandleValue, newCohort);
                        List<Exercise> exercises = new List<Exercise>();
                        Exercise newExercise = new Exercise(exerciseId, exerciseName, exerciseLanguage);
                        exercises.Add(newExercise);


                       
                        newStudent.Exercises = exercises;
                       





                        students.Add(newStudent);
                    }


             reader.Close();


                 return students;
                }
            }
        }
    }


}



