
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TraneeLibrary
{
   
    public class TraningSession
    {

        
        public int Id { get; set; }
        public DateTime Date { get; set; }

        public int? TrainingTemplateId { get; set; }
        public TrainingTemplate? TrainingTemplate { get; set; }

        public int Quality { get; set; } 

        public int RestBetweenExercise { get; set; }

        public List<Exercise> Exercises { get; set; } = new List<Exercise>();


    }

    public class Exercise 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string GroupOfMuscle { get; set; } 
        public int RestBetweenSets { get; set; } 


        public int TraningSessionId { get; set; }
        public TraningSession TraningSession { get; set; } = null!;


        public ObservableCollection<Set> Sets { get; set; } = new ObservableCollection<Set>();
    }

    public class Set: INotifyPropertyChanged
    {
        public int Id { get; set; }
        public int Number  { get; set; }
        public int Weight  { get; set; }
        public int Reps    { get; set; }
        public int Quality { get; set; }

        private string? _note;
        public string? Note
        {
            get => _note;
            set
            {
                if (_note != value)
                {
                    _note = value;
                    OnPropertyChanged();
                }
            }
        }

        public int ExerciseId { get; set; }
        public Exercise Exercise { get; set; } = null!;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}