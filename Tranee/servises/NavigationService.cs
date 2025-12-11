using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Maui.Controls;


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
            // 1. Просимо контейнер створити сторінку з усіма залежностями
            var page = _serviceProvider.GetRequiredService<TPage>();

            // 2. Отримуємо поточну навігацію (MainPage має бути в NavigationPage)
            var navigation = Application.Current.MainPage.Navigation;

            // 3. Переходимо
            await navigation.PushAsync(page);
        }



       /* public NavigationService(INavigation navigation)
        {
            _navigation = navigation;
        }

        public async Task NavigateTo(Page page)
        {
            await _navigation.PushAsync(page);
        }

        public async Task GoBack()
        {
            await _navigation.PopAsync();
        }  */
    }
}
