using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDCC.Data.CustomException
{
    public class BuildingException : BaseException
    {
        public BuildingException()
        {
        }

        public BuildingException(string message) : base(message)
        {
        }
    }

    public class BuildingIDNotFoundException : BuildingException
    {
        public BuildingIDNotFoundException()
        {
        }

        public BuildingIDNotFoundException(string message) : base(message)
        {
        }
    }

    public class BuildingDeletedException : BuildingException
    {
        public BuildingDeletedException()
        {
        }

        public BuildingDeletedException(string message) : base(message)
        {
        }
    }
}
