namespace NugetCompare.UI.Services
{
    using System.Collections.Generic;

    public interface IPackageService
    {
        IEnumerable<string> LoadDependencies(string path);
    }

    public class PackageService : IPackageService
    {

        public IEnumerable<string> LoadDependencies(string path)
        {
            // find all packages.config in the dir/ recursive

            // foreach file

            // 


            return new List<string>();
        }
    }
}
