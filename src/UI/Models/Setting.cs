using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace NugetCompare.UI
{
    using SimpleMvvmToolkit;

    public class Setting : ModelBase<Setting>
    {
        private string _searchDirectory;

        public string SearchDirectory
        {
            get { return _searchDirectory; }
            set
            {
                _searchDirectory = value;
                NotifyPropertyChanged(m => m.SearchDirectory);
            }
        }

        private ObservableCollection<Project> _projects;
        public ObservableCollection<Project> Projects
        {
            get { return _projects; }
            set
            {
                _projects = value;
                NotifyPropertyChanged(m => m.Projects);
            }
        }
    }

    public class Package : ModelBase<Package>
    {
        private string _name;
        private string _version;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                NotifyPropertyChanged(m => m.Name);
            }
        }

        public string Version
        {
            get { return _version; }
            set
            {
                _version = value;
                NotifyPropertyChanged(m => m.Version);
            }
        }
    }

    public class Project : ModelBase<Project>
    {
        private ObservableCollection<Package> _packages;
        private string _directory;
        private string _projectFile;

        public Project()
        {
            Packages = new ObservableCollection<Package>();
        }

        public ObservableCollection<Package> Packages
        {
            get { return _packages; }
            set
            {
                _packages = value;
                NotifyPropertyChanged(m => m.Packages);
            }
        }

        public string Directory
        {
            get { return _directory; }
            set
            {
                _directory = value;
                NotifyPropertyChanged(m => m.Directory);
            }
        }

        public string ProjectFile
        {
            get { return _projectFile; }
            set
            {
                _projectFile = value;
                NotifyPropertyChanged(m => m.ProjectFile);
            }
        }

        public string ProjectName
        {
            get
            {
                return !string.IsNullOrWhiteSpace(ProjectFile) && ProjectFile.Contains(".")
                    ? ProjectFile.Replace(".csproj", "")
                    : string.Empty;
            }
        }
    }
}
