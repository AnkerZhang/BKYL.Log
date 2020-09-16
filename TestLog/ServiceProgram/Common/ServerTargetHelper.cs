using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using NLog;
using Npgsql;
using ServiceProgram.EntityModel;
using ServiceProgram.EntityModel.RuleTarget;
using ServiceProgram.EntityModel.Tables;
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
        private static string b_worker_id = "";

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
            string notice = "_";
            if (ConfigModel.rule_config != null)
            {
                notice = ConfigModel.rule_config.notice;
                if (ConfigModel.rule_config.server_node != null)
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
            }
            if (msg == null)
                msg = "_";
            sb.Append($"{notice}#");
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
            string notice = "_";
            if (ConfigModel.rule_config != null && ConfigModel.rule_config.message_node != null)
            {
                if (ConfigModel.rule_config.message_node.task_count > 0 && ConfigModel.rule_config.message_node.task_count <= target.task_count)
                {
                    is_warning = true;
                    msg = $"  当前任务堆积数{target.task_count} 持续{ConfigModel.rule_config.message_node.time_out}秒已超过预警值{ConfigModel.rule_config.message_node.task_count}";
                }
                notice = ConfigModel.rule_config.notice;
            }
            if (msg == null)
                msg = "_";
            sb.Append($"{notice}#");

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
            string notice = "_";
            if (ConfigModel.rule_config != null)
            {
                notice = ConfigModel.rule_config.notice;
                if (ConfigModel.rule_config.redis_node != null)
                {
                    if (ConfigModel.rule_config.redis_node.mem_rate > 0 && ConfigModel.rule_config.redis_node.mem_rate <= target.mem_rate && target.max_bytes_val > 0)
                    {
                        is_warning = true;
                        msg += $"  当前Redis内存使用占比{target.mem_rate}% 持续{ConfigModel.rule_config.redis_node.time_out}秒已超过预警值{ConfigModel.rule_config.redis_node.mem_rate}%";
                    }
                }
            }
            if (msg == null)
                msg = "_";
            sb.Append($"{notice}#");

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
            string notice = "_";
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
                if (ConfigModel.rule_config != null)
                {
                    notice = ConfigModel.rule_config.notice;
                    if (ConfigModel.rule_config.pg_node != null)
                    {
                        var w2 = UtilHelper.UnitsChange(ConfigModel.rule_config.pg_node.total_size + "mb");
                        if (w1 >= w2)
                        {
                            is_warning = "1";
                            msg += $"库：{target.database} 表：{info.table_name} 24小时内数据空间增加{Math.Round(w1 / 1024.00 / 1024.00, 2)}GB 触发预警";
                        }
                    }
                }
            }
            sb.Append($"{notice}#");
            sb.Append($"{is_warning}#");
            sb.Append($"{msg}#");
            Console.WriteLine(sb.ToString());
            _log.Info(sb.ToString());
        }

        public static ser_worker LoadInformation()
        {
            ser_worker worker = new ser_worker();
            try
            {
                string ip = IpAddressHelp.GetLocalIP();
                string path = System.AppDomain.CurrentDomain.BaseDirectory;
                float use_cpu = CommonUse.GetPercentCPU();
                float use_memory = CommonUse.GetUsedMemory();
                worker = new ser_worker()
                {
                    b_worker_id = string.IsNullOrWhiteSpace(b_worker_id) ? Guid.NewGuid().ToString("N") : b_worker_id,
                    b_ip = ip,
                    b_wait_handle_count = 0,
                    b_update_time = DateTime.Now,
                    b_is_runing = 1,
                    b_use_memory = float.Parse((use_memory / 1024 / 1024).ToString("0.00")),
                    b_use_cpu = use_cpu,
                    b_installation_path = path,
                    b_state = 1,
                    b_worker_type = 2
                };
            }
            catch
            {

            }
            return worker;
        }

        public static void SaveSerWorker()
        {
            try
            {
                using (var conn = new NpgsqlConnection($"PORT={Program.appConfig.port};DATABASE={Program.appConfig.database};HOST={Program.appConfig.host};PASSWORD={Program.appConfig.pwd};USER ID={Program.appConfig.user_id}"))
                {
                    conn.Open();
                    var worker = LoadInformation();
                    if (string.IsNullOrWhiteSpace(b_worker_id))
                    {
                        var ser_worker = conn.QueryAsync<ser_worker>($"select * from ser_worker where b_ip='{worker.b_ip}' and b_installation_path='{worker.b_installation_path}'").Result.FirstOrDefault();
                        if (ser_worker == null)
                        {
                            ser_worker = worker;
                            var ex = conn.ExecuteAsync($"INSERT INTO ser_worker(b_worker_id,b_ip,b_wait_handle_count,b_update_time,b_is_runing,b_use_memory,b_use_cpu,b_installation_path,b_state,b_group_id,b_worker_type) " +
                               $"VALUES ('{ser_worker.b_worker_id}', '{ser_worker.b_ip}',0, '{ser_worker.b_update_time.ToString("yyyy-MM-dd HH:mm:ss")}', '{ser_worker.b_is_runing}', '{ser_worker.b_use_memory}', '{ser_worker.b_use_cpu}', '{ser_worker.b_installation_path}','{ser_worker.b_state}', NULL, '{ser_worker.b_worker_type}');").Result;

                        }
                        else
                        {
                            //update
                            var ex = conn.ExecuteAsync($"UPDATE ser_worker SET b_update_time = '{worker.b_update_time.ToString("yyyy-MM-dd HH:mm:ss")}', b_is_runing = '1', b_use_memory = '{worker.b_use_memory}', b_use_cpu = '{worker.b_use_cpu}',b_installation_path = '{worker.b_installation_path}', b_state = '0',b_group_id = NULL,b_worker_type = '{worker.b_worker_type}' WHERE b_worker_id = '{ ser_worker.b_worker_id}';").Result;
                        }
                        b_worker_id = ser_worker.b_worker_id;
                    }
                    else
                    {
                        //update
                        var ex = conn.ExecuteAsync($"UPDATE ser_worker SET b_update_time = '{worker.b_update_time.ToString("yyyy-MM-dd HH:mm:ss")}', b_is_runing = '1', b_use_memory = '{worker.b_use_memory}', b_use_cpu = '{worker.b_use_cpu}',b_installation_path = '{worker.b_installation_path}', b_state = '0',b_group_id = NULL,b_worker_type = '{worker.b_worker_type}' WHERE b_worker_id = '{b_worker_id}';").Result;
                    }
                    conn.Close();
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public static void RuleConfig()
        {
            RuleConfig rule_config = null;
            if (rule_config != null)
            {
                ConfigModel.rule_config = new RuleConfigModel
                {
                    notice = rule_config.notice,
                    message_node = new MessageTargetConfig
                    {
                        task_count = rule_config.message_task_count,
                        time_out = rule_config.redis_time_out
                    },
                    pg_node = new PgTargetConfig
                    {
                        total_size = rule_config.pg_total_size
                    },
                    redis_node = new RedisTargetConfig
                    {
                        mem_rate = rule_config.redis_mem_rate,
                        time_out = rule_config.redis_time_out
                    },
                    server_node = new ServerTargetConfig
                    {
                        time_out = rule_config.server_time_out,
                        mem_rate = rule_config.server_mem_rate,
                        cpu_rate = rule_config.server_cpu_rate,
                        disk_rate = rule_config.server_disk_rate
                    }
                };

            }
        }

        public static void DataConfig()
        {
            using (var conn = new NpgsqlConnection($"PORT={Program.appConfig.port};DATABASE={Program.appConfig.database};HOST={Program.appConfig.host};PASSWORD={Program.appConfig.pwd};USER ID={Program.appConfig.user_id}"))
            {
                conn.Open();
                var ser_configs = conn.QueryAsync<ser_config>($"select * from ser_config where b_worker_id='{b_worker_id}'").Result;
                conn.Close();

                var node = ser_configs.Where(w => w.b_key == "node_name").FirstOrDefault();
                ConfigModel.node_name = node?.b_value;

                var msg_config = ser_configs.Where(w => w.b_key == "msg_config").FirstOrDefault();
                if (msg_config != null)
                {
                    ConfigModel.msg_config = Newtonsoft.Json.JsonConvert.DeserializeObject<MessageConfigModel>(msg_config.b_value);
                }
                else
                {
                    ConfigModel.msg_config = null;
                }
                var redis_config = ser_configs.Where(w => w.b_key == "redis_config").FirstOrDefault();
                if (redis_config != null)
                {
                    ConfigModel.redis_configs = Newtonsoft.Json.JsonConvert.DeserializeObject<List<RedisConfigModel>>(redis_config.b_value);
                }
                else
                {
                    ConfigModel.redis_configs = null;
                }
                var pg_config = ser_configs.Where(w => w.b_key == "pg_config").FirstOrDefault();
                if (pg_config != null)
                {
                    ConfigModel.data_configs = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DataConfigModel>>(pg_config.b_value);
                }
                else
                {
                    ConfigModel.data_configs = null;
                }
                if (ConfigModel.rule_config == null)
                {
                    ConfigModel.rule_config = new RuleConfigModel();
                }
                var notice_config = ser_configs.Where(w => w.b_key == "notice").FirstOrDefault();
                if (notice_config != null)
                {
                    ConfigModel.rule_config.notice = notice_config.b_value;
                }
                else
                {
                    ConfigModel.rule_config.notice = null;
                }
                var server_config = ser_configs.Where(w => w.b_key == "server_node").FirstOrDefault();
                if (server_config != null)
                {
                    ConfigModel.rule_config.server_node = Newtonsoft.Json.JsonConvert.DeserializeObject<ServerTargetConfig>(server_config.b_value);
                }
                else
                {
                    ConfigModel.rule_config.server_node = null;
                }
                var redis_rule_config = ser_configs.Where(w => w.b_key == "redis_node").FirstOrDefault();
                if (redis_rule_config != null)
                {
                    ConfigModel.rule_config.redis_node = Newtonsoft.Json.JsonConvert.DeserializeObject<RedisTargetConfig>(redis_rule_config.b_value);
                }
                else
                {
                    ConfigModel.rule_config.redis_node = null;
                }
                var pg_rule_config = ser_configs.Where(w => w.b_key == "pg_node").FirstOrDefault();
                if (pg_rule_config != null)
                {
                    ConfigModel.rule_config.pg_node = Newtonsoft.Json.JsonConvert.DeserializeObject<PgTargetConfig>(pg_rule_config.b_value);
                }
                else
                {
                    ConfigModel.rule_config.pg_node = null;
                }
                var message_rule_config = ser_configs.Where(w => w.b_key == "message_node").FirstOrDefault();
                if (message_rule_config != null)
                {
                    ConfigModel.rule_config.message_node = Newtonsoft.Json.JsonConvert.DeserializeObject<MessageTargetConfig>(message_rule_config.b_value);
                }
                else
                {
                    ConfigModel.rule_config.message_node = null;
                }
            }
        }
    }
}
