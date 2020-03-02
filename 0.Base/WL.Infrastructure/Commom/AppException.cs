using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WL.Infrastructure.Commom
{
    public class AppException : Exception
    {
        public int ErrorStatus { get; set; }

        public string ErrorCode { get; set; }

        public int Level { get; set; }



        public AppException(int level, int errorStatus, string errorCode, string message, Exception exception)
            : base(message, exception)
        {
            this.ErrorStatus = errorStatus;
            this.Level = level;
            this.ErrorCode = errorCode;
        }


        public AppException(int errorStatus, string errorCode, string message)
            : this(0, errorStatus, errorCode, message, null)
        {
        }

        public AppException(string errorCode, string message)
            : this((int)ReturnResultStatus.BLLError, errorCode, message)
        {
        }

        public AppException(string message)
            : this(string.Empty, message)
        {
        }

        public AppException(Exception e)
            : base(e.Message, e)
        {
        }
    }
}
