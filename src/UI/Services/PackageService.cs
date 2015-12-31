using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Xml;

namespace NugetCompare.UI
{
    public interface IPackageService
    {
        List<Project> LoadProjects(string path);
        List<SharedPackage> GetSharedPackages(string path);
    }

    public class PackageService : IPackageService
    {
        private const string PackagesConfigFileName = "packages.config";
        private List<Project> _projects; 

        public List<Project> LoadProjects(string path)
        {

            if (!Directory.Exists(path))
            {
                throw new DirectoryNotFoundException();
            }
            
            var locatedPackages = FindPackages(path).ToList();
            if (!locatedPackages.Any())
                return null;

            _projects = locatedPackages.Select(fullPackagePath => new Project
            {
                Packages = GetPackages(fullPackagePath),
                Directory = Path.GetFullPath(fullPackagePath),
                ProjectFile = FindProjectFile(Path.GetDirectoryName(fullPackagePath))
            }).ToList();

            return _projects;
        }

        public List<SharedPackage> GetSharedPackages(string path)
        {
            // no me gusta
            var projects = LoadProjects(path).SelectMany(b => b.Packages.Select(c => new { c.Name, c.Version, b.ProjectName }));

            return (from item in projects.GroupBy(grp => grp.Name).Select(s => s.Key)
                    where projects.Count(w => w.Name == item) > 1
                    select new SharedPackage
                    {
                        Name = item,
                        ProjectPackage = projects.Where(x => x.Name == item).Select(s => new SharedPackage.ProjectPackageVersion
                        {
                            ProjectName = s.ProjectName,
                            Version = s.Version
                        }).ToObservableCollection()
                    }).ToList();
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
