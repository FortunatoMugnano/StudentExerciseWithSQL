using StudentExercise.Models;
using System;
using System.Collections.Generic;

namespace StudentExercise
{
    class Program
    {
        static void Main(string[] args)
        {
            Repository exerciseRepository = new Repository();

            //QUERY ALL THE EXERCISES

            Console.WriteLine("Getting All the Exercises:");
            Console.WriteLine("-------------------------");

            List<Exercise> allExercises = exerciseRepository.getAllExercises();

            foreach (Exercise ex in allExercises)
            {
                Console.WriteLine($"{ex.Id}: {ex.Name}, {ex.Language}");
            }

            //QUERY ALL THE JAVASCRIPT EXERCISES

            Console.WriteLine("");
            Console.WriteLine("Getting All the Javascript Exercises:");
            Console.WriteLine("-------------------------");

            List<Exercise> allJsExercises = exerciseRepository.getAllJavaScriptExercises("Javascript");

            foreach (Exercise ex in allJsExercises)
            {
                Console.WriteLine($"{ex.Id}: {ex.Name}, {ex.Language}");
            }

            //QUERY ALL THE INSTRUCTORS WITH THE COHORTS NAMES


            Console.WriteLine("");
            Console.WriteLine("Getting All the Instructors:");
            Console.WriteLine("-------------------------");

            List<Instructor> allInstructors = exerciseRepository.GetAllInstructors();

            foreach (Instructor inst in allInstructors)
            {
                Console.WriteLine($"{inst.Id}: {inst.FirstName} {inst.LastName}, Slach: {inst.SlackHandle}, Speciality: {inst.Specialty}. Theacher in {inst.CohortName}");
            }

            //QUERY ALL THE STUDENTS WITH THE COHORTS NAMES AND EXERCISES


            List<Student> studentsWithExercises = exerciseRepository.GetAllStudents();
            foreach (Student stud in studentsWithExercises)
            {
                Console.WriteLine("__________________________________________________");
                Console.WriteLine($"Student:{stud.Id}: {stud.FirstName} {stud.LastName}. Slack: {stud.SlackHandle}");
                Console.WriteLine($"Cohort:{stud.CohortId.Name}");
                Console.WriteLine($"Assignments: {stud.Exercises.Count}");
                foreach (Exercise exer in stud.Exercises)
                {
                    Console.WriteLine($"Exercise: {exer.Name} Language: {exer.Language}");
                }

            }

            //ADDING MORE EXERCISES

            /*
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            var newExercise = new Exercise();
            Console.WriteLine("New Exercise Name");
            Console.Write(">");
            newExercise.Name = Console.ReadLine();
            Console.WriteLine("New Exercise Language");
            Console.Write(">");
            newExercise.Language = Console.ReadLine();


            exerciseRepository.AddExercise(newExercise);



            //ADDING MORE INSTRUCTORS
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            var newInstructor = new Instructor();
            Console.WriteLine("New Instructor First Name");
            Console.Write(">");
            newInstructor.FirstName = Console.ReadLine();
            Console.WriteLine("New Instructor Last Name");
            Console.Write(">");
            newInstructor.LastName = Console.ReadLine();
            Console.WriteLine("New Instructor Slack Handle");
            Console.Write(">");
            newInstructor.SlackHandle = Console.ReadLine();
            Console.WriteLine("New Instructor Speciality");
            Console.Write(">");
            newInstructor.Specialty = Console.ReadLine();
            Console.WriteLine("New Employee Cohort Id Number");
            Console.Write(">");
            newInstructor.CohortId = int.Parse(Console.ReadLine());

            exerciseRepository.AddInstructor(newInstructor);

            

            //ASSIGNING MORE EXERCISES


            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            var assignExercise = new StudentExercises();
            Console.WriteLine("Student Id:");
            Console.Write(">");
            assignExercise.StudentId = int.Parse(Console.ReadLine());
            Console.WriteLine("Exercise Id:");
            Console.Write(">");
            assignExercise.ExerciseId = int.Parse(Console.ReadLine());


            exerciseRepository.AssignStudentExercise(assignExercise);
            */
        }
    }
}
