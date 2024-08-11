using DistanceCalculator.Commands;
using DistanceCalculator.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace DistanceCalculator.ViewModels
{
    /// <summary>
    /// Модель представления для главного окна.
    /// </summary>
    public class MainViewModel : INotifyPropertyChanged
    {
        private City _selectedCity1;
        private City _selectedCity2;
        private string _distanceResult;

        /// <summary>
        /// Список городов.
        /// </summary>
        public ObservableCollection<City> Cities { get; set; }

        /// <summary>
        /// Выбранный первый город.
        /// </summary>
        public City SelectedCity1
        {
            get => _selectedCity1;
            set
            {
                _selectedCity1 = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Выбранный второй город.
        /// </summary>
        public City SelectedCity2
        {
            get => _selectedCity2;
            set
            {
                _selectedCity2 = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Результат расчета расстояния.
        /// </summary>
        public string DistanceResult
        {
            get => _distanceResult;
            set
            {
                _distanceResult = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Команда для расчета расстояния.
        /// </summary>
        public ICommand CalculateDistanceCommand { get; }

        /// <summary>
        /// Инициализирует новый экземпляр класса MainViewModel.
        /// </summary>
        public MainViewModel()
        {
            // Загрузка данных городов из JSON файла
            Cities = LoadCities();
            CalculateDistanceCommand = new RelayCommand(CalculateDistance);
        }

        /// <summary>
        /// Загружает список городов из JSON файла.
        /// </summary>
        /// <returns>Список городов.</returns>
        private ObservableCollection<City> LoadCities()
        {
            var json = File.ReadAllText("./data/cities.json");
            return JsonConvert.DeserializeObject<ObservableCollection<City>>(json);
        }

        /// <summary>
        /// Вычисляет расстояние между двумя выбранными городами.
        /// </summary>
        private void CalculateDistance()
        {
            if (SelectedCity1 != null && SelectedCity2 != null)
            {
                var distance = GetDistance(SelectedCity1, SelectedCity2);
                DistanceResult = $"Расстояние: {distance:F2} км";
            }
            else
            {
                DistanceResult = "Пожалуйста, выберите оба города.";
            }
        }

        /// <summary>
        /// Вычисляет расстояние между двумя городами.
        /// </summary>
        /// <param name="city1">Первый город.</param>
        /// <param name="city2">Второй город.</param>
        /// <returns>Расстояние в километрах.</returns>
        private double GetDistance(City city1, City city2)
        {
            var d1 = city1.Latitude * (Math.PI / 180.0);
            var num1 = city1.Longitude * (Math.PI / 180.0);
            var d2 = city2.Latitude * (Math.PI / 180.0);
            var num2 = city2.Longitude * (Math.PI / 180.0) - num1;
            var d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) + Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0);

            return 6371.0 * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3)));
        }

        /// <summary>
        /// Событие, возникающее при изменении свойства.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Вызывает событие PropertyChanged.
        /// </summary>
        /// <param name="propertyName">Имя измененного свойства.</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
