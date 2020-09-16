using Quartz;
using ServiceProgram.JobServer.BaseJob;
using ServiceProgram.JobServer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProgram.JobServer
{
    /// <summary>
    /// 
    /// </summary>
    public class InitServiceJob
    {
        public async Task<string> Init()
        {
            var assembly = GetType().Assembly;
            var jobType = assembly.GetTypes().Where(a => a.BaseType == typeof(JobBase));
            var baseJobs = from a in jobType
                           select Activator.CreateInstance(a) as JobBase;


            foreach (var baseJob in baseJobs)
            {
                string job_cron = baseJob.Cron;
                string job_name = baseJob.JobName;

                try
                {

                    #region 初始化数据

                    #endregion 初始化数据

                    var current_jobdetail = await JMStdSchedulerFactory.GetIScheduler().GetJobDetail(new Quartz.JobKey(job_name));
                    if (current_jobdetail == null)
                    {
                        var current_assembly_job = baseJobs.Where(a => a.JobName == job_name).FirstOrDefault();
                        if (current_assembly_job == null)
                        {
                            continue;
                        }

                        //jobDetail初始化

                        var jobDetail = JobBuilder.Create(current_assembly_job.GetType())
                                     .WithIdentity(job_name)
                                     .Build();

                        #region 触发器初始化

                        ITrigger jobTrigger = null;

                        //按照cron表达式去进行初始化创建
                        jobTrigger = TriggerBuilder.Create()
                                                    .WithIdentity(job_name + "Trigger")
                                                    .StartNow()
                                                    .WithCronSchedule(job_cron, (zw) => { zw.WithMisfireHandlingInstructionDoNothing(); })
                                                    .Build();

                        //将jobDetail跟JobTigger同时保存到本地JobStore中
                        var job_scheduler = await JMStdSchedulerFactory.GetIScheduler().ScheduleJob(jobDetail, jobTrigger, current_assembly_job.CancellationSource.Token);
                        Console.WriteLine($"任务：{job_name} 同步完成");

                        #endregion 触发器初始化
                    }
                    else
                    {
                            var delete_result = await JMStdSchedulerFactory.GetIScheduler().DeleteJob(new Quartz.JobKey(job_name));
                            if (delete_result == false)
                            {
                                Console.WriteLine("本地作业同步", $"【已删除】机器： 删除本地任务：{job_name} 失败");
                            }
                            else
                            {
                                Console.WriteLine("本地作业同步", $"【已删除】机器： 删除本地任务：{job_name} 成功");
                            }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("任务：" + job_name + ex.ToString());
                }
            }

            return null;
        }
    }
}
