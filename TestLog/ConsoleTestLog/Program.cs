using BKYL.Log;
using BKYL.Log.LogFactory;
using NLog;
using NLog.Config;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleTestLog
{
    class Program
    {
        private static ILog _log;
        static void Main(string[] args)
        {
            //获取全局日志对象 该方法全局只需要获取一次即可
            _log = LogExtension.GetGlobalLog(BKYL.Log.LogFactory.LogEnum.console);

            #region 注释

            _log.Error("这个出错了", new Exception("我是一个错误信息"), true, true);
            _log.Error("这个出错了", new Exception("我是一个错误信息22222"), true, true);
            #endregion

          Console.ReadLine();
        }
    }
}
