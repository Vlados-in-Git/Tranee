
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

       
        public CreatingTemplatePageViewModel(SchemaService schemaService, AddNewSchemaViewModel addNewSchemaViewModel)
        {
            DraftExercise = new ExerciseTemplate();
            
            _schemaService = schemaService;
           

            AddExerciseToBufferCommand = new Command(async () => AddExercise());
            RemoveExerciseCommand = new Command<ExerciseTemplate>(async (delExercise) => RemoveExercise(delExercise) );  // ??
            SaveTemplateCommand = new Command(async () => SaveTemplate());
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
            if (CurrentTemplate.Name == null || CurrentTemplate.RestBetweenSets == null)
            {
                Application.Current.MainPage.DisplayAlert("Стривайте","Заповність всі поля", "OK"); // TODOS: MAKE DisplayAlert()

                return;
            }



            foreach(ExerciseTemplate ex in AddedExercises)
            {
                CurrentTemplate.ExerciseTemplates.Add(ex);
            }

            await _schemaService.AddTemplateAsync(CurrentTemplate);

           

            await Application.Current.MainPage.DisplayAlert("Успіх", "Шаблон збережено!", "OK");

            await Application.Current.MainPage.Navigation.PopAsync();


        }


        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }


}
