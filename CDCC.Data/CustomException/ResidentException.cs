using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDCC.Data.CustomException
{
    public class ResidentException : BaseException
    {
        public ResidentException()
        {
        }

        public ResidentException(string message) : base(message)
        {
        }
    }

    public class ResidentIDNotFoundException : ResidentException
    {
        public ResidentIDNotFoundException()
        {
        }

        public ResidentIDNotFoundException(string message) : base(message)
        {
        }
    }

    //public class ResidentIDExistedException : ResidentException
    //{
    //    public ResidentIDExistedException()
    //    {
    //    }

    //    public ResidentIDExistedException(string message) : base(message)
    //    {
    //    }
    //}

    public class ResidentDeletedException : ResidentException
    {
        public ResidentDeletedException()
        {
        }

        public ResidentDeletedException(string message) : base(message)
        {
        }
    }

}
