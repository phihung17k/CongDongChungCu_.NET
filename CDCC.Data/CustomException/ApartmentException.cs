using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDCC.Data.CustomException
{
    public class ApartmentException : BaseException
    {
        public ApartmentException()
        {
        }
        public ApartmentException(string message) : base(message)
        {
        }
    }

    public class ApartmentIDNotFoundException : ApartmentException
    {
        public ApartmentIDNotFoundException()
        {
        }

        public ApartmentIDNotFoundException(string message) : base(message)
        {
        }
    }

    public class ApartmentDeletedException : ApartmentException
    {
        public ApartmentDeletedException()
        {
        }

        public ApartmentDeletedException(string message) : base(message)
        {
        }
    }
}
