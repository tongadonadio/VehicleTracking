using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebTracking.Repository.Exceptions
{
    public class IncorrectUserRoleException : Exception
    {
        public IncorrectUserRoleException() : base() { }
        public IncorrectUserRoleException(string message) : base(message) { }
        public IncorrectUserRoleException(string message, Exception inner) : base(message, inner) { }
    }
}
