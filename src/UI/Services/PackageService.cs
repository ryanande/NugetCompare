using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Xml;
using SimpleMvvmToolkit;

namespace NugetCompare.UI
{
    public interface IPackageService
    {
        ObservableCollection<Project> LoadDependencies(string val);
    }

    public class PackageService : IPackageService
    {

        private const string PackagesConfigFileName = "packages.config";

        public ObservableCollection<Project> LoadDependencies(string val)
        {

            if (!Directory.Exists(val))
            {
                throw new DirectoryNotFoundException();
            }


            var locatedPackages = FindPackages(val).ToList();
            if (!locatedPackages.Any())
                return null;

            return locatedPackages.Select(fullPackagePath => new Project
            {
                Packages = GetPackages(fullPackagePath),
                Directory = Path.GetFullPath(fullPackagePath),
                ProjectFile = FindProjectFile(Path.GetDirectoryName(fullPackagePath))
            }).ToObservableCollection();
        }

        private ObservableCollection<Package> GetPackages(string path)
        {
            var packages = new ObservableCollection<Package>();
            using (var reader = XmlReader.Create(path))
            {
                while (reader.Read())
                {
                    if (!reader.IsStartElement() || reader["id"] == null)
                        continue;

                    var package = new Package
                    {
                        Name = reader["id"] ?? "",
                        Version = reader["version"] ?? ""
                    };
                    packages.Add(package);
                }
            }

            return packages;
        }


        IEnumerable<string> FindPackages(string path)
        {
            var files = Directory.EnumerateFiles(path).Where(m => m.Contains(PackagesConfigFileName)).ToList();
            foreach (var subDir in Directory.EnumerateDirectories(path))
            {
                files.AddRange(FindPackages(subDir));
            }

            return files;
        }

        private string FindProjectFile(string path)
        {
            var file = Directory.EnumerateFiles(path).SingleOrDefault(f => f.EndsWith(".csproj"));
            return Path.GetFileName(file);
        }

    }


   

}
