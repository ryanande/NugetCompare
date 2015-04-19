using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NugetCompare.UI
{
    using Ookii.Dialogs.Wpf;
    using SimpleMvvmToolkit;

    /// <summary>
    /// Interaction logic for SelectDiretoryView.xaml
    /// </summary>
    public partial class SelectDiretoryView : UserControl
    {

        private readonly SelectDiretoryViewModel _viewModel;

        public SelectDiretoryView()
        {
            InitializeComponent();
            _viewModel = (SelectDiretoryViewModel)DataContext;
            if (_viewModel == null)
            {
                return;
            }

            _viewModel.OpenBrowse += OnOpenBrowse;
        }

        void OnOpenBrowse(object sender, NotificationEventArgs e)
        {
            var dialog = new VistaFolderBrowserDialog { ShowNewFolderButton = true };
            var showDialog = dialog.ShowDialog();
            if (showDialog != null && (bool)showDialog)
            {
                _viewModel.Model.SearchDirectory = dialog.SelectedPath;
            }
        }
    }
}
