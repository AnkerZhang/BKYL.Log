using System;
using System.Collections.Generic;
using System.Text;

namespace BKYL.Log.LogFactory
{
    /// <summary>
    /// 日志工厂接口
    /// </summary>
    public interface ILogFactory
    {
        /// <summary>
        /// 日志接口
        /// </summary>
        /// <param name="type_log">name</param>
        /// <returns>ILog</returns>
        ILog GetLog(LogEnum type_log);
    }
}
