using System;
using System.Windows.Input;
using SimpleMvvmToolkit;

namespace NugetCompare.UI
{

    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// Use the <strong>mvvmprop</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// </summary>
    public class SelectDiretoryViewModel : ViewModelDetailBase<SelectDiretoryViewModel, Setting>
    {


        public event EventHandler<NotificationEventArgs<Exception>> ErrorNotice;
        public event EventHandler<NotificationEventArgs> GenerationStarted;
        public event EventHandler<NotificationEventArgs> GenerateComplete;
        public event EventHandler<NotificationEventArgs> OpenBrowse;

        private ICommand _generateCommand;
        public ICommand GenerateCommand
        {
            get {
                return _generateCommand ??
                       (_generateCommand =
                           new AsyncDelegateCommand(Generate, null, s => GenerateCompleted(),
                               ex => NotifyError(ex.Message, ex)));
            }
        }


        public ICommand BrowseCommand
        {
            get
            {
                return new DelegateCommand(Browse);
            }
        }


        private bool _generating;
        public bool Generating
        {
            get
            {
                return _generating;
            }
            set
            {
                _generating = value;
                NotifyPropertyChanged(m => m.Generating);
            }
        }

        public void Browse()
        {
            Notify(OpenBrowse, new NotificationEventArgs());
        }

        public void Generate()
        {
            
        }


        private void GenerateCompleted()
        {
            Generating = false;

        }
        
        // Helper method to notify View of an error
        private void NotifyError(string message, Exception error)
        {
            // Notify view of an error
            Notify(ErrorNotice, new NotificationEventArgs<Exception>(message, error));
        }
    }
}