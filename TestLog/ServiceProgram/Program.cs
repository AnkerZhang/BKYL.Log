using System;
using System.Collections.Generic;
using System.Linq;
using ServiceProgram.EntityModel;
using Microsoft.Extensions.Configuration;
using ServiceProgram.Common;
using ServiceProgram.JobServer;
using ServiceProgram.EntityModel.RuleTarget;
using System.Runtime.InteropServices;
using System.Threading;
using Npgsql;
using Dapper;
using ServiceProgram.EntityModel.Config;
using ServiceProgram.EntityModel.Tables;

namespace ServiceProgram
{
    class Program
    {
        public static DataAppConfig appConfig;
        static void Main(string[] args)
        {

            var worker = ServerTargetHelper.LoadInformation();
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(worker));
            //加载配置文件
            UtilHelper.log = BKYL.Log.LogExtension.GetGlobalLog(BKYL.Log.LogFactory.LogEnum.console);
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");
            var configuration = builder.Build();
            appConfig = configuration.GetSection("data_config").Get<DataAppConfig>();
            ServerTargetHelper.SaveSerWorker();

            Console.WriteLine("==========================开始加载配置文件========================");
            ServerTargetHelper.DataConfig();
            Console.WriteLine("==========================配置加载完成========================");
            InitServiceJob job = new InitServiceJob();
            Console.WriteLine("==========================Job加载========================");
            _ = job.Init().Result;

            while (true)
            {
                Thread.Sleep(1000 * 60 * 60 * 24);
            }

        }
    }
}
