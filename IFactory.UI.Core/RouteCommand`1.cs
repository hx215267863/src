using System;
using System.Windows.Input;

namespace IFactory.UI.Core
{
    public class RouteCommand<TParameter> : ICommand
    {
        private Action<TParameter> action;

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public RouteCommand(Action<TParameter> action)
        {
            this.action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (this.action == null)
                return;
            this.action((TParameter)parameter);
        }
    }
}
