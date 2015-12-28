using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace NugetCompare.UI
{
    public interface IPackageService
    {
        List<PackageConfig> LoadDependencies(string val);
    }

    public class PackageService : IPackageService
    {

        private const string PackagesConfigFileName = "packages.config";

        public List<PackageConfig> LoadDependencies(string val)
        {

            if (!Directory.Exists(val))
            {
                throw new DirectoryNotFoundException();
            }


            var locatedPackages = FindPackages(val).ToList();
            if (!locatedPackages.Any())
                return null;

            return locatedPackages.Select(fullPackagePath => new PackageConfig
            {
                Packages = GetPackages(fullPackagePath),
                Directory = Path.GetFullPath(fullPackagePath),
                ProjectFile = FindProjectFile(Path.GetDirectoryName(fullPackagePath))
            }).ToList();
        }

        private List<Package> GetPackages(string path)
        {
            var packages = new List<Package>();
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


    public class Package
    {
        public string Name { get; set; }
        public string Version { get; set; }

    }

    public class PackageConfig
    {
        public PackageConfig()
        {
            Packages = new List<Package>();
        }

        public List<Package> Packages { get; set; }
        public string Directory { get; set; }
        public string ProjectFile { get; set; }

        public string ProjectName()
        {
            return !string.IsNullOrWhiteSpace(ProjectFile) || ProjectFile.Contains(".") ? ProjectFile.Split('.')[0] : string.Empty;
        }
    }

}
