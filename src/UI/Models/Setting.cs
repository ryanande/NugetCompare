namespace NugetCompare.UI
{
    using System.Collections.ObjectModel;
    using System.Linq;
    using SimpleMvvmToolkit;


    public class Setting : ModelBase<Setting>
    {
        private string _searchDirectory;
        private ObservableCollection<Project> _projects;
        private ObservableCollection<SharedPackage> _sharedPackages;
        private ObservableCollection<string> _solutions;

        public string SearchDirectory
        {
            get { return _searchDirectory; }
            set
            {
                _searchDirectory = value;
                NotifyPropertyChanged(m => m.SearchDirectory);
            }
        }

        public ObservableCollection<string> Solutions
        {
            get { return _solutions; }
            set
            {
                _solutions = value;
                NotifyPropertyChanged(m => m.Solutions);
            }
        }

        public ObservableCollection<Project> Projects
        {
            get { return _projects; }
            set
            {
                _projects = value;
                NotifyPropertyChanged(m => m.Projects);
            }
        }

        public ObservableCollection<SharedPackage> SharedPackages
        {
            get { return _sharedPackages; }
            set
            {
                _sharedPackages = value;
                NotifyPropertyChanged(m => m.SharedPackages);
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

    public class SharedPackage : ModelBase<SharedPackage>
    {
        private string _name;
        private ObservableCollection<ProjectPackageVersion> _projectPackage;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                NotifyPropertyChanged(m => m.Name);
            }
        }

        public ObservableCollection<ProjectPackageVersion> ProjectPackage
        {
            get { return _projectPackage; }
            set
            {
                _projectPackage = value;
                NotifyPropertyChanged(m => m.ProjectPackage);
            }
        }

        public class ProjectPackageVersion
        {
            public string ProjectName { get; set; }
            public string Version { get; set; }
        }
    }
}
