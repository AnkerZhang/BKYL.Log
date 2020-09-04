using NLog;
using System;
using System.Collections.Generic;
using System.Text;

namespace BKYL.Log.LogFactory.Imp
{
    /// <summary>
    /// 
    /// </summary>
    public class Log:ILog
    {
        private ILogger _net_log;
        private ILogger _log;
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="log"></param>
        /// <param name="net_log"></param>
        public Log(ILogger log, ILogger net_log)
        {
            _log = log;
            _net_log = net_log;
        }

        /// <summary>
        /// Info级别日志
        /// </summary>
        /// <param name="message">记录的消息</param>
        /// <param name="is_warning">是否预警</param>
        /// <param name="is_console">是否打印控制台</param>
        public void Info(string message, bool is_warning = false, bool is_console = false)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                message = "_";
            }
            StringBuilder msg = new StringBuilder(message);
            if (is_console)
            {
                Console.WriteLine($"{_log.Name}{message}");
            }
            if (is_warning)
            {
                msg.Append($"#{Common.UtilHelper.IsWarning()}");
            }
            else
            {
                msg.Append("#0");
            }
            msg.Append("#_");
            _log.Info(Common.UtilHelper.filterLine(msg.ToString()));
            _net_log.Info(Common.UtilHelper.filterLine(msg.ToString()));
        }
        /// <summary>
        /// Info级别日志
        /// </summary>
        /// <param name="format">包含格式项的字符串</param>
        /// <param name="is_warning">是否预警</param>
        /// <param name="is_console">是否打印控制台</param>
        /// <param name="args">要格式化的参数</param>
        public void Info(string format, bool is_warning = false, bool is_console = false, params object[] args)
        {
            string message = string.Format(format, args);
            Info(message, is_warning, is_console);
        }
        /// <summary>
        /// Info级别日志
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="exception">异常对象</param>
        /// <param name="is_warning">是否预警</param>
        /// <param name="is_console">是否打印控制台</param>
        public void Info(string message, Exception exception, bool is_warning = false, bool is_console = false)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                message = "_";
            }
            StringBuilder msg = new StringBuilder(message);
            if (is_console)
            {
                Console.WriteLine($"{_log.Name}{message}{exception?.ToString()}");
            }
            if (is_warning)
            {
                msg.Append($"#{Common.UtilHelper.IsWarning()}");
            }
            else
            {
                msg.Append("#0");
            }
            string exception_str = "_";
            if (exception != null)
            {
                exception_str = exception.ToString();
            }

            _log.Info(exception, msg.ToString());
            _net_log.Info(Common.UtilHelper.filterLine(msg.Append($"#{exception_str}").ToString()));
        }
        /// <summary>
        /// Debug级别日志
        /// </summary>
        /// <param name="message">记录的消息</param>
        /// <param name="is_warning">是否预警</param>
        /// <param name="is_console">是否打印控制台</param>
        public void Debug(string message, bool is_warning = false, bool is_console = false)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                message = "_";
            }
            StringBuilder msg = new StringBuilder(message);
            if (is_console)
            {
                Console.WriteLine($"{_log.Name}{message}");
            }
            if (is_warning)
            {
                msg.Append($"#{Common.UtilHelper.IsWarning()}");
            }
            else
            {
                msg.Append("#0");
            }
            msg.Append("#_");
            _log.Debug(Common.UtilHelper.filterLine(msg.ToString()));
            _net_log.Debug(Common.UtilHelper.filterLine(msg.ToString()));
        }
        /// <summary>
        /// Debug级别日志
        /// </summary>
        /// <param name="format">包含格式项的字符串</param>
        /// <param name="is_warning">是否预警</param>
        /// <param name="is_console">是否打印控制台</param>
        /// <param name="args">要格式化的参数</param>
        public void Debug(string format, bool is_warning = false, bool is_console = false, params object[] args)
        {
            string message = string.Format(format, args);
            Debug(message, is_warning, is_console);
        }

        /// <summary>
        /// Debug级别日志
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="exception">异常对象</param>
        /// <param name="is_warning">是否预警</param>
        /// <param name="is_console">是否打印控制台</param>
        public void Debug(string message, Exception exception, bool is_warning = false, bool is_console = false)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                message = "_";
            }
            StringBuilder msg = new StringBuilder(message);
            if (is_console)
            {
                Console.WriteLine($"{_log.Name}{message}{exception?.ToString()}");
            }
            if (is_warning)
            {
                msg.Append($"#{Common.UtilHelper.IsWarning()}");
            }
            else
            {
                msg.Append("#0");
            }
            string exception_str = "_";
            if (exception != null)
            {
                exception_str = exception.ToString();
            }

            _log.Debug(exception, msg.ToString());
            _net_log.Debug(Common.UtilHelper.filterLine(msg.Append($"#{exception_str}").ToString()));
        }
        /// <summary>
        /// Warn级别日志
        /// </summary>
        /// <param name="message">记录的消息</param>
        /// <param name="is_warning">是否预警</param>
        /// <param name="is_console">是否打印控制台</param>
        public void Warn(string message, bool is_warning = false, bool is_console = false)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                message = "_";
            }
            StringBuilder msg = new StringBuilder(message);
            if (is_console)
            {
                Console.WriteLine($"{_log.Name}{message}");
            }
            if (is_warning)
            {
                msg.Append($"#{Common.UtilHelper.IsWarning()}");
            }
            else
            {
                msg.Append("#0");
            }
            msg.Append("#_");
            _log.Warn(Common.UtilHelper.filterLine(msg.ToString()));
            _net_log.Warn(Common.UtilHelper.filterLine(msg.ToString()));
        }
        /// <summary>
        /// Warn级别日志
        /// </summary>
        /// <param name="format">包含格式项的字符串</param>
        /// <param name="is_warning">是否预警</param>
        /// <param name="is_console">是否打印控制台</param>
        /// <param name="args">要格式化的参数</param>
        public void Warn(string format, bool is_warning = false, bool is_console = false, params object[] args)
        {
            string message = string.Format(format, args);
            Warn(message, is_warning, is_console);
        }

        /// <summary>
        /// Warn级别日志
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="exception">异常对象</param>
        /// <param name="is_warning">是否预警</param>
        /// <param name="is_console">是否打印控制台</param>
        public void Warn(string message, Exception exception, bool is_warning = false, bool is_console = false)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                message = "_";
            }
            StringBuilder msg = new StringBuilder(message);
            if (is_console)
            {
                Console.WriteLine($"{_log.Name}{message}{exception?.ToString()}");
            }
            if (is_warning)
            {
                msg.Append($"#{Common.UtilHelper.IsWarning()}");
            }
            else
            {
                msg.Append("#0");
            }
            string exception_str = "_";
            if (exception != null)
            {
                exception_str = exception.ToString();
            }

            _log.Warn(exception, msg.ToString());
            _net_log.Warn(Common.UtilHelper.filterLine(msg.Append($"#{exception_str}").ToString()));
        }
        /// <summary>
        /// Error级别日志
        /// </summary>
        /// <param name="message">记录的消息</param>
        /// <param name="is_warning">是否预警</param>
        /// <param name="is_console">是否打印控制台</param>
        public void Error(string message, bool is_warning = false, bool is_console = false)
        {
            _log.Error(message);
            if (string.IsNullOrWhiteSpace(message))
            {
                message = "_";
            }
            StringBuilder msg = new StringBuilder(message);
            if (is_console)
            {
                Console.WriteLine($"{_log.Name}{message}");
            }
            if (is_warning)
            {
                msg.Append($"#{Common.UtilHelper.IsWarning()}");
            }
            else
            {
                msg.Append("#0");
            }
            msg.Append("#_");
            _net_log.Error(Common.UtilHelper.filterLine(msg.ToString()));
        }
        /// <summary>
        /// Error级别日志
        /// </summary>
        /// <param name="format">包含格式项的字符串</param>
        /// <param name="is_warning">是否预警</param>
        /// <param name="is_console">是否打印控制台</param>
        /// <param name="args">要格式化的参数</param>
        public void Error(string format, bool is_warning = false, bool is_console = false, params object[] args)
        {
            string message = string.Format(format, args);
            Error(message, is_warning, is_console);
        }
        /// <summary>
        /// Error级别日志
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="exception">异常对象</param>
        /// <param name="is_warning">是否预警</param>
        /// <param name="is_console">是否打印控制台</param>
        public void Error(string message, Exception exception, bool is_warning = false, bool is_console = false)
        {
            _log.Error(exception,message);
            if (string.IsNullOrWhiteSpace(message))
            {
                message = "_";
            }
            StringBuilder msg = new StringBuilder(message);
            if (is_console)
            {
                Console.WriteLine($"{_log.Name}{message}{exception?.ToString()}");
            }
            if (is_warning)
            {
                msg.Append($"#{Common.UtilHelper.IsWarning()}");
            }
            else
            {
                msg.Append("#0");
            }
            string exception_str = "_";
            if (exception != null)
            {
                exception_str = exception.ToString();
            }
            msg.Append($"#{exception_str}");
            _net_log.Error(Common.UtilHelper.filterLine(msg.ToString()));
        }

        /// <summary>
        /// Fatal级别日志
        /// </summary>
        /// <param name="message">记录的消息</param>
        /// <param name="is_warning">是否预警</param>
        /// <param name="is_console">是否打印控制台</param>
        public void Fatal(string message, bool is_warning = false, bool is_console = false)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                message = "_";
            }
            StringBuilder msg = new StringBuilder(message);
            if (is_console)
            {
                Console.WriteLine($"{_log.Name}{message}");
            }
            if (is_warning)
            {
                msg.Append($"#{Common.UtilHelper.IsWarning()}");
            }
            else
            {
                msg.Append("#0");
            }
            msg.Append("#_");
            _log.Fatal(Common.UtilHelper.filterLine(msg.ToString()));
            _net_log.Fatal(Common.UtilHelper.filterLine(msg.ToString()));
        }
        /// <summary>
        /// Fatal级别日志
        /// </summary>
        /// <param name="format">包含格式项的字符串</param>
        /// <param name="is_warning">是否预警</param>
        /// <param name="is_console">是否打印控制台</param>
        /// <param name="args">要格式化的参数</param>
        public void Fatal(string format, bool is_warning = false, bool is_console = false, params object[] args)
        {
            string message = string.Format(format, args);
            Fatal(message, is_warning, is_console);
        }
        /// <summary>
        /// Fatal级别日志
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="exception">异常对象</param>
        /// <param name="is_warning">是否预警</param>
        /// <param name="is_console">是否打印控制台</param>
        public void Fatal(string message, Exception exception, bool is_warning = false, bool is_console = false)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                message = "_";
            }
            StringBuilder msg = new StringBuilder(message);
            if (is_console)
            {
                Console.WriteLine($"{_log.Name}{message}{exception?.ToString()}");
            }
            if (is_warning)
            {
                msg.Append($"#{Common.UtilHelper.IsWarning()}");
            }
            else
            {
                msg.Append("#0");
            }
            string exception_str = "_";
            if (exception != null)
            {
                exception_str = exception.ToString();
            }

            _log.Fatal(exception, msg.ToString());
            _net_log.Fatal(Common.UtilHelper.filterLine(msg.Append($"#{exception_str}").ToString()));
        }
    }
}
