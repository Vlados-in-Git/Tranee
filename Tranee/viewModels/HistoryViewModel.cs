using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tranee.servises;
using TraneeLibrary;

namespace Tranee.viewModels
{
    public class HistoryViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        private readonly NavigationService _navigationService;
        private readonly TrainingService _trainingService;

        public ObservableCollection<TraningSession> HistorySessions { get; set; } = new();

        public HistoryViewModel(NavigationService navigation, TrainingService trainingService)
        {
            _navigationService = navigation;
            _trainingService = trainingService;

            LoadHistoryCommand = new Command(async () => await LoadData());
        }

        public ICommand LoadHistoryCommand { get; }

        private async Task LoadData()
        {
            var data = await _trainingService.GetHistoryAsync();

            MainThread.BeginInvokeOnMainThread(() =>
            {
                HistorySessions.Clear();
                foreach (var item in data)
                {
                    HistorySessions.Add(item);
                }
            });
        }
    }
}
