using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceProgram.EntityModel.RuleTarget
{
    /// <summary>
    /// 
    /// </summary>
    public class RuleConfig
    {
        /// <summary>
        /// 预警邮箱
        /// </summary>
        public string notice { get; set; }
        /// <summary>
        /// 服务器cpu使用率
        /// </summary>
        public double server_cpu_rate { get; set; }
        /// <summary>
        /// 服务器内存使用率
        /// </summary>
        public double server_mem_rate { get; set; }
        /// <summary>
        /// 服务器磁盘占用率
        /// </summary>
        public double server_disk_rate { get; set; }
        /// <summary>
        /// 大于指标值持续N秒预警
        /// </summary>
        public int server_time_out { get; set; }


        /// <summary>
        /// 消息队列任务堆积数大于某值预警
        /// </summary>
        public int message_task_count { get; set; }
        /// <summary>
        /// 大于指标值持续N秒预警
        /// </summary>
        public int message_time_out { get; set; }

        /// <summary>
        /// 已使用内存占比大于某值预警
        /// </summary>
        public double redis_mem_rate { get; set; }
        /// <summary>
        /// 大于指标值持续N秒预警
        /// </summary>
        public int redis_time_out { get; set; }

        /// <summary>
        /// 一天 24小时单库增N MB大小进行预警   ---单位MB
        /// </summary>
        public int pg_total_size { get; set; }
    }
}
