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
        private List<ExerciseHistoryItem> _fullHistory = new();

        public ObservableCollection<ISeries> Series { get; set; } = new();
        public ObservableCollection<Axis> XAxes { get; set; } = new();
        public ObservableCollection<Axis> YAxes { get; set; } = new();
        public ObservableCollection<string> ExerciseNames { get; set; } = new();

        public ICommand FilterCommand { get; private set; }

        public List<string> ChartMetrics { get; } = new List<string>
        {
            "Сила (Макс. вага)",       // 0
            "Об'єм (Тоннаж)",          // 1
            "1ПМ (Теоретичний)",       // 2
            "Витривалість (Повтори)"   // 3
        };

       
        private int _selectedMetricIndex = 0;
        public int SelectedMetricIndex
        {
            get => _selectedMetricIndex;
            set
            {
                if (_selectedMetricIndex != value)
                {
                    _selectedMetricIndex = value;
                    OnPropertyChanged();

                    
                    ApplyFilter(CurrentFilter);
                }
            }
        }

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

            
            SelectedMetricIndex = 0;

            LoadExercisesListAsync();
        }

        public async void LoadExercisesListAsync()
        {
            var names = await _trainingService.GetUniqueExerciseNamesAsync();
            ExerciseNames.Clear();
            foreach (var name in names) ExerciseNames.Add(name);

            if (ExerciseNames.Any())
                SelectedExercise = ExerciseNames.First();
        }

        public async void LoadChartAsync(string exerciseName)
        {
            ChartTitle = $"Прогрес: {exerciseName}";
            var data = await _trainingService.GetExerciseHistoryAsync(exerciseName);
            _fullHistory = data ?? new List<ExerciseHistoryItem>();
            ApplyFilter("0");
        }

        private string _currentFilter = "0";
        public string CurrentFilter
        {
            get => _currentFilter;
            set { if (_currentFilter != value) { _currentFilter = value; OnPropertyChanged(); } }
        }

        private bool _isChartVisible = true;
        public bool IsChartVisible
        {
            get => _isChartVisible;
            set { if (_isChartVisible != value) { _isChartVisible = value; OnPropertyChanged(); } }
        }

        private bool _isNoDataVisible = false;
        public bool IsNoDataVisible
        {
            get => _isNoDataVisible;
            set { if (_isNoDataVisible != value) { _isNoDataVisible = value; OnPropertyChanged(); } }
        }

        private void ApplyFilter(string monthsStr)
        {
            if (_fullHistory == null || !_fullHistory.Any())
            {
                DrawChart(new List<ExerciseHistoryItem>());
                return;
            }

            CurrentFilter = monthsStr;
            int months = int.Parse(monthsStr);
            List<ExerciseHistoryItem> filteredData;

            if (months == 0)
                filteredData = _fullHistory.ToList();
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
                IsChartVisible = false;
                IsNoDataVisible = true;
                return;
            }

            IsChartVisible = true;
            IsNoDataVisible = false;

            XAxes.Clear();
            XAxes.Add(new Axis
            {
                Labels = data.Select(x => x.Date.ToString("dd.MM")).ToList(),
                LabelsRotation = 45,
                TextSize = 12
            });

            List<double> valuesToShow;
            string seriesName;
            SKColor lineColor;

            
            switch (SelectedMetricIndex)
            {
                case 1:
                    valuesToShow = data.Select(x => x.Volume).ToList();
                    seriesName = "Об'єм (кг)";
                    lineColor = SKColors.OrangeRed;
                    break;
                case 2:
                    valuesToShow = data.Select(x => x.OneRepMax).ToList();
                    seriesName = "1ПМ (кг)";
                    lineColor = SKColors.Purple;
                    break;
                case 3:
                    valuesToShow = data.Select(x => (double)x.TotalReps).ToList();
                    seriesName = "Повтори (шт)";
                    lineColor = SKColors.SeaGreen;
                    break;
                case 0:
                default:
                    valuesToShow = data.Select(x => x.MaxWeight).ToList();
                    seriesName = "Максимальна вага";
                    lineColor = SKColors.DodgerBlue;
                    break;
            }

            YAxes.Clear();
            YAxes.Add(new Axis
            {
                Labeler = value => SelectedMetricIndex == 3 ? $"{value:N0}" : $"{value:N0} кг",
                TextSize = 12
            });

            Series.Clear();
            Series.Add(new LineSeries<double>
            {
                Values = valuesToShow,
                Name = seriesName,
                Stroke = new SolidColorPaint(lineColor) { StrokeThickness = 4 },
                GeometrySize = 12,
                GeometryStroke = new SolidColorPaint(lineColor) { StrokeThickness = 3 },
                GeometryFill = new SolidColorPaint(lineColor.WithAlpha(150)),
                LineSmoothness = 0.5,
                Fill = new SolidColorPaint(lineColor.WithAlpha(50))
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    public class ExerciseHistoryItem
    {
        public DateTime Date { get; set; }
        public double MaxWeight { get; set; }
        public double Volume { get; set; }
        public double OneRepMax { get; set; }
        public int TotalReps { get; set; }
    }
}