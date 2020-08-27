using BKYL.Log.LogFactory;
using System;
using System.Collections.Generic;
using System.Text;

namespace BKYL.Log
{
    /// <summary>
    /// 
    /// </summary>
    public class LogExtension
    {

        /// <summary>
        /// 获取全局日志对象 开箱即用
        /// </summary>
        /// <param name="log_type">日志类型</param>
        /// <returns>日志对象可全局使用</returns>
        public static ILog GetGlobalLog(BKYL.Log.LogFactory.LogEnum log_type)
        {
            NLog.LogManager.LoadConfiguration("nlog.config");
            BKYL.Log.LogFactory.ILogFactory f = new BKYL.Log.LogFactory.Imp.LogFactory();
            var log = f.GetLog(log_type);
            return log;
        }
    }
}
