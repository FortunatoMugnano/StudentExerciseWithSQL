﻿using System;
using System.Collections.Generic;
using System.Text;

namespace StudentExercise.Models
{
    class Exercise
    {
        public Exercise(int id, string name, string language)
        {
            Id = id;
            Name = name;
            Language = language;


        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Language { get; set; }
       

      
    }
}
