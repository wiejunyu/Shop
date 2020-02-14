namespace WL.Infrastructure.Logging
{
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Security;

    /// <summary>
    /// 跟踪日志。
    /// </summary>
    public class TraceSourceLogger : ILogger
    {
        #region Members

        /// <summary>
        /// 跟踪源。
        /// </summary>
        private readonly TraceSource _source;

        #endregion Members

        #region 初始化跟踪日志

        /// <summary>
        /// 初始化跟踪日志。
        /// </summary>
        public TraceSourceLogger()
        {
            //创建一个源追踪实体。
            _source = new TraceSource("Liberty");
        }

        #endregion 初始化跟踪日志

        #region private method

        /// <summary>
        /// 获得消息。
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private static string GetMessage(string message, params object[] args)
        {
            if (args == null || args.Length <= 0)
                return message;

            return string.Format(CultureInfo.InvariantCulture, message ?? string.Empty, args);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="message"></param>
        private void TraceInternal(TraceEventType eventType, string message)
        {
            try
            {
                //指定可输出的等级
                _source.Switch.Level = SourceLevels.Critical | SourceLevels.Error | SourceLevels.Warning | SourceLevels.Information | SourceLevels.Verbose;

                //增加侦听器
                _source.Listeners.Add(new TextWriterTraceListener(GetLogFileStream(eventType)));

                //写入消息
                _source.TraceEvent(eventType, (int)eventType, message);
                _source.Flush();
                _source.Close();
            }
            catch (SecurityException)
            {
            }
        }

        /// <summary>
        /// 流的方式打开文件
        /// </summary>
        /// <returns>文件流</returns>
        private static StreamWriter GetLogFileStream(TraceEventType eventType)
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Temp", "log", GetFolderName(eventType), DateTime.Now.ToString("yyyyMMdd"));

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            path = Path.Combine(path, DateTime.Now.ToString("yyyyMMddHHmmssfff") + "." + (int)eventType + ".log");

            return File.AppendText(path);
        }

        /// <summary>
        /// 获得由指定类型转换成的文件夹名称。
        /// </summary>
        /// <param name="eventType">跟踪事件类型。</param>
        /// <returns>文件夹名称。</returns>
        private static string GetFolderName(TraceEventType eventType)
        {
            return eventType.ToString();
        }

        #endregion private method

        #region ILogger Members

        /// <summary>
        /// 日志信息。
        /// </summary>
        /// <param name="message">日志信息。</param>
        /// <param name="args">信息参数。</param>
        public void LogInfo(string message, params object[] args)
        {
            var messageToTrace = GetMessage(message, args);

            TraceInternal(TraceEventType.Information, messageToTrace);
        }

        /// <summary>
        /// 非关键性错误日志信息。
        /// </summary>
        /// <param name="message">非关键性错误日志信息。</param>
        /// <param name="args">信息参数。</param>
        public void LogWarning(string message, params object[] args)
        {
            var messageToTrace = GetMessage(message, args);

            TraceInternal(TraceEventType.Warning, messageToTrace);
        }

        /// <summary>
        /// 可恢复错误日志信息。
        /// </summary>
        /// <param name="message">可恢复错误日志信息。</param>
        /// <param name="args">信息参数。</param>
        public void LogError(string message, params object[] args)
        {
            var messageToTrace = GetMessage(message, args);

            TraceInternal(TraceEventType.Error, messageToTrace);
        }

        /// <summary>
        /// 可恢复错误日志信息。
        /// </summary>
        /// <param name="message">可恢复错误日志信息。</param>
        /// <param name="exception">异常对象。</param>
        /// <param name="args">信息参数。</param>
        public void LogError(string message, Exception exception, params object[] args)
        {
            var messageToTrace = GetMessage(message, args);

            // The ToString() create a string representation of the current exception
            var exceptionData = exception != null ? exception.ToString() : string.Empty;

            TraceInternal(TraceEventType.Error, string.Format(CultureInfo.InvariantCulture, "{0} Exception:{1}", messageToTrace, exceptionData));
        }

        /// <summary>
        /// 调试日志信息。
        /// </summary>
        /// <param name="message">调试日志信息。</param>
        /// <param name="args">信息参数。</param>
        public void Debug(string message, params object[] args)
        {
            var messageToTrace = GetMessage(message, args);

            TraceInternal(TraceEventType.Verbose, messageToTrace);
        }

        /// <summary>
        /// 调试日志信息。
        /// </summary>
        /// <param name="message">调试日志信息。</param>
        /// <param name="exception">异常对象。</param>
        /// <param name="args">信息参数。</param>
        public void Debug(string message, Exception exception, params object[] args)
        {
            var messageToTrace = GetMessage(message, args);

            // The ToString() create a string representation of the current exception
            var exceptionData = exception != null ? exception.ToString() : string.Empty;

            TraceInternal(TraceEventType.Error, string.Format(CultureInfo.InvariantCulture, "{0} Exception:{1}", messageToTrace, exceptionData));
        }

        /// <summary>
        /// 调试日志信息。
        /// </summary>
        /// <param name="item">跟踪对象。</param>
        public void Debug(object item)
        {
            if (item != null)
            {
                TraceInternal(TraceEventType.Verbose, item.ToString());
            }
        }

        /// <summary>
        /// 致命错误信息。
        /// </summary>
        /// <param name="message">致命错误信息。</param>
        /// <param name="args">信息参数。</param>
        public void Fatal(string message, params object[] args)
        {
            var messageToTrace = GetMessage(message, args);

            TraceInternal(TraceEventType.Critical, messageToTrace);
        }

        /// <summary>
        /// 致命错误信息。
        /// </summary>
        /// <param name="message">致命错误信息。</param>
        /// <param name="exception">异常对象。</param>
        /// <param name="args">信息参数。</param>
        public void Fatal(string message, Exception exception, params object[] args)
        {
            var messageToTrace = GetMessage(message, args);

            // The ToString() create a string representation of the current exception
            var exceptionData = exception != null ? exception.ToString() : string.Empty;

            TraceInternal(TraceEventType.Critical, string.Format(CultureInfo.InvariantCulture, "{0} Exception:{1}", messageToTrace, exceptionData));
        }

        #endregion ILogger Members
    }
}