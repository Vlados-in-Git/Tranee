using Tranee.viewModels;

namespace Tranee.views
{
    public partial class HistoryPage : ContentPage
    {
       
        private readonly HistoryViewModel _vm;

       
        public HistoryPage(HistoryViewModel vm)
        {
            InitializeComponent();

           
            _vm = vm;
            BindingContext = _vm;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

           
            if (BindingContext is HistoryViewModel vm)
            {
                vm.LoadHistoryCommand.Execute(null);
            }
        }
    }
}   