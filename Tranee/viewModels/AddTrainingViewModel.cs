
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


namespace Tranee.viewModels
{
    internal class AddTrainingViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly NavigationService _serviceNavigation;
        private readonly TrainingService _serviceTraining;

        public ObservableCollection<TraningSession> Sessions { get; set; } = new ObservableCollection<TraningSession>();

        public AddTrainingViewModel(TrainingService service)
        {
            //   _serviceNavigation = service;
            _serviceTraining = service;

            AddTrainingCommand = new Command(async () => await AddTraining());
            LoadDataCommand = new Command(async () => await LoadData());

            Task.Run(LoadData);
        }

        public ICommand LoadData { get; }
        public ICommand AddTraining { get; }

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
            var newTraining = new TraningSession()
            {
                Date = DateTime.Now,
                Quality = 10,
                Exercises = new List<Exercise> { new Exercise { Name = "Squat MVVM" } }
            };
            await _serviceTraining.AddSessionAsync(newTraining);
            await LoadData();
        }
    }
}
