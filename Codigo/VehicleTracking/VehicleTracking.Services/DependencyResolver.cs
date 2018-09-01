using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.DependencyResolver;
using VehicleTracking.Services.ServiceImplementations;
using VehicleTracking.Services.Services;

namespace VehicleTracking.Services
{
    [Export(typeof(Component))]
    public class DependencyResolver : Component
    {
        public void SetUp(RegisterComponent registerComponent)
        {
            registerComponent.RegisterType<RoleService, RoleServiceImp>();
            registerComponent.RegisterType<UserService, UserServiceImpl>();
            registerComponent.RegisterType<BatchService, BatchServiceImpl>();
            registerComponent.RegisterType<VehicleService, VehicleServiceImpl>();
            registerComponent.RegisterType<ZoneService, ZoneServiceImp>();
            registerComponent.RegisterType<HistoryService, HistoryServiceImp>();
            registerComponent.RegisterType<InspectionService, InspectionServiceImpl>();
            registerComponent.RegisterType<TransportService, TransportServiceImpl>();
            registerComponent.RegisterType<FlowService, FlowServiceImp>();
        }
    }
}
