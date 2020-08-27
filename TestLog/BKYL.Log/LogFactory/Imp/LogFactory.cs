using NLog;
using System;
using System.Text;

namespace BKYL.Log.LogFactory.Imp
{
    /// <summary>
    /// 工程实现类
    /// </summary>
    public class LogFactory : ILogFactory
    {
        /// <summary>
        /// 获取记录器
        /// </summary>
        /// <param name="type_log"></param>
        /// <returns></returns>
        public ILog GetLog(LogEnum type_log)
        {
            var name = type_log.ToString();
            var log= LogManager.GetLogger(name);
            return new Log(log, LogManager.GetLogger("net_"+name));
        }
    }
}
