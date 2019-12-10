using System;
using System.Collections.Generic;
using System.Text;

namespace StudentExercise.Models
{
    class Cohort
    {
        
        
            public Cohort(int id, string name)
            {
                Id = id;
                Name = name;
            }
            public int Id { get; set; }
            public string Name { get; set; }
        
    }
}
