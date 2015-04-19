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
    }
}
