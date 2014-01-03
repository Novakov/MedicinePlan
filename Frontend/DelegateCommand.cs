using System;
using System.Windows.Input;

namespace Frontend
{
    public class DelegateCommand : ICommand
    {
        private readonly Action action;

        public DelegateCommand(Action action)
        {
            this.action = action;
        }

        public bool CanExecute(object parameter)
        {
            return this.action != null;
        }

        public void Execute(object parameter)
        {
            if (this.action != null)
            {
                this.action();
            }
        }

        public event EventHandler CanExecuteChanged;
    }

    public class DelegateCommand<TParameter> : ICommand
    {
        private readonly Action<TParameter> action;

        public DelegateCommand(Action<TParameter> action)
        {
            this.action = action;
        }

        public bool CanExecute(object parameter)
        {
            return this.action != null;
        }

        public void Execute(object parameter)
        {
            if (this.action != null)
            {
                this.action((TParameter)parameter);
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}