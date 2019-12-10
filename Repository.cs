using StudentExercise.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

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
                       
                        Exercise exercise = new Exercise
                        {
                            Id = idValue,
                            Name = nameValue,
                            Language = languageNameValue,
                         
                        };

                        
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
                    cmd.Parameters.Add(new SqlParameter("@Javascript", language ));
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
                        
                        Exercise exercise = new Exercise
                        {
                            Id = idValue,
                            Name = nameValue,
                            Language = languageNameValue,

                        };

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

        public void AssignStudentExercise( StudentExercises studentExercises)
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
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {

                    cmd.CommandText = "SELECT" +
                        " Student.Id," +
                        " Student.FirstName," +
                        " Student.LastName, " +
                        " Student.SlackHandle," +
                        " Student.CohortId," +
                        " Cohort.Name," +
                        " StudentExercise.Id"+
                        " FROM Student " +
                        " LEFT JOIN  Cohort ON Student.CohortId = Cohort.Id" +
                        " LEFT JOIN Exercise On StudentExercise.StudentId = Student.Id" ;

                    


                    SqlDataReader reader = cmd.ExecuteReader();


                    List<Student> students = new List<Student>();


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
                        //int exerciseColumnPosition = reader.GetOrdinal("Name");
                        //int exerciseIdValue = reader.GetInt32(exerciseColumnPosition);
                        int cohortIdColumnPosition = reader.GetOrdinal("Name");
                        string cohortValue = reader.GetString(cohortIdColumnPosition);

                        //StudentExercises exer = new StudentExercises();
                        Student student = new Student
                        {
                            Id = idValue,
                            FirstName = firstNameValue,
                            LastName = lastNameValue,
                            SlackHandle = slackHandleValue,
                            CohortName = cohortValue,
                            
                        };

                       


                        students.Add(student);
                    }


                    reader.Close();


                    return students;
                }
            }
        }


    }

}
