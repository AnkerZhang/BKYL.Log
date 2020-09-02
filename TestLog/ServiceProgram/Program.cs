using System;
using System.Collections.Generic;
using System.Linq;
using ServiceProgram.EntityModel;
using Microsoft.Extensions.Configuration;
using ServiceProgram.Common;
using ServiceProgram.JobServer;
using ServiceProgram.EntityModel.RuleTarget;

namespace ServiceProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            //加载配置文件
            UtilHelper.log = BKYL.Log.LogExtension.GetGlobalLog(BKYL.Log.LogFactory.LogEnum.console);
            Console.WriteLine("==========================开始加载配置文件========================");
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");
            var configuration = builder.Build();
            ConfigModel.action = configuration["action"].ToString();
            if (string.IsNullOrWhiteSpace(ConfigModel.action) == false)
            {
                var arr = ConfigModel.action.Split(",");
                if (arr.Any(a => a == "server"))
                {
                    ConfigModel.node_name = configuration["nodel_name"].ToString();
                }
                if (arr.Any(a => a == "message"))
                {
                    var msg_config = configuration.GetSection("msg_config").Get<MessageConfigModel>();
                    ConfigModel.msg_config = msg_config;
                }
                if (arr.Any(a => a == "redis"))
                {
                    var redis_configs = configuration.GetSection("redis_config").Get<List<RedisConfigModel>>();
                    ConfigModel.redis_configs = redis_configs;
                }
                if (arr.Any(a => a == "pg"))
                {
                    var pg_config = configuration.GetSection("pg_config").Get<List<DataConfigModel>>();
                    ConfigModel.data_configs = pg_config;
                }
            }
            var rule_config = configuration.GetSection("rule_config").Get<RuleConfigModel>();
            ConfigModel.rule_config = rule_config;
            InitServiceJob job = new InitServiceJob();
            _ = job.Init().Result;
            Console.ReadLine();

        }
    }
}
