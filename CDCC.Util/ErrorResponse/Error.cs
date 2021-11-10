using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CDCC.Util.ErrorResponse
{
    public class Error
    {
        public Error(HttpStatusCode statusCode, string message)
        {
            Code = (int)statusCode;
            Message = message;
        }
        public int Code { get; set; }
        public string Message { get; set; }
    }
}
