using System;
using System.Collections.Generic;
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
    public class ActiveTraningViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private TraningSession _currentSession;
        private readonly TrainingService _trainingService;

        public TraningSession CurrentSession
        {
            get => _currentSession;

            set
            {
                _currentSession = value;
                OnPropertyChanged();


            }
               
        }


        public ActiveTraningViewModel(TrainingService trainingService)
        {
            _trainingService = trainingService;

            FinishWorkoutCommand = new Command(async () => FinishWorkout());

            AddSetCommand = new Command<Exercise>(async (exercise) => AddSet(exercise));
            EditNoteCommand = new Command<Set>(async (s) => await EditNote(s));
        }

        public ICommand EditNoteCommand { get;  }

        private async Task EditNote(Set set)
        {
            if (set == null) return;

            
            string result = await Application.Current.MainPage.DisplayPromptAsync(
                "Нотатка",
                "Додайте коментар до підходу:",
                initialValue: set.Note, 
                maxLength: 100,
                keyboard: Keyboard.Text);

            
            if (result != null)
            {
                set.Note = result;

               
                if (string.IsNullOrWhiteSpace(result)) set.Note = null;
            }
        }
        public ICommand AddSetCommand { get; }

        private void AddSet(Exercise exercise)
        {
            if (exercise == null) return;

            var newSet = new Set
            {
                Number = exercise.Sets.Count + 1, 
                Weight = 0,
                Reps = 0,
                ExerciseId = exercise.Id
            };

            exercise.Sets.Add(newSet);


        }

        public TimeSpan SessionTime
        {
            get
            {
               
                return CurrentSession?.Date.TimeOfDay ?? TimeSpan.Zero;
            }
            set
            {
                if (CurrentSession != null)
                {
                    
                    CurrentSession.Date = CurrentSession.Date.Date + value;

                    OnPropertyChanged();
                    OnPropertyChanged(nameof(CurrentSession));
                }
            }
        }


        public ICommand FinishWorkoutCommand { get; }

        private async Task FinishWorkout()
        {
            if (CurrentSession == null) return;

            try
            {
                await _trainingService.FinishSessionAsync(CurrentSession);

                await Application.Current.MainPage.DisplayAlert("Успіх", "Тренування збережено!", "OK");

                await Application.Current.MainPage.Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Помилка", $"Не вдалося зберегти: {ex.Message}", "OK");
            }
        }
        public async Task Initialize(int id)
        {

            CurrentSession = await _trainingService.GetSessionByIdAsync(id);

            if (CurrentSession.Quality == 0)
            {
                CurrentSession.Quality = 5;
            }

            OnPropertyChanged(nameof(SessionTime));
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
