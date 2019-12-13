using System;
using System.Collections.Generic;
using System.Text;

namespace StudentExercise.Models
{
    class Student
    {
        
        public Student(int id, string firstName, string lastname, string slack)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastname;
            SlackHandle = slack;
        }

        public Student(int id, string firstName, string lastname, string slack, Cohort cohort)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastname;
            SlackHandle = slack;
            CohortId = cohort;
        }
        

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SlackHandle { get; set; }
        public Cohort CohortId { get; set; }


        public List<Exercise> Exercises { get; set; }
    }
}

