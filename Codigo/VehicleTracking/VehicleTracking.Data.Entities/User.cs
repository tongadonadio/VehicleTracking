using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracking.Data.Entities
{
    public class User : Entity
    {
        public User()
        {
            this.Id = Guid.NewGuid();
        }

        public User(string Name, string LastName, string UserName, int Phone, string Password, Role Role)
        {
            this.Id = Guid.NewGuid();
            this.Name = Name;
            this.LastName = LastName;
            this.UserName = UserName;
            this.Phone = Phone;
            this.Password = Password;
            this.IdRole = Role;
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public int Phone { get; set; }

        public string Password { get; set; }

        public virtual Role IdRole { get; set; }

    }
}
