using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.DependencyResolver;
using VehicleTracking.Repository;
using WebTracking.Repository;

namespace VehicleTracking.Services
{
    [Export(typeof(Component))]
    public class DependencyResolver : Component
    {
        public void SetUp(RegisterComponent registerComponent)
        {
            registerComponent.RegisterType<RoleDAO, RoleDAOImp>();
            registerComponent.RegisterType<BatchDAO, BatchDAOImp>();
            registerComponent.RegisterType<UserDAO, UserDAOImp>();
            registerComponent.RegisterType<VehicleDAO, VehicleDAOImpl>();
            registerComponent.RegisterType<ZoneDAO, ZoneDAOImp>();
            registerComponent.RegisterType<HistoryDAO, HistoryDAOImp>();
            registerComponent.RegisterType<InspectionDAO, InspectionDAOImp>();
            registerComponent.RegisterType<TransportDAO, TransportDAOImp>();
            registerComponent.RegisterType<FlowDAO, FlowDAOImp>();
        }
    }
}
