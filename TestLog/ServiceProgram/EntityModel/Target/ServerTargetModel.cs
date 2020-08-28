using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceProgram.EntityModel.Target
{
    public class ServerTargetModel
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
        public double disk_rate { get; set; }
    }
    
}
