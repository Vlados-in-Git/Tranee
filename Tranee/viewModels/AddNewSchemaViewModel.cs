using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tranee.servises;
using TraneeLibrary;
using static System.Collections.Specialized.BitVector32;

namespace Tranee.viewModels
{
    public class AddNewSchemaViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly SchemaService _SchemaService;

        public ObservableCollection<TrainingTemplate> Templates { get; set; } = new ObservableCollection<TrainingTemplate>();

        public ICommand AddTestSchemaCommand { get; }
        public AddNewSchemaViewModel(SchemaService service)
        {
            _SchemaService = service;

            AddTestSchemaCommand = new Command(async () => await AddSchema() );

            Task.Run(LoadData);
        }

        private async Task LoadData()
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
            TargertSets = 3,       // Увага, тут у тебе одруківка в назві властивості (див. коментар нижче)
            TargetReps = 10
        },
        new ExerciseTemplate
        {
            Name = "Присідання зі штангою",
            GroupOfMuscle = "Ноги",
            RestBetweenSets = 180, // 3 хвилини
            TargertSets = 4,
            TargetReps = 8
        },
        new ExerciseTemplate
        {
            Name = "Тяга верхнього блоку",
            GroupOfMuscle = "Спина",
            RestBetweenSets = 90,
            TargertSets = 3,
            TargetReps = 12
        }
    }
            };

            // Тут ти викликаєш свій сервіс, наприклад:
            // myService.Add(newTrainingTemplate);



            await _SchemaService.AddTemplateAsync(newTrainingTemplate);


           Templates.Add(newTrainingTemplate);
            
        }
    }
}
