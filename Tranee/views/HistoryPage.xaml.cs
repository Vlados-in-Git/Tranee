using Tranee.viewModels;

namespace Tranee.views
{
    public partial class HistoryPage : ContentPage
    {
        // 1. Поле оголошуємо всередині класу, а не всередині методів
        private readonly HistoryViewModel _vm;

        // 2. Залишаємо тільки ОДИН конструктор, який приймає ViewModel
        public HistoryPage(HistoryViewModel vm)
        {
            InitializeComponent();

            // Зберігаємо та прив'язуємо ViewModel
            _vm = vm;
            BindingContext = _vm;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Викликаємо команду завантаження
            if (BindingContext is HistoryViewModel vm)
            {
                vm.LoadHistoryCommand.Execute(null);
            }
        }
    }
}   