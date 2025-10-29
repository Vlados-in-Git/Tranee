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
        private readonly INavigation _navigation;

        public NavigationService(INavigation navigation)
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
        }
    }
}
