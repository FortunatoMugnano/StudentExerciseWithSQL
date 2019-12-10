using System;
using System.Collections.Generic;
using System.Text;

namespace StudentExercise.Models
{
    class StudentExercises
    {
        /*
        public StudentExercises(int id)
        {
            Id = id;
        }
        public StudentExercises(int id, Student student, Exercise exercise)
        {
            Id = id;
            StudentId = student;
            ExerciseId = exercise;
        }
        */
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int ExerciseId { get; set; }

    }
}
