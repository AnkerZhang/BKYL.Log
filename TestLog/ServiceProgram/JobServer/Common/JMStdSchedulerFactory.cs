using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace ServiceProgram.JobServer.Common
{
    /// <summary>
    ///
    /// </summary>
    public class JMStdSchedulerFactory
    {
        private static IScheduler _IScheduler;

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public static IScheduler GetIScheduler()
        {
            if (_IScheduler != null)
            {
                return _IScheduler;
            }

            IScheduler _scheduler;

            var schedulerFactory = new StdSchedulerFactory(GetProperties());
            _scheduler = schedulerFactory.GetScheduler().Result;
            _scheduler.Start().Wait();

            _IScheduler = _scheduler;

            return _IScheduler;
        }

        /// <summary>
        /// 拿督公共的properties配置
        /// </summary>
        /// <returns></returns>
        public static NameValueCollection GetProperties()
        {
            NameValueCollection properties = new NameValueCollection
            {
                //["quartz.scheduler.instanceName"] = "TestScheduler",
                //["quartz.scheduler.instanceId"] = "instance_one",
                ["quartz.threadPool.type"] = "Quartz.Simpl.SimpleThreadPool, Quartz",
                ["quartz.threadPool.threadCount"] = "100",
                ["quartz.jobStore.misfireThreshold"] = "5000",
                ["quartz.threadPool.threadPriority"] = "Normal"
                //["quartz.jobStore.type"] = "Quartz.Impl.AdoJobStore.JobStoreTX, Quartz",
                //["quartz.jobStore.useProperties"] = "false",
                //["quartz.jobStore.dataSource"] = "default",
                //["quartz.jobStore.tablePrefix"] = "QRTZ_",
                //["quartz.jobStore.clustered"] = "true",
                //["quartz.jobStore.driverDelegateType"] = "Quartz.Impl.AdoJobStore.SqlServerDelegate, Quartz",
                //["quartz.dataSource.default.connectionString"] = TestConstants.SqlServerConnectionString,
                //["quartz.dataSource.default.provider"] = TestConstants.DefaultSqlServerProvider,
                //["quartz.serializer.type"] = "json"
            };
            return properties;
        }
    }
}
