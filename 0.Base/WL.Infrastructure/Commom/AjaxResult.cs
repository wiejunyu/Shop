using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WL.Infrastructure.Common
{
    public class AjaxResult
    {
        ReturnResultStatus _Status = ReturnResultStatus.Succeed;
        public AjaxResult()
        {
        }
        public AjaxResult(ReturnResultStatus Status)
        {
            this._Status = Status;
        }

        public AjaxResult(string Message)
        {
            this.Message = Message;
        }
        public AjaxResult(ReturnResultStatus Status, string Message)
        {
            this.Status = Status;
            this.Message = (Message);
        }


        public ReturnResultStatus Status { get { return this._Status; } set { this._Status = value; } }
        public string Message { get; set; }

        public static AjaxResult CreateResult(string Message = null)
        {
            AjaxResult result = CreateResult(ReturnResultStatus.Succeed, Message);
            return result;
        }
        public static AjaxResult CreateResult(ReturnResultStatus Status, string Message = null)
        {
            AjaxResult result = new AjaxResult(Status);
            result.Message = Message;
            return result;
        }
        public static AjaxResult<T> CreateResult<T>(T Data)
        {
            AjaxResult<T> result = CreateResult<T>(ReturnResultStatus.Succeed, Data);
            return result;
        }
        public static AjaxResult<T> CreateResult<T>(ReturnResultStatus Status)
        {
            AjaxResult<T> result = CreateResult<T>(Status, default(T));
            return result;
        }
        public static AjaxResult<T> CreateResult<T>(ReturnResultStatus Status, T Data)
        {
            AjaxResult<T> result = new AjaxResult<T>(Status);
            result.Data = Data;
            return result;
        }
        public static AjaxResult<T> CreateResult<T>(ReturnResultStatus Status, string Message, T Data)
        {
            AjaxResult<T> result = new AjaxResult<T>(Status);
            result.Data = Data;
            result.Message = Message;
            return result;
        }
    }
    public class AjaxResult<T> : AjaxResult
    {
        public AjaxResult()
        {
        }
        public AjaxResult(ReturnResultStatus Status)
            : base(Status)
        {
        }
        public AjaxResult(ReturnResultStatus Status, T Data)
            : base(Status)
        {
            this.Data = Data;
        }

        public AjaxResult(ReturnResultStatus Status, string Message, T Data) : base(Status, Message)
        {
            this.Data = Data;
        }

        public T Data { get; set; }
    }

}
