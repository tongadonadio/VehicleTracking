using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.ImporterOffline;

namespace VehicleTracking.Services.ImporterOffline
{
    public class ImporterOffline
    {
        private Dictionary<string, IImporterOffline> importers;
        private string importersFolderRelativePath;

        public ImporterOffline(string folderPath)
        {
            this.importers = new Dictionary<string, IImporterOffline>();
            this.importersFolderRelativePath = folderPath;

            LoadImporters();
        }

        private void LoadImporters()
        {
            var importerFolderPath = Directory.GetCurrentDirectory() + "\\" + this.importersFolderRelativePath;

            if (Directory.Exists(importerFolderPath))
            {
                var folderFiles = Directory.GetFiles(importerFolderPath);

                foreach (var file in folderFiles)
                {
                    var importerAssembly = Assembly.LoadFile(file);
                    var importerAssemblyTypes = importerAssembly.GetExportedTypes();

                    foreach (var importerAssemblyType in importerAssemblyTypes)
                    {
                        if (typeof(IVehicleImportOffline).IsAssignableFrom(importerAssemblyType))
                        {
                            LoadImporterOfType(importerAssemblyType);
                        }
                    }
                }
            }
        }

        private void LoadImporterOfType(Type type)
        {
            var instance = Activator.CreateInstance(type) as IImporterOffline;
            instance.Initialize();
            this.importers.Add(instance.Id, instance);
        }

        public List<IImporterOffline> All()
        {
            return this.importers.Values.ToList();
        }

        public List<T> AllOfType<T>() where T: IImporterOffline
        {
            var importersOfTypeT = this.importers.Values.Where(i => typeof(T).IsAssignableFrom(i.GetType()));
            return importersOfTypeT.Cast<T>().ToList();
        }

    }
}
