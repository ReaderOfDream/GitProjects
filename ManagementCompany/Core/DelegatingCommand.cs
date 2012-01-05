using System;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Threading;

namespace Core
{
    public class Fact : INotifyPropertyChanged
    {
        private readonly Func<bool> predicate;

        public Fact(INotifyPropertyChanged observable, Func<bool> predicate)
        {
            this.predicate = predicate;
            observable.PropertyChanged += (sender, args) => PropertyChanged(this, new PropertyChangedEventArgs("Value"));
        }

        public bool Value
        {
            get
            {
                return predicate();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }

    public class DelegatingCommand : ICommand
    {
        private readonly Action action;
        private readonly Fact canExecute;

        public DelegatingCommand(Action action)
            : this(action, null)
        {
        }

        public DelegatingCommand(Action action, Fact canExecute)
        {
            this.action = action;
            this.canExecute = canExecute;
            var dispatcher = Dispatcher.CurrentDispatcher;
            if (canExecute != null)
            {
                this.canExecute.PropertyChanged +=
                    (sender, args) =>
                        dispatcher.Invoke(CanExecuteChanged, this, EventArgs.Empty);
            }
        }

        public void Execute(object parameter)
        {
            action();
        }

        public bool CanExecute(object parameter)
        {
            if (canExecute == null)
                return true;
            return canExecute.Value;
        }

        public event EventHandler CanExecuteChanged = delegate { };
    }
}
