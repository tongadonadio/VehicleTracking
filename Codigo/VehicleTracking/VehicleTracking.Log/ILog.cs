using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Log.Events;

namespace VehicleTracking.Log
{
    public interface ILog
    {
        List<LogEvent> FindEvents(Predicate<LogEvent> predicate);
        void WriteEvent(LogEvent e);
    }
}
