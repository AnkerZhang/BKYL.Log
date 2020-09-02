using ServiceProgram.EntityModel.RuleTarget;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceProgram.EntityModel
{
    /// <summary>
    /// 配置文件
    /// </summary>
    public static class ConfigModel
    {
        /// <summary>
        /// action
        /// </summary>
        public static string action { get; set; }  
        /// <summary>
        /// 服务器名称
        /// </summary>
        public static string node_name { get; set; }
        /// <summary>
        /// 消息队列配置文件
        /// </summary>
        public static MessageConfigModel msg_config { get; set; }
        /// <summary>
        /// redis配置文件
        /// </summary>
        public static List<RedisConfigModel> redis_configs { get; set; }
        /// <summary>
        /// pg数据库配置文件
        /// </summary>
        public static List<DataConfigModel> data_configs { get; set; }
        public static RuleConfigModel rule_config { get; set; }
    }
}
