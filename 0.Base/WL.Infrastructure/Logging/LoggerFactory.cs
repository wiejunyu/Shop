namespace WL.Infrastructure.Logging
{
    using System;

    /// <summary>
    /// 日志工厂。
    /// </summary>
    public class LoggerFactory
    {
        #region Members

        /// <summary>
        /// 日志工厂的实例。
        /// </summary>
        private static readonly LoggerFactory Instance = new LoggerFactory();

        /// <summary>
        /// 当前日志工厂的实例。
        /// </summary>
        private ILoggerFactory _current = new NullLoggerFactory();

        #endregion

        #region Properties

        /// <summary>
        /// 当前日志工厂。
        /// </summary>
        public static ILoggerFactory Current
        {
            get
            {
                return Instance.InnerCurrent;
            }
        }

        /// <summary>
        /// 当前日志工厂。
        /// </summary>
        private ILoggerFactory InnerCurrent
        {
            get
            {
                return _current;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 设置当前使用的日志方式。
        /// </summary>
        /// <param name="logFactory">日志工厂。</param>
        public static void SetLoggerFactory(ILoggerFactory logFactory)
        {
            Instance.InnerSetLoggerFactory(logFactory);
        }

        #endregion

        #region Pricate Method

        /// <summary>
        /// 设置当前使用的日志方式。
        /// </summary>
        /// <param name="logFactory">日志工厂。</param>
        private void InnerSetLoggerFactory(ILoggerFactory logFactory)
        {
            if (logFactory == null)
            {
                throw new ArgumentNullException("logFactory");
            }

            _current = logFactory;
        }
        
        #endregion
    }
}