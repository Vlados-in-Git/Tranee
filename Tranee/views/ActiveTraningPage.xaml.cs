using Tranee.viewModels;

namespace Tranee.views;

public partial class ActiveTraningPage : ContentPage
{
	public ActiveTraningPage(ActiveTraningViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}