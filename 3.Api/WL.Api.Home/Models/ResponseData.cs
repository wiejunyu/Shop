using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WL.Infrastructure.Commom;

namespace WL.API.Models
{
    public class ResponseData<T>
    {
        /// <summary>
        /// 状态 0表示正常
        /// </summary>
        public int status { get; set; }
        public string message { get; set; }
        public T data { get; set; }


        public ResponseData() { }

        public ResponseData(T data)
        {
            this.data = data;
        }

        public void HandleException(Exception ex)
        {
            this.status = (int)ReturnResultStatus.BLLError;
            this.message = ex.Message;
        }

        public void HandleException(AppException ex)
        {
            this.status = ex.ErrorStatus;
            this.message = ex.Message;
        }

    }
}