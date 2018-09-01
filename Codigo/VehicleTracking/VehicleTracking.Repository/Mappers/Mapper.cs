using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Data.Entities;
using VehicleTracking.Web.Models;

namespace WebTracking.Repository.Mappers
{
    public interface Mapper<T, U>
        where T : Entity
        where U : Model
    {

        T ToEntity(U model);

        U ToDTO(T Entity);
    }
}
