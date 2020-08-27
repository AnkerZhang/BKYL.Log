using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceProgram.EntityModel.RuleTarget
{
    /// <summary>
    /// 服务器cpu使用率大于数值预警或的关系
    /// </summary>
    public class ServerTargetConfig
    {
        /// <summary>
        /// Cpu使用率
        /// </summary>
        public double cpu_rate { get; set; }
        /// <summary>
        /// 内存使用率
        /// </summary>
        public double mem_rate { get; set; }
        /// <summary>
        /// 磁盘占用使用率
        /// </summary>
        public double driver_rate { get; set; }
        /// <summary>
        /// 大于指标值持续N秒预警
        /// </summary>
        public int time_out { get; set; }
    }
}
