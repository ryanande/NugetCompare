namespace NugetCompare.UI
{
    using System;
    using System.ComponentModel;
    using System.Windows.Input;

    /// <summary>
    /// Property of Kain...
    /// </summary>
    public class AsyncDelegateCommand : ICommand
    {
        readonly BackgroundWorker _worker = new BackgroundWorker();
        readonly Func<bool> _canExecute;


        public AsyncDelegateCommand(Action action, Func<bool> canExecute = null, Action<object> completed = null, Action<Exception> error = null)
        {

            _worker.DoWork += (s, e) =>
            {
                CommandManager.InvalidateRequerySuggested();
                action();
            };

            _worker.RunWorkerCompleted += (s, e) =>
            {

                if (completed != null && e.Error == null)
                    completed(e.Result);

                if (error != null && e.Error != null)
                    error(e.Error);

                CommandManager.InvalidateRequerySuggested();
            };
            _canExecute = canExecute;
        }


        public AsyncDelegateCommand(Action<object> action, Func<bool> canExecute = null, Action<object> completed = null, Action<Exception> error = null)
        {
            _worker.DoWork += (s, e) =>
            {
                CommandManager.InvalidateRequerySuggested();
                action(e.Argument);
            };

            _worker.RunWorkerCompleted += (s, e) =>
            {

                if (completed != null && e.Error == null)
                    completed(e.Result);

                if (error != null && e.Error != null)
                    error(e.Error);

                CommandManager.InvalidateRequerySuggested();
            };

            _canExecute = canExecute;
        }


        public void Cancel()
        {
            if (_worker.IsBusy)
                _worker.CancelAsync();
        }


        public bool CanExecute(object parameter)
        {
            return (_canExecute == null) ?
                    !(_worker.IsBusy) : !(_worker.IsBusy)
                        && _canExecute();
        }


        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }


        public void Execute(object parameter)
        {
            _worker.RunWorkerAsync(parameter);
        }

        public void Execute()
        {
            _worker.RunWorkerAsync();
        }
    }
}
