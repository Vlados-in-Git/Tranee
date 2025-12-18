using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

using System.Runtime.CompilerServices;
using System.Windows.Input;
using Tranee.servises;
using Tranee.views;

namespace Tranee.viewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private readonly NavigationService _service;

      

        public MainPageViewModel(NavigationService service)
        {
            _service = service;

            OpenCurrentSchemaPage = new Command(async () => await NavigateToCurrentSchemaPage());
            OpenAnalizePage = new Command(async () => await NavigateToAnalizePage());
            // OpenAddTrainingPage = new Command(async () => await NavigateToAddTrainingPage());
            OpenAddTrainingPage = new Command(async () => await NavigateToHistoryPage());
        }


        public ICommand OpenHistoryPage { get;  }

        private async Task NavigateToHistoryPage()
        {
            await _service.NavigateTo<HistoryPage>();
        }

        
        public ICommand OpenAddTrainingPage { get; }
        private async Task NavigateToAddTrainingPage()
        {
            
         
            await _service.NavigateTo<AddTrainPage>();
        }

        
        public ICommand OpenAnalizePage { get; }
        private async Task NavigateToAnalizePage()
        {
           
            await _service.NavigateTo<AnalyticsPage>();
        }

        
        public ICommand OpenCurrentSchemaPage { get; }
        private async Task NavigateToCurrentSchemaPage()
        {
           
            await _service.NavigateTo<CurrentSchemaPage>();
        }

        
      






    }
}
