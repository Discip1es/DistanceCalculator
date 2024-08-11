using System.Windows.Input;

namespace DistanceCalculator
{
    /// <summary>
    /// Реализация интерфейса ICommand для упрощения создания команд.
    /// </summary>
    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        /// <summary>
        /// Событие, возникающее при изменении возможности выполнения команды.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса RelayCommand.
        /// </summary>
        /// <param name="execute">Действие, выполняемое командой.</param>
        /// <param name="canExecute">Функция, определяющая, может ли команда выполняться.</param>
        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        /// <summary>
        /// Определяет, может ли команда выполняться.
        /// </summary>
        /// <param name="parameter">Параметр команды.</param>
        /// <returns>True, если команда может выполняться; в противном случае — false.</returns>
        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute();
        }

        /// <summary>
        /// Выполняет команду.
        /// </summary>
        /// <param name="parameter">Параметр команды.</param>
        public void Execute(object parameter)
        {
            _execute();
        }
    }
}
