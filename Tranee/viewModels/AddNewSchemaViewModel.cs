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

            int newSessionId = await _SchemaService.StartSessionFromTemplateAsync(template.Id);         //create session in DB

            if (newSessionId == -1) return;

            var activePage = _serviceProvider.GetService<ActiveTraningPage>();      //create page

            if (activePage.BindingContext is ActiveTraningViewModel targetViewModel)
            {
                await targetViewModel.Initialize(newSessionId); // create data in VM
            }

            await Application.Current.MainPage.Navigation.PushAsync(activePage); //push page

        }

        private async Task OpenDetails(TrainingTemplate template)
        {
            if (template == null) return;

            // Переходимо на сторінку деталей.
            // Передаємо туди шаблон (щоб показати дані)
            // І передаємо 'this' (цю ViewModel), щоб кнопка "Почати" там працювала
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
            /*

            // Створюємо тестовий об'єкт шаблону тренування
            var newTrainingTemplate = new TrainingTemplate
            {
                Name = "Фулбаді для початківців",
                Description = "Базове тренування на все тіло, акцент на великі групи м'язів",
                RestBetweenSets = 90, // 1.5 хвилини відпочинку між вправами

                // Додаємо список вправ
                ExerciseTemplates = new List<ExerciseTemplate>
    {
        new ExerciseTemplate
        {
            Name = "Жим лежачи",
            GroupOfMuscle = "Груди",
            RestBetweenSets = 120, // 2 хвилини
            TargetSets = 3,       // Увага, тут у тебе одруківка в назві властивості (див. коментар нижче)
            TargetReps = 10
        },
        new ExerciseTemplate
        {
            Name = "Присідання зі штангою",
            GroupOfMuscle = "Ноги",
            RestBetweenSets = 180, // 3 хвилини
            TargetSets = 4,
            TargetReps = 8
        },
        new ExerciseTemplate
        {
            Name = "Тяга верхнього блоку",
            GroupOfMuscle = "Спина",
            RestBetweenSets = 90,
            TargetSets = 3,
            TargetReps = 12
        }
    }
            };

            // Тут ти викликаєш свій сервіс, наприклад:
            // myService.Add(newTrainingTemplate);



            await _SchemaService.AddTemplateAsync(newTrainingTemplate);


           Templates.Add(newTrainingTemplate);  */
            
        }
    }
}
