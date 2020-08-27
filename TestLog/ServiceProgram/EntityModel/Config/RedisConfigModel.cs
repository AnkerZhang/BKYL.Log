using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceProgram.EntityModel
{
    /// <summary>
    /// Redis指标配置
    /// </summary>
    public class RedisConfigModel
    {
        /// <summary>
        /// 节点名称
        /// </summary>
        public string node_name { get; set; }
        /// <summary>
        /// Redis Exporter指标
        /// </summary>
        public string url { get; set; }
    }
}
