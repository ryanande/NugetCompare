using System;
using System.Windows.Input;
using SimpleMvvmToolkit;

namespace NugetCompare.UI
{
    using System.Threading;

    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// Use the <strong>mvvmprop</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// </summary>
    public class SelectDiretoryViewModel : ViewModelDetailBase<SelectDiretoryViewModel, Setting>
    {


        public event EventHandler<NotificationEventArgs<Exception>> ErrorNotice;
        public event EventHandler<NotificationEventArgs> ScanStarted;
        public event EventHandler<NotificationEventArgs> ScanComplete;
        public event EventHandler<NotificationEventArgs> OpenBrowse;

        private readonly IPackageService _packageService;

        private ICommand _scanCommand;
        public ICommand ScanCommand
        {
            get {
                return _scanCommand ??
                       (_scanCommand =
                           new AsyncDelegateCommand(Scan, null, s => ScanCompleted(),
                               ex => NotifyError(ex.Message, ex)));
            }
        }


        public ICommand BrowseCommand => new DelegateCommand(Browse);


        private bool _scanning;
        public bool Scanning
        {
            get
            {
                return _scanning;
            }
            set
            {
                _scanning = value;
                NotifyPropertyChanged(m => m.Scanning);
            }
        }

        public SelectDiretoryViewModel(IPackageService packageService)
        {
            _packageService = packageService;
            Model = new Setting();
        }

        public void Browse()
        {
            Notify(OpenBrowse, new NotificationEventArgs());
        }

        public void Scan()
        {
            Scanning = true;

            // here I would typically like to use automapper to map from a domain obj to a ModelBase (service shouldn't know UI stuff)
            Model.Packages = _packageService.LoadDependencies(Model.SearchDirectory);
            Thread.Sleep(5000);
        }


        private void ScanCompleted()
        {
            Scanning = false;

        }
        
        // Helper method to notify View of an error
        private void NotifyError(string message, Exception error)
        {
            // Notify view of an error
            Notify(ErrorNotice, new NotificationEventArgs<Exception>(message, error));
        }
    }
}