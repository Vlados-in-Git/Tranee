using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tranee.servises;
using Tranee.views;
using TraneeLibrary;
using static System.Collections.Specialized.BitVector32;

namespace Tranee.viewModels
{
    public class AddNewSchemaViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly SchemaService _SchemaService;
        private readonly IServiceProvider _serviceProvider;
        private readonly NavigationService _navigationService;
        public ObservableCollection<TrainingTemplate> Templates { get; set; } = new ObservableCollection<TrainingTemplate>();

        public ICommand AddTestSchemaCommand { get; }
        public AddNewSchemaViewModel(SchemaService service, IServiceProvider serviceProvider, NavigationService navigation)
        {
            _SchemaService = service;
            _serviceProvider = serviceProvider;
            _navigationService = navigation;

            AddTestSchemaCommand = new Command(async () => await AddSchema() );
            StartTraningByTemplate = new Command<TrainingTemplate>(async (template) => await StartTraning( template ));
            OpenDetailsCommand = new Command<TrainingTemplate>(async (template)  => await OpenDetails(template));



        }

        public ICommand OpenDetailsCommand { get; }
        public Command<TrainingTemplate> StartTraningByTemplate { get; }

        private async Task StartTraning(TrainingTemplate template)
        {
            if (template == null) return;

            int newSessionId = await _SchemaService.StartSessionFromTemplateAsync(template.Id);        

            if (newSessionId == -1) return;

            var activePage = _serviceProvider.GetService<ActiveTraningPage>();      

            if (activePage.BindingContext is ActiveTraningViewModel targetViewModel)
            {
                await targetViewModel.Initialize(newSessionId);
            }

            await Application.Current.MainPage.Navigation.PushAsync(activePage); 

        }

        private async Task OpenDetails(TrainingTemplate template)
        {
            if (template == null) return;

           
            await Application.Current.MainPage.Navigation.PushAsync(new TemplateDetailsPage(this, template));
        }

        public async Task LoadData()
        {
            var data = await _SchemaService.GetAllTrainingTemplatesAsync();

            MainThread.BeginInvokeOnMainThread(() =>
            {
                Templates.Clear();
                foreach (var item in data)
                {
                    Templates.Add(item);
                }
            });

        }

        
        private async Task AddSchema()
        {


            await _navigationService.NavigateTo<CreatingTemplatePage>();
           
            
        }
    }
}
