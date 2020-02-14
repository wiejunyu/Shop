namespace WL.Infrastructure.Logging
{
    /// <summary>
    /// 跟踪日志工厂。
    /// </summary>
    public class TraceSourceLoggerFactory : ILoggerFactory
    {
        /// <summary>
        /// 创建一个跟踪日志的实例。
        /// </summary>
        /// <returns>跟踪日志的实例。</returns>
        public ILogger Create()
        {
            return new TraceSourceLogger();
        }
    }
}