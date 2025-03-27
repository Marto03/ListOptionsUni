using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.CustomExceptions
{
    public class UserAlreadyExistsException : Exception
    {
        public UserAlreadyExistsException(string userName)
            : base($"Потребителското име \"{userName}\" вече съществува.") { }
    }
}
