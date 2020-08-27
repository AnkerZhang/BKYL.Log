using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceProgram.EntityModel.RuleTarget
{
    /// <summary>
    /// 
    /// </summary>
    public class RedisTargetConfig
    {
        /// <summary>
        /// 已使用内存占比大于某值预警
        /// </summary>
        public double mem_rate { get; set; }
        /// <summary>
        /// 大于指标值持续N秒预警
        /// </summary>
        public int time_out { get; set; }
    }
}
