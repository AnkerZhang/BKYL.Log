using Dapper;
using Npgsql;
using Quartz;
using ServiceProgram.Common;
using ServiceProgram.EntityModel.RuleTarget;
using ServiceProgram.EntityModel.Target;
using ServiceProgram.Environment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ServiceProgram.JobServer.Jobs
{
    /// <summary>
    /// 
    /// </summary>
    public class RuleConfigTaskJob : BaseJob.JobBase
    {
        public override string JobName => "RuleConfigTaskJob";

        public override string Cron => "30 0/10 * * * ?";//每隔10分钟执行一次

        protected override void ExcuteJob(IJobExecutionContext context, CancellationTokenSource cancellationSource)
        {
            ServerTargetHelper.DataConfig();
            ServerTargetHelper.SaveSerWorker();
        }
    }
}
