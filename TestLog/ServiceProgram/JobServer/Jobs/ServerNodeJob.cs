using Quartz;
using ServiceProgram.Common;
using ServiceProgram.EntityModel.Target;
using ServiceProgram.Environment;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ServiceProgram.JobServer.Jobs
{
    /// <summary>
    /// 
    /// </summary>
    public class ServerNodeJob : BaseJob.JobBase
    {
        public override string JobName => "ServerNodeJob";

        public override string Cron => "0 0/10 * * * ?";

        protected override void ExcuteJob(IJobExecutionContext context, CancellationTokenSource cancellationSource)
        {
            var time = DateTime.Now;
            SystemEnvironment info = new SystemEnvironment();
            ServerTargetHelper.ServerTarget(time, new ServerTargetModel
            {
                cpu_rate = info.GetCpuRate(),
                disk_rate = info.GetDriverRate(),
                mem_rate = info.GetMemRate()
            }) ;
        }
    }
}
