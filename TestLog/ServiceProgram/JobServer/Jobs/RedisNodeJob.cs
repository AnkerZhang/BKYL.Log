using Quartz;
using ServiceProgram.Common;
using ServiceProgram.EntityModel.Target;
using ServiceProgram.Environment;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace ServiceProgram.JobServer.Jobs
{
    /// <summary>
    /// 
    /// </summary>
    public class RedisNodeJob : BaseJob.JobBase
    {
        public override string JobName => "RedisNodeJob";

        public override string Cron => "0 0/10 * * * ?";

        protected override void ExcuteJob(IJobExecutionContext context, CancellationTokenSource cancellationSource)
        {
            var time = DateTime.Now;
            foreach (var item in EntityModel.ConfigModel.redis_configs)
            {
                string res = HttpHelper.HttpGet(item.url);
                if (string.IsNullOrWhiteSpace(res))
                {
                    return;
                }
                RedisTargetModel redis_target = new RedisTargetModel { redis_name = item.node_name };

                Regex used_bytes_reg = new Regex("redis_memory_used_bytes .+");
                MatchCollection matchs = used_bytes_reg.Matches(res);
                string used_bytes_val = matchs[2].Value;
                var redis_memory_used_bytes = UtilHelper.ChangeDataToD(used_bytes_val.Replace("redis_memory_used_bytes", "").Trim());
                redis_target.used_bytes_val = redis_memory_used_bytes;

                Regex max_bytes_reg = new Regex("redis_memory_max_bytes .+");
                matchs = max_bytes_reg.Matches(res);
                string max_bytes_val = matchs[2].Value;
                var redis_memory_max_bytes = UtilHelper.ChangeDataToD(max_bytes_val.Replace("redis_memory_max_bytes", "").Trim());
                redis_target.max_bytes_val = redis_memory_max_bytes;

                Regex connected_clients_reg = new Regex("redis_connected_clients .+");
                matchs = connected_clients_reg.Matches(res);
                string connected_clients_val = matchs[2].Value;
                var redis_connected_clients = int.Parse(connected_clients_val.Replace("redis_connected_clients", "").Trim());
                redis_target.connected_clients_val = redis_connected_clients;

                Regex key_count_reg = new Regex("redis_db_keys{db=.+");
                matchs = key_count_reg.Matches(res);
                int key_count = 0;
                foreach (var match in matchs)
                {
                    Regex number_reg = new Regex(@" \d+");
                    var m = number_reg.Match(match.ToString());
                    if (int.TryParse(m.Value, out int tmp))
                    {
                        key_count += tmp;
                    }
                }
                redis_target.redis_db_keys = key_count;

                ServerTargetHelper.RedisTarget(time, redis_target);
            }
        }
    }
}
