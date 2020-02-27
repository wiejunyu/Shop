namespace WL.Infrastructure.Commom
{
    public class ReturnResult
    {
        /// <summary>
        /// 返回状态码，与 HttpStatusCode 一致
        /// </summary>
        public int Status
        {
            get;
            set;
        }

        public string Message
        {
            get;
            set;
        }

        public ReturnResult()
        {
            Status = (int)ReturnResultStatus.ServerError;
        }
    }
}
