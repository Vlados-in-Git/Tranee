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

        /*
         public MainPageViewModel(NavigationService service)
         {
             _service = service;

             OpenCurrentSchemaPage = new Command(async () => await NavigateToCurrentSchemaPage());

             OpenNewSchemaPage = new Command(async () => await NavigateToNewSchemaPage());

             OpenAnalizePage = new Command(async () => await NavigateToAnalizePage());

             OpenAddTrainingPage = new Command(async () => await NavigateToAddTrainingPage());            
         }

         public ICommand OpenAddTrainingPage { get; }

         private async Task NavigateToAddTrainingPage()
         {
             await _service.NavigateTo(new AddTrainPage() );
         }
         public ICommand OpenAnalizePage { get; }

         private async Task NavigateToAnalizePage()
         {
             await _service.NavigateTo(new AnalizePage());
         }
         public ICommand OpenCurrentSchemaPage { get; } // public щоб binding у view міг знайти команду

         private async Task NavigateToCurrentSchemaPage()
         {
             await _service.NavigateTo(new CurrentSchemaPage());
         }

         public ICommand OpenNewSchemaPage { get; }

         private async Task NavigateToNewSchemaPage()
         {
             await _service.NavigateTo(new NewSchemaPage());

         } */

        public MainPageViewModel(NavigationService service)
        {
            _service = service;

            OpenCurrentSchemaPage = new Command(async () => await NavigateToCurrentSchemaPage());
            OpenNewSchemaPage = new Command(async () => await NavigateToNewSchemaPage());
            OpenAnalizePage = new Command(async () => await NavigateToAnalizePage());
            OpenAddTrainingPage = new Command(async () => await NavigateToAddTrainingPage());
        }

        // --- AddTrainingPage ---
        public ICommand OpenAddTrainingPage { get; }
        private async Task NavigateToAddTrainingPage()
        {
            // БУЛО: await _service.NavigateTo(new AddTrainPage());
            // СТАЛО:
            await _service.NavigateTo<AddTrainPage>();
        }

        // --- AnalizePage ---
        public ICommand OpenAnalizePage { get; }
        private async Task NavigateToAnalizePage()
        {
            // СТАЛО:
            await _service.NavigateTo<AnalizePage>();
        }

        // --- CurrentSchemaPage ---
        public ICommand OpenCurrentSchemaPage { get; }
        private async Task NavigateToCurrentSchemaPage()
        {
            // СТАЛО:
            await _service.NavigateTo<CurrentSchemaPage>();
        }

        // --- NewSchemaPage ---
        public ICommand OpenNewSchemaPage { get; }
        private async Task NavigateToNewSchemaPage()
        {
            // СТАЛО:
            await _service.NavigateTo<NewSchemaPage>();
        }






    }
}
