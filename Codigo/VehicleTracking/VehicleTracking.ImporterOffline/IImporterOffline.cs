using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracking.ImporterOffline
{
    public interface IImporterOffline
    {
        string Id { get; }
        string Name { get; }
        string Version { get; }

        void Initialize();
    }
}
