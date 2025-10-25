using Tranee.views;
using Microsoft.Maui.Controls;

namespace Tranee
{
    public partial class MainPage : ContentPage
    {
        

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OpenCurrentSchemaPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CurrentSchemaPage());
        }

     
    }
}
