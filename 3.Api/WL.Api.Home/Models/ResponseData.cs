using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WL.Infrastructure.Common;

namespace WL.API.Models
{
    public class ResponseData<T>
    {
        /// <summary>
        /// 状态 200表示正常
        /// </summary>
        public int Status { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }


        public ResponseData() { }

        public ResponseData(T Data)
        {
            this.Data = Data;
        }

        public void HandleException(Exception ex)
        {
            this.Status = (int)ReturnResultStatus.BLLError;
            this.Message = ex.Message;
        }

        public void HandleException(AppException ex)
        {
            this.Status = ex.ErrorStatus;
            this.Message = ex.Message;
        }

    }
}