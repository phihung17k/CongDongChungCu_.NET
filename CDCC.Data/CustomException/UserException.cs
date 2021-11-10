using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDCC.Data.CustomException
{
    public class UserException : BaseException
    {
        public UserException()
        {
        }
        public UserException(string message) : base(message)
        {
        }
    }

    public class UserIDNotFoundException : UserException
    {
        public UserIDNotFoundException()
        {
        }

        public UserIDNotFoundException(string message) : base(message)
        {
        }
    }

    public class UserDeletedException : UserException
    {
        public UserDeletedException()
        {
        }
        public UserDeletedException(string message) : base(message)
        {
        }
    }
}
