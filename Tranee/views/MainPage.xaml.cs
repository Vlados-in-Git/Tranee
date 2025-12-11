using Tranee.views;
using Microsoft.Maui.Controls;
using Tranee.servises;
using Tranee.viewModels;

namespace Tranee
{
    public partial class MainPage : ContentPage
    {
        

        public MainPage(MainPageViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = viewModel;
            /*
            // створив обєкт сервісу навігації, щоб мати доступ до команд, передав аргументом властивість MainPage.Navigation щоб мати доступ до цієї сторінки
             var navigationService = new NavigationService(this.Navigation);

            // через BC створюю обєкт VM і передаю туди Servises, звязуючи V - VM - Service
            BindingContext = new MainPageViewModel(navigationService); */
        }



     
    }
}
