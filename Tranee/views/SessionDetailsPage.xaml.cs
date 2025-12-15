using Tranee.viewModels;
using TraneeLibrary;

namespace Tranee.views;

public partial class SessionDetailsPage : ContentPage
{
	private TraningSession _session;
	private HistoryViewModel _viewModel;

	public SessionDetailsPage(HistoryViewModel viewModel, TraningSession session)
	{
		InitializeComponent();

		_viewModel = viewModel;
		_session = session;

		BindingContext = session;

    }
}