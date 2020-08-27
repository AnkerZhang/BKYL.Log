using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceProgram.EntityModel.RuleTarget
{
    /// <summary>
    /// 
    /// </summary>
    public class MessageTargetConfig
    {
        /// <summary>
        /// 任务堆积数
        /// </summary>
        public double task_count { get; set; }
        /// <summary>
        /// 大于指标值持续N秒预警
        /// </summary>
        public int time_out { get; set; }
    }
}
