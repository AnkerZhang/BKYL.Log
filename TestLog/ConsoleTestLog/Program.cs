using BKYL.Log;
using BKYL.Log.LogFactory;
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

            //#region 单程序高并发

            ////准备10个线程并发执行
            //var multipleTasks = new[]
            //{
            //    Task.Run(() => DoComplexCalculation(1)),
            //    Task.Run(() => DoComplexCalculation(2)),
            //    Task.Run(() => DoComplexCalculation(3)),
            //    Task.Run(() => DoComplexCalculation(4)),
            //    Task.Run(() => DoComplexCalculation(5)),
            //    Task.Run(() => DoComplexCalculation(6)),
            //    Task.Run(() => DoComplexCalculation(7)),
            //    Task.Run(() => DoComplexCalculation(8)),
            //    Task.Run(() => DoComplexCalculation(9)),
            //    Task.Run(() => DoComplexCalculation(10)),

            //    Task.Run(() => DoComplexCalculation(11)),
            //    Task.Run(() => DoComplexCalculation(12)),
            //    Task.Run(() => DoComplexCalculation(13)),
            //    Task.Run(() => DoComplexCalculation(14)),
            //    Task.Run(() => DoComplexCalculation(15)),
            //    Task.Run(() => DoComplexCalculation(16)),
            //    Task.Run(() => DoComplexCalculation(17))
            //};

            //var combinedTask = Task.WhenAll(multipleTasks);



            //#endregion

            //for (int i = 0; i < 10; i++)
            //{
            //    Thread.Sleep(1);
            //    DoComplexCalculation(i);
            //}

            Console.ReadLine();




        }

        public static void DoComplexCalculation(int i)
        {
            Console.WriteLine("压力测试d" + i);
            _log.Info("压力测试ggg");
        }
    }
}
