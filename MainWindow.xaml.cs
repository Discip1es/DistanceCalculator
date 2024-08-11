using DistanceCalculator.ViewModels;
using MahApps.Metro.Controls;

namespace DistanceCalculator
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса MainWindow.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            // Установка DataContext для привязки данных
            DataContext = new MainViewModel();
        }
    }
}
