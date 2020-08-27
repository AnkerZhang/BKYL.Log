using Quartz;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceProgram.JobServer.BaseJob
{
    /// <summary>
    /// 所有计划的基类
    /// 并且所有任务不允许并发执行
    /// </summary>
    [DisallowConcurrentExecution]
    public abstract class JobBase : IJob
    {
        /// <summary>
        /// 计划任务名称
        /// </summary>
        /// <returns></returns>
        public abstract string JobName { get; }
        /// <summary>
        /// Job执行的超时时间(毫秒)，默认30秒
        /// </summary>
        public virtual int JobTimeout => 30 * 1000;

        public static Bkyl.Log.LogGateway.ILogGateway _log = new Bkyl.Log.LogGateway.Imp.LogGateway();
        /// <summary>
        /// 子类重写Cron表达式
        /// http://cron.qqe2.com/
        /// </summary>
        public abstract string Cron { get; }// "0/5 * * * * ?"; 5秒执行一次
        public Task Execute(IJobExecutionContext context)
        {
            Timer timer = null;
            try
            {
                timer = new Timer(CancelOperation, null, JobTimeout, Timeout.Infinite);
                ExcuteJob(context, CancellationSource);
            }
            catch (Exception ex)
            {
                _log.Error(this.GetType().Name + "error:" + ex.Message, ex, true, true);
            }
            finally
            {
                if (timer != null)
                    timer.Dispose();
            }
            return Task.CompletedTask;
        }
        /// <summary>
        /// Job具体类去实现自己的逻辑
        /// </summary>
        protected abstract void ExcuteJob(IJobExecutionContext context, CancellationTokenSource cancellationSource);
        /// <summary>
        /// 取消资源
        /// </summary>
        public CancellationTokenSource CancellationSource => new CancellationTokenSource();
        /// <summary>
        /// 当某个job超时时，它将被触发，可以发一些通知邮件等
        /// </summary>
        /// <param name="arg"></param>
        private void CancelOperation(object arg)
        {
            CancellationSource.Cancel();
            Console.WriteLine("Job执行超时，已经取消，等待下次调度...");
        }
    }
}
