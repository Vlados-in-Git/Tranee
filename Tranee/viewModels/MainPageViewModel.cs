using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Windows.Input;
using Tranee.servises;
using Tranee.views;

namespace Tranee.viewModels
{
    internal class MainPageViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private readonly NavigationService _service;

        public ICommand OpenCurrentSchemaPage { get; } // public щоб binding у view міг знайти команду
        public MainPageViewModel(NavigationService service)
        {
            _service = service;

            OpenCurrentSchemaPage = new Command(async () => await NavigateToCurrentSchemaPage());

        }

       private async Task NavigateToCurrentSchemaPage()
        {
            await _service.NavigateTo(new CurrentSchemaPage());
        }








    }
}
