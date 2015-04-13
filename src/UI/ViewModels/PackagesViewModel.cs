﻿using System;
using System.Windows;
using System.Threading;
using System.Collections.ObjectModel;

// Toolkit namespace
using SimpleMvvmToolkit;

namespace NugetCompare.UI.ViewModels
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// Use the <strong>mvvmprop</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// </summary>
    public class PackagesViewModel : ViewModelBase<PackagesViewModel>
    {
        // TODO: Add a member for IXxxServiceAgent
        private readonly IPackageService _serviceAgent;

        // Default ctor
        public PackagesViewModel() { }

        // TODO: ctor that accepts IXxxServiceAgent
        public PackagesViewModel(IPackageService serviceAgent)
        {
            _serviceAgent = serviceAgent;
        }

        // TODO: Add events to notify the view or obtain data from the view
        public event EventHandler<NotificationEventArgs<Exception>> ErrorNotice;

        // TODO: Add properties using the mvvmprop code snippet

        // TODO: Add methods that will be called by the view

        // TODO: Optionally add callback methods for async calls to the service agent
        
        // Helper method to notify View of an error
        private void NotifyError(string message, Exception error)
        {
            // Notify view of an error
            Notify(ErrorNotice, new NotificationEventArgs<Exception>(message, error));
        }
    }
}