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
           
        }



     
    }
}
