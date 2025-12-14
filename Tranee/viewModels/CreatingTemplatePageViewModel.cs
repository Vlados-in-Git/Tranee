
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
using TraneeLibrary;

namespace Tranee.viewModels
{
    public class CreatingTemplatePageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private ExerciseTemplate _draftExercise;
        private SchemaService _schemaService;
      

        public TrainingTemplate CurrentTemplate { get; set; } = new TrainingTemplate();
        public ExerciseTemplate DraftExercise
        {
            get => _draftExercise;

            set
            {
                _draftExercise = value;
               OnPropertyChanged();
            }
        }


        public ObservableCollection<ExerciseTemplate> AddedExercises { get; set; } = new ObservableCollection<ExerciseTemplate>();

       
        public CreatingTemplatePageViewModel(SchemaService schemaService)
        {
            DraftExercise = new ExerciseTemplate();
            
            _schemaService = schemaService;
           

            AddExerciseToBufferCommand = new Command(async () => AddExercise());
            RemoveExerciseCommand = new Command<ExerciseTemplate>(async (delExercise) => RemoveExercise(delExercise) );  // ??
            SaveTemplateCommand = new Command(async () =>await SaveTemplate());
        }


        public ICommand SaveTemplateCommand { get; }
        public ICommand AddExerciseToBufferCommand { get; }
         public ICommand RemoveExerciseCommand { get; }

        private void AddExercise()
        {
            if (DraftExercise.Name == null) return;

            AddedExercises.Add(DraftExercise);

            DraftExercise = new ExerciseTemplate();
        }

        private void RemoveExercise(ExerciseTemplate exercise)
        {
            if (exercise == null) return;

            AddedExercises.Remove(exercise);

        }

        private async Task SaveTemplate()
        {
            // 1. Валідація
            if (string.IsNullOrWhiteSpace(CurrentTemplate.Name))
            {
                await Application.Current.MainPage.DisplayAlert("Увага", "Вкажіть назву тренування", "ОК");
                return;
            }

            if (AddedExercises.Count == 0)
            {
                await Application.Current.MainPage.DisplayAlert("Увага", "Додайте хоча б одну вправу", "ОК");
                return;
            }

            // 2. Гарантуємо, що список у моделі існує (виправлення пункту 4 з чек-листа)
            if (CurrentTemplate.ExerciseTemplates == null)
                CurrentTemplate.ExerciseTemplates = new List<ExerciseTemplate>();

            CurrentTemplate.ExerciseTemplates.Clear(); // про всяк випадок очистимо

            foreach (var ex in AddedExercises)
            {
                // Ось цей рядок рятує ситуацію:
                ex.TrainingTemplate = CurrentTemplate;

                CurrentTemplate.ExerciseTemplates.Add(ex);
            }

            // 4. Зберігаємо
            await _schemaService.AddTemplateAsync(CurrentTemplate);
            await Application.Current.MainPage.DisplayAlert("Успіх", "Шаблон збережено!", "OK");

            // 5. Виходимо
            await Application.Current.MainPage.Navigation.PopAsync();
        }


        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }


}
