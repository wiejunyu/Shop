namespace WL.Infrastructure.Logging
{
    /// <summary>
    /// 空日志工厂。
    /// </summary>
    public class NullLoggerFactory : ILoggerFactory
    {
        /// <summary>
        /// 创建一个空日志的实例。
        /// </summary>
        /// <returns>空日志的实例。</returns>
        public ILogger Create()
        {
            return new NullLogger();
        }
    }
}