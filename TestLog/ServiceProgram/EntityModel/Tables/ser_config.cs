using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceProgram.EntityModel.Tables
{
    /// <summary>
    /// 服务运行配置表
    /// </summary>
    public class ser_config
    {
        public int b_config_id { get; set; }
        public string b_worker_id { get; set; }
        /// <summary>
        /// 配置项
        /// </summary>
        public string b_key { get; set; }
        /// <summary>
        /// 配置项值
        /// </summary>
        public string b_value { get; set; }
        public DateTime b_create_time { get; set; }
        public DateTime b_update_time { get; set; }
    }
}
