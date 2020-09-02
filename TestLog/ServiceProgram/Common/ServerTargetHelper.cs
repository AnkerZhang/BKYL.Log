using System;
using System.Collections.Generic;
using System.Text;
using NLog;
using ServiceProgram.EntityModel;
using ServiceProgram.EntityModel.Target;

namespace ServiceProgram.Common
{
    public class ServerTargetHelper
    {
        /// <summary>
        /// 日志对象
        /// </summary>
        private static ILogger _log = LogManager.GetCurrentClassLogger();
        private static long server_time = 0;
        private static long message_time = 0;
        private static long redis_time = 0;

        private static Dictionary<string, long> pg_total_site = new Dictionary<string, long>(); //单位KB
        //#时间#ServerName#NodelName#预留字段#Json数据#预警邮箱#是否预警#预警内容#

        /// <summary>
        /// 
        /// </summary>
        public static void ServerTarget(DateTime time, ServerTargetModel target)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{time.ToString("yyyy-MM-dd HH:mm:ss.ffff")}#");
            sb.Append("target_server_node#");
            string node_name = "_";
            if (string.IsNullOrWhiteSpace(ConfigModel.node_name) == false)
            {
                node_name = ConfigModel.node_name;
            }
            sb.Append($"{node_name}#");
            sb.Append($"_#");
            sb.Append($"{Newtonsoft.Json.JsonConvert.SerializeObject(target)}#");
            bool is_warning = false;
            string msg = null;
            if (ConfigModel.rule_config != null && ConfigModel.rule_config.server_node != null)
            {
                if (ConfigModel.rule_config.server_node.cpu_rate > 0 && ConfigModel.rule_config.server_node.cpu_rate <= target.cpu_rate)
                {
                    is_warning = true;
                    msg += $"  当前CPU使用率{target.cpu_rate}% 持续{ConfigModel.rule_config.server_node.time_out}秒超过预警值{ConfigModel.rule_config.server_node.cpu_rate}%";
                }
                if (ConfigModel.rule_config.server_node.disk_rate > 0 && ConfigModel.rule_config.server_node.disk_rate <= target.disk_rate)
                {
                    is_warning = true;
                    msg += $"  当前磁盘占用率{target.disk_rate}% 持续{ConfigModel.rule_config.server_node.time_out}秒超过预警值{ConfigModel.rule_config.server_node.disk_rate}%";
                }
                if (ConfigModel.rule_config.server_node.mem_rate > 0 && ConfigModel.rule_config.server_node.mem_rate <= target.mem_rate)
                {
                    is_warning = true;
                    msg += $"  当前内存使用率{target.mem_rate}% 持续{ConfigModel.rule_config.server_node.time_out}秒超过预警值{ConfigModel.rule_config.server_node.mem_rate}%";
                }
            }
            if (msg == null)
                msg = "_";
            sb.Append($"{ConfigModel.rule_config.notice}#");
            var now = DateTimeUtility.ConvertToTimeStamp(time);
            if (is_warning)
            {
                if (server_time <= 0)
                {
                    server_time = now;
                    is_warning = false;
                }
                else
                {
                    if (now - server_time >= ConfigModel.rule_config.server_node.time_out)
                    {
                        server_time = 0;
                    }
                    else
                    {
                        is_warning = false;
                    }
                }

            }
            else
            {
                server_time = 0;
            }

            if (is_warning)
            {
                sb.Append("1#");
            }
            else
            {
                sb.Append("0#");
            }

            sb.Append($"{msg}#");
            if (ConfigModel.rule_config.is_console)
                Console.WriteLine(sb.ToString());

