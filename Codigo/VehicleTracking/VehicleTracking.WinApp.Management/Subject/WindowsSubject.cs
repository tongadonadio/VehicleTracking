using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VehicleTracking.WinApp.Management.Observer;

namespace VehicleTracking.WinApp.Management.Subject
{
    public class WindowsSubject
    {

        private List<WindowsObserver> observers;

        public WindowsSubject()
        {
            this.observers = new List<WindowsObserver>();
        }

        public void Attach(WindowsObserver observer)
        {
            this.observers.Add(observer);
        }

        public void Detach(WindowsObserver observer)
        {
            this.observers.Remove(observer);
        }

        public void Notify()
        {
            foreach(WindowsObserver observer in this.observers)
            {
                observer.UpdateForm();
            }
        }

    }
}
