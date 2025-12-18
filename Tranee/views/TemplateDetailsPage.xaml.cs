
using Tranee.viewModels;
using TraneeLibrary;

namespace Tranee.views;


public partial class TemplateDetailsPage : ContentPage
{
	private TrainingTemplate _trainingTemplate;
	private AddNewSchemaViewModel _viewModel;
	public TemplateDetailsPage(AddNewSchemaViewModel viewModel, TrainingTemplate trainingTemplate)
	{
		InitializeComponent();

		_trainingTemplate = trainingTemplate;
		_viewModel = viewModel;

		BindingContext = trainingTemplate;

	}

    private void Button_Clicked(object sender, EventArgs e)
    {
        
        if (_viewModel.StartTraningByTemplate.CanExecute(_trainingTemplate))
        {
            _viewModel.StartTraningByTemplate.Execute(_trainingTemplate);
        }
    }
}