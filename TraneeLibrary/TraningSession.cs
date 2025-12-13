
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace TraneeLibrary
{
    //class wich discrabe a traning session in general
    public class TraningSession
    {

        // TODO: To relate a traningSession with TrainingTemplate
        public int Id { get; set; }
        public DateTime Date { get; set; } // date of training

        public int? TrainingTemplateId { get; set; }
        public TrainingTemplate? TrainingTemplate { get; set; }

        public int Quality { get; set; } // TODO: make a structure( from easy to hard), show how hard train was

        public int RestBetweenExercise { get; set; }

        public List<Exercise> Exercises { get; set; } = new List<Exercise>();

        // public Exercise 
        // make a Sets as a new object, so group of similars object will represent a all training


    }

    public class Exercise // 
    {
        public int Id { get; set; }
        public string Name { get; set; } // name of exercise
        public string GroupOfMuscle { get; set; } // name on wich group of muscle train this exercise TODO: make that group of muscle could be several in one record
        public int RestBetweenSets { get; set; } // Time of rest between sets

        // make a default weight ( like weight only griph) ?


        public int TraningSessionId { get; set; }
        public TraningSession TraningSession { get; set; } = null!;


        public ObservableCollection<Set> Sets { get; set; } = new ObservableCollection<Set>();
    }

    public class Set // TODO: make Inherit from Exercise
    {
        public int Id { get; set; }
        public int Number  { get; set; }// it first or second or ... set
        public int Weight  { get; set; }// how match Kg you gain in Set;
        public int Reps    { get; set; }// how many time you gained weight;
        public int Quality { get; set; }// show how hard train was TODO: Make a Quality as particular structure to use once in several calsess
        public string? Note { get; set; }// A note about this set ( might be null)

        public int ExerciseId { get; set; }
        public Exercise Exercise { get; set; } = null!;

    }
}