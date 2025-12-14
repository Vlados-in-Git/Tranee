

using Tranee.viewModels;

namespace Tranee.views;

public partial class CreatingTemplatePage : ContentPage
{
	public CreatingTemplatePage( CreatingTemplatePageViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}