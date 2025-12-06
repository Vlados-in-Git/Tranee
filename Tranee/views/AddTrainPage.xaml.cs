using Tranee.viewModels;
using Tranee.views;
using Microsoft.Maui.Controls;
using Tranee.servises;
using Tranee.viewModels;

namespace Tranee.views
{
    public partial class AddTrainPage : ContentPage
    {

        public AddTrainPage(AddTrainingViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = viewModel;
            /*
            // створив обєкт сервісу навігації, щоб мати доступ до команд, передав аргументом властивість MainPage.Navigation щоб мати доступ до цієї сторінки
            var trainingService = new TrainingService();

            // через BC створюю обєкт VM і передаю туди Servises, звязуючи V - VM - Service
            BindingContext = new AddTrainingViewModel(trainingService); */
        }
    }
}

