using System;
using System.Collections.Generic;
using System.Text;

namespace BKYL.Log.LogFactory
{
    /// <summary>
    /// 
    /// </summary>
    public interface ILog
    {
        /// <summary>
        /// Info级别日志
        /// </summary>
        /// <param name="message">记录的消息</param>
        /// <param name="is_warning">是否预警</param>
        /// <param name="is_console">是否打印控制台</param>
        void Info(string message, bool is_warning = false, bool is_console = false);
        /// <summary>
        /// Info级别日志
        /// </summary>
        /// <param name="format">包含格式项的字符串</param>
        /// <param name="is_warning">是否预警</param>
        /// <param name="is_console">是否打印控制台</param>
        /// <param name="args">要格式化的参数</param>
        void Info(string format, bool is_warning = false, bool is_console = false, params object[] args);
        /// <summary>
        /// Info级别日志
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="exception">异常对象</param>
        /// <param name="is_warning">是否预警</param>
        /// <param name="is_console">是否打印控制台</param>
        void Info(string message, Exception exception, bool is_warning = false, bool is_console = false);
        /// <summary>
        /// Debug级别日志
        /// </summary>
        /// <param name="message">记录的消息</param>
        /// <param name="is_warning">是否预警</param>
        /// <param name="is_console">是否打印控制台</param>
        void Debug(string message, bool is_warning = false, bool is_console = false);
        /// <summary>
        /// Debug级别日志
        /// </summary>
        /// <param name="format">包含格式项的字符串</param>
        /// <param name="is_warning">是否预警</param>
        /// <param name="is_console">是否打印控制台</param>
        /// <param name="args">要格式化的参数</param>
        void Debug(string format, bool is_warning = false, bool is_console = false, params object[] args);

        /// <summary>
        /// Debug级别日志
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="exception">异常对象</param>
        /// <param name="is_warning">是否预警</param>
        /// <param name="is_console">是否打印控制台</param>
        void Debug(string message, Exception exception, bool is_warning = false, bool is_console = false);
        /// <summary>
        /// Warn级别日志
        /// </summary>
        /// <param name="message">记录的消息</param>
        /// <param name="is_warning">是否预警</param>
        /// <param name="is_console">是否打印控制台</param>
        void Warn(string message, bool is_warning = false, bool is_console = false);
        /// <summary>
        /// Warn级别日志
        /// </summary>
        /// <param name="format">包含格式项的字符串</param>
        /// <param name="is_warning">是否预警</param>
        /// <param name="is_console">是否打印控制台</param>
        /// <param name="args">要格式化的参数</param>
        void Warn(string format, bool is_warning = false, bool is_console = false, params object[] args);

        /// <summary>
        /// Warn级别日志
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="exception">异常对象</param>
        /// <param name="is_warning">是否预警</param>
        /// <param name="is_console">是否打印控制台</param>
        void Warn(string message, Exception exception, bool is_warning = false, bool is_console = false);
        /// <summary>
        /// Error级别日志
        /// </summary>
        /// <param name="message">记录的消息</param>
        /// <param name="is_warning">是否预警</param>
        /// <param name="is_console">是否打印控制台</param>
        void Error(string message, bool is_warning = false, bool is_console = false);
        /// <summary>
        /// Error级别日志
        /// </summary>
        /// <param name="format">包含格式项的字符串</param>
        /// <param name="is_warning">是否预警</param>
        /// <param name="is_console">是否打印控制台</param>
        /// <param name="args">要格式化的参数</param>
        void Error(string format, bool is_warning = false, bool is_console = false, params object[] args);
        /// <summary>
        /// Error级别日志
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="exception">异常对象</param>
        /// <param name="is_warning">是否预警</param>
        /// <param name="is_console">是否打印控制台</param>
        void Error(string message, Exception exception, bool is_warning = false, bool is_console = false);

        /// <summary>
        /// Fatal级别日志
        /// </summary>
        /// <param name="message">记录的消息</param>
        /// <param name="is_warning">是否预警</param>
        /// <param name="is_console">是否打印控制台</param>
        void Fatal(string message, bool is_warning = false, bool is_console = false);
        /// <summary>
        /// Fatal级别日志
        /// </summary>
        /// <param name="format">包含格式项的字符串</param>
        /// <param name="is_warning">是否预警</param>
        /// <param name="is_console">是否打印控制台</param>
        /// <param name="args">要格式化的参数</param>
        void Fatal(string format, bool is_warning = false, bool is_console = false, params object[] args);
        /// <summary>
        /// Fatal级别日志
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="exception">异常对象</param>
        /// <param name="is_warning">是否预警</param>
        /// <param name="is_console">是否打印控制台</param>
        void Fatal(string message, Exception exception, bool is_warning = false, bool is_console = false);
    }
}