            _log.Info(sb.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        public static void MessageTarget(DateTime time, MessageTargetModel target)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{time.ToString("yyyy-MM-dd HH:mm:ss.ffff")}#");
            sb.Append("target_message_node#");
            sb.Append("kafka#");
            sb.Append($"_#");
            sb.Append($"{Newtonsoft.Json.JsonConvert.SerializeObject(target)}#");
            bool is_warning = false;
            string msg = null;
            if (ConfigModel.rule_config != null && ConfigModel.rule_config.message_node != null)
            {
                if (ConfigModel.rule_config.message_node.task_count > 0 && ConfigModel.rule_config.message_node.task_count <= target.task_count)
                {
                    is_warning = true;
                    msg = $"  当前任务堆积数{target.task_count} 持续{ConfigModel.rule_config.message_node.time_out}秒已超过预警值{ConfigModel.rule_config.message_node.task_count}";
                }
            }
            if (msg == null)
                msg = "_";
            sb.Append($"{ConfigModel.rule_config.notice}#");

            var now = DateTimeUtility.ConvertToTimeStamp(time);
            if (is_warning)
            {
                if (message_time <= 0)
                {
                    message_time = now;
                    is_warning = false;
                }
                else
                {
                    if (now - message_time >= ConfigModel.rule_config.message_node.time_out)
                    {
                        message_time = 0;
                    }
                    else
                    {
                        is_warning = false;
                    }
                }

            }
            else
            {
                message_time = 0;
            }

            if (is_warning)
            {
                sb.Append("1#");
            }
            else
            {
                sb.Append("0#");
            }

            sb.Append($"{msg}#");
            if (ConfigModel.rule_config.is_console)
                Console.WriteLine(sb.ToString());
            _log.Info(sb.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        public static void RedisTarget(DateTime time, RedisTargetModel target)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{time.ToString("yyyy-MM-dd HH:mm:ss.ffff")}#");
            sb.Append("target_redis_node#");
            string node_name = "_";
            if (string.IsNullOrWhiteSpace(target.redis_name) == false)
            {
                node_name = target.redis_name;
            }
            sb.Append($"{node_name}#");
            sb.Append($"_#");
            sb.Append($"{Newtonsoft.Json.JsonConvert.SerializeObject(target)}#");
            bool is_warning = false;
            string msg = null;
            if (ConfigModel.rule_config != null && ConfigModel.rule_config.server_node != null)
            {
                if (ConfigModel.rule_config.redis_node.mem_rate > 0 && ConfigModel.rule_config.redis_node.mem_rate <= target.mem_rate)
                {
                    is_warning = true;
                    msg += $"  当前Redis内存使用占比{target.mem_rate}% 持续{ConfigModel.rule_config.redis_node.time_out}秒已超过预警值{ConfigModel.rule_config.redis_node.mem_rate}%";
                }
            }
            if (msg == null)
                msg = "_";
            sb.Append($"{ConfigModel.rule_config.notice}#");

            var now = DateTimeUtility.ConvertToTimeStamp(time);
            if (is_warning)
            {
                if (redis_time <= 0)
                {
                    redis_time = now;
                    is_warning = false;
                }
                else
                {
                    if (now - redis_time >= ConfigModel.rule_config.redis_node.time_out)
                    {
                        redis_time = 0;
                    }
                    else
                    {
                        is_warning = false;
                    }
                }

            }
            else
            {
                redis_time = 0;
            }

            if (is_warning)
            {
                sb.Append("1#");
            }
            else
            {
                sb.Append("0#");
            }

            sb.Append($"{msg}#");
            if (ConfigModel.rule_config.is_console)
                Console.WriteLine(sb.ToString());

            _log.Info(sb.ToString());
        }


        /// <summary>
        /// 
        /// </summary>
        public static void PostgresTarget(DateTime time, PostgresTargetModel target)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{time.ToString("yyyy-MM-dd HH:mm:ss.ffff")}#");
            sb.Append("target_postgres_node#");
            sb.Append($"{target.pg_name}#");
            sb.Append($"{target.database}#");
            sb.Append($"{Newtonsoft.Json.JsonConvert.SerializeObject(target)}#");
            string msg = "-";
            string is_warning = "0";
            sb.Append($"{ConfigModel.rule_config.notice}#");

            foreach (var info in target.infos)
            {
                long tmp = 0;
                if (pg_total_site.ContainsKey($"{target.pg_name}{target.database}{info.table_name}"))
                {
                    tmp = pg_total_site[$"{target.pg_name}{target.database}{info.table_name}"];
                    pg_total_site[$"{target.pg_name}{target.database}{info.table_name}"] = info.total_size;
                }
                else
                {
                    pg_total_site.Add($"{target.pg_name}{target.database}{info.table_name}", info.total_size);
                    tmp = info.total_size;
                }

                var w1 = info.total_size - tmp;
                var w2 = UtilHelper.UnitsChange(ConfigModel.rule_config.pg_node.total_size + "mb");
                if (w1 >= w2)
                {
                    is_warning = "1";
                    msg += $"库：{target.database} 表：{info.table_name} 24小时内数据空间增加{Math.Round(w1 / 1024.00 / 1024.00, 2)}GB 触发预警";
                }
            }
            sb.Append($"{is_warning}#");
            sb.Append($"{msg}#");
            if (ConfigModel.rule_config.is_console)
                Console.WriteLine(sb.ToString());
            _log.Info(sb.ToString());
        }
    }
}
