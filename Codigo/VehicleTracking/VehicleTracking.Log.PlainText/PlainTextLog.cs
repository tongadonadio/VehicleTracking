using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Log;
using VehicleTracking.Log.Events;
using VehicleTracking.Web.Models;

namespace VehicleTracking.Log.PlainText
{
    public class PlainTextLog : ILog
    {
        private string fileRelativePath;
        public string LogFilePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\log.txt";

        public PlainTextLog(string logFileRelativePath)
        {
            this.fileRelativePath = logFileRelativePath;
        }

        public List<LogEvent> FindEvents(Predicate<LogEvent> predicate)
        {
            var logEvents = new List<LogEvent>();
            var logEventTypeCache = new Dictionary<string, Type>();
            if (!File.Exists(this.LogFilePath))
            {
                var newFile = File.Create(this.LogFilePath);
                newFile.Close();
            }
            var logFileContents = File.ReadAllText(this.LogFilePath);

            foreach (var line in logFileContents.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {
                try
                {
                    var aux = line.Split(new string[] { " - " }, StringSplitOptions.RemoveEmptyEntries);
                    var typeName = aux[0].Split(']').First().Trim(new char[] { '[', ']' });
                    var dateTime = DateTime.ParseExact(aux[0].Split(']').Last().Trim(), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                    var userName = aux[1].Trim();
                    UserDTO user = new UserDTO();
                    user.UserName = userName;
                    var content = aux.Count() > 2 ? aux[2].Trim() : "";

                    var logEventType = typeof(Nullable);

                    if (logEventTypeCache.ContainsKey(typeName))
                    {
                        logEventType = logEventTypeCache[typeName];
                    }
                    else
                    {
                        var assembly = AppDomain.CurrentDomain.GetAssemblies().Single(a => a.GetName().Name == "VehicleTracking.Log");
                        var type = assembly.GetTypes().Single(t => t.FullName == "VehicleTracking.Log.Events." + typeName);

                        logEventType = type;
                        logEventTypeCache.Add(typeName, type);
                    }

                    VehicleDTO vehicle = new VehicleDTO();
                    vehicle.Vin = (logEventType.Name == "VehicleImportEvent") ? content.Split(':')[1] : "";

                    var logEvent = (logEventType.Name == "VehicleImportEvent") ? Activator.CreateInstance(logEventType, user, vehicle)
                                                                               : Activator.CreateInstance(logEventType, user);
                    var logEventTyped = logEvent as LogEvent;

                    logEventType.GetProperty("Date").SetValue(logEvent, dateTime, null);
                    logEventType.GetProperty("Content").SetValue(logEvent, content, null);

                    if (predicate(logEventTyped))
                    {
                        logEvents.Add(logEventTyped);
                    }
                }
                catch
                {

                }
            }

            return logEvents;
        }

        public void WriteEvent(LogEvent e)
        {
            var eventString = string.Format("[{0}] {1} - {2} - {3}", e.GetType().Name, e.Date.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), e.User.UserName, e.Content);
            File.AppendAllText(this.LogFilePath, eventString + Environment.NewLine);
        }
    }
}
