using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tranee.servises;
using Tranee.views;
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
            OpenDetailsCommand = new Command<TraningSession>(async (session) => OpenDetails(session));
        }



        public ICommand OpenDetailsCommand { get; }

        private async Task OpenDetails( TraningSession session)
        {
            if (session == null) return;

            await Application.Current.MainPage.Navigation.PushAsync(new SessionDetailsPage(this, session) );
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
