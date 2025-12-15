using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using Tranee.servises;

namespace Tranee.viewModels
{
    public class AnalyticsViewModel : INotifyPropertyChanged
    {
        private readonly TrainingService _trainingService;

        // Тут ми зберігаємо історію
        private List<ExerciseHistoryItem> _fullHistory = new();

        public ObservableCollection<ISeries> Series { get; set; } = new();
        public ObservableCollection<Axis> XAxes { get; set; } = new();
        public ObservableCollection<Axis> YAxes { get; set; } = new();
        public ObservableCollection<string> ExerciseNames { get; set; } = new();

        public ICommand FilterCommand { get; private set; }

        private string _selectedExercise;
        public string SelectedExercise
        {
            get => _selectedExercise;
            set
            {
                if (_selectedExercise != value)
                {
                    _selectedExercise = value;
                    OnPropertyChanged();

                    if (!string.IsNullOrEmpty(value))
                    {
                        LoadChartAsync(value);
                    }
                }
            }
        }

        private string _chartTitle = "Оберіть вправу";
        public string ChartTitle
        {
            get => _chartTitle;
            set
            {
                if (_chartTitle != value)
                {
                    _chartTitle = value;
                    OnPropertyChanged();
                }
            }
        }

        public AnalyticsViewModel(TrainingService trainingService)
        {
            _trainingService = trainingService;
            FilterCommand = new Command<string>(ApplyFilter);
            LoadExercisesListAsync();
        }

        public async void LoadExercisesListAsync()
        {
            var names = await _trainingService.GetUniqueExerciseNamesAsync();

            ExerciseNames.Clear();
            foreach (var name in names)
            {
                ExerciseNames.Add(name);
            }

            if (ExerciseNames.Any())
            {
                SelectedExercise = ExerciseNames.First();
            }
        }

        public async void LoadChartAsync(string exerciseName)
        {
            ChartTitle = $"Прогрес: {exerciseName}";

            // 1. Отримуємо дані з сервісу (це список Tuples)
            var serviceData = await _trainingService.GetExerciseHistoryAsync(exerciseName);

            // 2. Перетворюємо їх у наш клас ExerciseHistoryItem
            _fullHistory = new List<ExerciseHistoryItem>();

            if (serviceData != null)
            {
                foreach (var item in serviceData)
                {
                    // item.Date та item.MaxWeight беруться з кортежу, який повертає сервіс
                    _fullHistory.Add(new ExerciseHistoryItem
                    {
                        Date = item.Date,
                        MaxWeight = item.MaxWeight
                    });
                }
            }

            // 3. За замовчуванням показуємо все ("0")
            ApplyFilter("0");
        }


        private string _currentFilter = "0";
        public string CurrentFilter
        {
            get => _currentFilter;
            set
            {
                if (_currentFilter != value)
                {
                    _currentFilter = value;
                    OnPropertyChanged();
                }
            }
        }

        private void ApplyFilter(string monthsStr)
        {
            if (_fullHistory == null || !_fullHistory.Any())
            {
                Series.Clear();
                XAxes.Clear();
                return;
            }

            // <-- ВАЖЛИВО: Запам'ятовуємо, який фільтр ми натиснули
            CurrentFilter = monthsStr;

            int months = int.Parse(monthsStr);
            List<ExerciseHistoryItem> filteredData;

            if (months == 0)
            {
                filteredData = _fullHistory.ToList();
            }
            else
            {
                var cutoffDate = DateTime.Now.AddMonths(-months);
                filteredData = _fullHistory.Where(x => x.Date >= cutoffDate).ToList();
            }

            DrawChart(filteredData);
        }

        private void DrawChart(List<ExerciseHistoryItem> data)
        {
            if (data == null || !data.Any())
            {
                Series.Clear();
                XAxes.Clear();
                return;
            }

            XAxes.Clear();
            XAxes.Add(new Axis
            {
                Labels = data.Select(x => x.Date.ToString("dd.MM")).ToList(),
                LabelsRotation = 45,
                TextSize = 12
            });

            YAxes.Clear();
            YAxes.Add(new Axis
            {
                Labeler = value => $"{value} кг",
                TextSize = 12
            });

            var mainColor = SKColors.DodgerBlue;

            Series.Clear();
            Series.Add(new LineSeries<double>
            {
                Values = data.Select(x => x.MaxWeight).ToList(),
                Name = "Максимальна вага",
                Stroke = new SolidColorPaint(mainColor) { StrokeThickness = 4 },
                GeometrySize = 12,
                GeometryStroke = new SolidColorPaint(mainColor) { StrokeThickness = 3 },
                GeometryFill = new SolidColorPaint(mainColor.WithAlpha(150)),
                LineSmoothness = 0.5,
                Fill = new SolidColorPaint(mainColor.WithAlpha(50))
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    // --- ДОДАВ КЛАС, ЯКОГО НЕ ВИСТАЧАЛО ---
    public class ExerciseHistoryItem
    {
        public DateTime Date { get; set; }
        public double MaxWeight { get; set; }
    }
}