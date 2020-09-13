using BKYL.Log;
using System;

namespace ConsoleTestLog
{
    class Program
    {
        static void Main(string[] args)
        {
            //获取全局日志对象 该方法全局只需要获取一次即可
            var _log = LogExtension.GetGlobalLog(BKYL.Log.LogFactory.LogEnum.console);

            #region 使用ILogGateway

            _log.Error("这个出错了", new Exception("我是一个错误信息"), true, true);
            try
            {
                int i = 0;
                i = i / i;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex);
            }

            //还有Dbug、Error、Fatal、Warn方法 使用方法一样
            //采集的日志格式如下
            #endregion

            Console.WriteLine("日志记录完成");
            Console.ReadLine();
        }
    }
}
