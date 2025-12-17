using Tranee.viewModels;
using Tranee.views;
using Microsoft.Maui.Controls;
using Tranee.servises;
using Tranee.viewModels;


namespace Tranee.views
{
    public partial class AddTrainPage : ContentPage
    {

        public AddTrainPage(AddTrainingViewModel viewModel )
        {
            InitializeComponent();

            BindingContext = viewModel;

         
        }
    }
}

