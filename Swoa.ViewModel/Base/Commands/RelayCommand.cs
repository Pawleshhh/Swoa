using System;
using System.Diagnostics;
using System.Windows.Input;

namespace Swoa.ViewModel
{
    public class RelayCommand : ICommand
    {
        #region Fields

        private readonly Action<object> _execute;

        private readonly Predicate<object> _canExecute;

        #endregion Fields

        #region Constructor

        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException("execute");
            _canExecute = canExecute;
        }

        #endregion Constructor

        #region ICommand Members

        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        #endregion ICommand Members

        public static ICommand Create(ref ICommand command, Action<object> execute, Predicate<object> canExecute = null)
            => command ??= new RelayCommand(execute, canExecute);
    }
}