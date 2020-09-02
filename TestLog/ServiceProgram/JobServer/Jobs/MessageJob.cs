using Dapper;
using Npgsql;
using Quartz;
using ServiceProgram.Common;
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
    public class MessageJob : BaseJob.JobBase
    {
        public override string JobName => "MessageJob";

        public override string Cron => "0 0/10 * * * ?";

        protected override void ExcuteJob(IJobExecutionContext context, CancellationTokenSource cancellationSource)
        {
            var time = DateTime.Now;
            var total = 0;
            using (var conn = new NpgsqlConnection($"PORT={EntityModel.ConfigModel.msg_config.port};DATABASE={EntityModel.ConfigModel.msg_config.database};HOST={EntityModel.ConfigModel.msg_config.host};PASSWORD={EntityModel.ConfigModel.msg_config.pwd};USER ID={EntityModel.ConfigModel.msg_config.user_id}"))
            {
                conn.Open();
                total = conn.ExecuteScalar<int>($"select count(0) from {EntityModel.ConfigModel.msg_config.table_name}");
                conn.Close();
            }
            ServerTargetHelper.MessageTarget(time, new EntityModel.Target.MessageTargetModel { task_count = total });
        }
    }
}
