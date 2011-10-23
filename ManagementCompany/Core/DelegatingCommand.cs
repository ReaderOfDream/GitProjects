using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Core
{
    public class DelegatingCommand : ICommand
    {
        private readonly Action action;
        private readonly Func<bool> fact;

        public DelegatingCommand(Action action, Func<bool> fact)
        {
            this.action = action;
            this.fact = fact;
        }

        #region Implementation of ICommand

        public void Execute(object parameter)
        {
            action();
        }

        public bool CanExecute(object parameter)
        {
            return fact();
        }

        public event EventHandler CanExecuteChanged;

        #endregion
    }
}
