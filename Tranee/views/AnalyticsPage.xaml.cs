using Tranee.viewModels;

namespace Tranee.views;

public partial class AnalyticsPage : ContentPage
{
    // Отримуємо ViewModel автоматично
    public AnalyticsPage(AnalyticsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}