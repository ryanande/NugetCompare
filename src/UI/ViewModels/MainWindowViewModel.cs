using System;
using System.Collections.Generic;
using System.Windows.Controls;
using SimpleMvvmToolkit;

namespace NugetCompare.UI
{
    public class MainPageViewModel : ViewModelBase<MainPageViewModel>
    {
        public event EventHandler<NotificationEventArgs<Exception>> ErrorNotice;

        private UserControl _currentView;
        public UserControl CurrentView
        {
            get
            {
                return _currentView;
            }
            set
            {
                _currentView = value;
                NotifyPropertyChanged(m => m.CurrentView);
            }
        }

        public Stack<UserControl> ControlStack { get; set; }


        public MainPageViewModel()
        {
            RegisterToReceiveMessages(MessageTokens.Navigation, OnNavigationRequested);
            ControlStack = new Stack<UserControl>();
            CurrentView = new SelectDiretoryView();
        }


        void OnNavigationRequested(object sender, NotificationEventArgs e)
        {
            if (e.Message == ControlNames.SelectDirectory)
            {
                ControlStack.Push(CurrentView);
                CurrentView = new SelectDiretoryView();
            }
            else
            {
                CurrentView = ControlStack.Pop();
            }
        }

        // Helper method to notify View of an error
        private void NotifyError(string message, Exception error)
        {
            // Notify view of an error
            Notify(ErrorNotice, new NotificationEventArgs<Exception>(message, error));
        }
    }
}