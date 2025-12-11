

using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tranee.servises;
using Tranee.views;
using TraneeLibrary;

using Set = TraneeLibrary.Set;


namespace Tranee.viewModels
{
    public class AddTrainingViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly NavigationService _serviceNavigation;
        private readonly TrainingService _serviceTraining;

        public ObservableCollection<TraningSession> Sessions { get; set; } = new ObservableCollection<TraningSession>();

         public ICommand AddTrainingCommand { get; }

        public AddTrainingViewModel(TrainingService service)
        {
            //   _serviceNavigation = service;
            _serviceTraining = service;

            AddTrainingCommand = new Command(async () => await AddTraining());
            //LoadDataCommand = new Command(async () => await LoadData());

            Task.Run(LoadData);
        }

     //   public ICommand LoadData { get; }
       

        private async Task LoadData()
        {
            var data = await _serviceTraining.GetAllSessionAsync();

            MainThread.BeginInvokeOnMainThread(() =>
                {
                    Sessions.Clear();
                    foreach (var item in data)
                    {
                        Sessions.Add(item);
                    }
                });

        }

        private async Task AddTraining()
        {

            // Create a realistic training session with multiple exercises and sets.
            var session = new TraningSession()
            {
                Date = DateTime.Now,
                Quality = 8,
                RestBetweenExercise = 90,
                Exercises = new List<Exercise>()
            };

            // Squat exercise with three sets
            var squat = new Exercise
            {
                Name = "Barbell Back Squat",
                GroupOfMuscle = "Legs",
                RestBetweenSets = 120,
                TraningSession = session,
                Sets = new List<Set>
                {
                    new Set { Number = 1, Weight = 60, Reps = 8, Quality = 6, Note = "Warmup", TraningSession = session },
                    new Set { Number = 2, Weight = 90, Reps = 5, Quality = 8, Note = null, TraningSession = session },
                    new Set { Number = 3, Weight = 95, Reps = 5, Quality = 8, Note = "Good depth", TraningSession = session }
                }
            };

        // Bench press exercise with three sets
        var bench = new Exercise
        {
            Name = "Barbell Bench Press",
            GroupOfMuscle = "Chest",
            RestBetweenSets = 90,
            TraningSession = session,
            Sets = new List<Set>
                {
                    new Set { Number = 1, Weight = 40, Reps = 8, Quality = 6, Note = "Warmup", TraningSession = session },
                    new Set { Number = 2, Weight = 60, Reps = 6, Quality = 7, Note = null, TraningSession = session },
                    new Set { Number = 3, Weight = 65, Reps = 5, Quality = 8, Note = "Controlled", TraningSession = session }
                }
        };

            session.Exercises.Add(squat);
            session.Exercises.Add(bench);

        await _serviceTraining.AddSessionAsync(session);
            await LoadData();
        }
    }
}
