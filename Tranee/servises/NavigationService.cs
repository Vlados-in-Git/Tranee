using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Maui.Controls;
using Tranee.viewModels;
using Tranee.views;


namespace Tranee.servises
{
    public class NavigationService
    {
    

        private readonly IServiceProvider _serviceProvider;
        public NavigationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public async Task NavigateTo<TPage>() where TPage : Page
        {
           
            var page = _serviceProvider.GetRequiredService<TPage>();

           
            var navigation = Application.Current.MainPage.Navigation;

           
            await navigation.PushAsync(page);
        }

        public async Task NavigateTo<Tpage>(int parameter) where Tpage : Page
        {
            var page = _serviceProvider.GetRequiredService<Tpage>();

            if(page.BindingContext is ActiveTraningViewModel viewModel)
            {
                await viewModel.Initialize(parameter);
            }

            await Application.Current.MainPage.Navigation.PushAsync(page);
        }


    
    }
}
