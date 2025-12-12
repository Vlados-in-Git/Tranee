using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Tranee.servises;
using TraneeLibrary;

namespace Tranee.viewModels
{
    public class ActiveTraningViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private TraningSession _currentSession;
        private readonly TrainingService _trainingService;

        public TraningSession CurrentSession
        {
            get => _currentSession;

            set
            {
                _currentSession = value;
                OnPropertyChanged();
            }
               
        }

        public ActiveTraningViewModel(TrainingService trainingService)
        {
            _trainingService = trainingService;
        }
        public async Task Initialize(int id)
        {
            CurrentSession = await _trainingService.GetSessionByIdAsync(id);
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
