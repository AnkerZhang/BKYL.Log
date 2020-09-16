using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceProgram.EntityModel.RuleTarget
{
    public class RuleConfigModel
    {
        public ServerTargetConfig server_node { get; set; }
        public MessageTargetConfig message_node { get; set; }
        public RedisTargetConfig redis_node { get; set; }
        public PgTargetConfig pg_node { get; set; }
        public string notice { get; set; }

    }

    public class RuleDataConfigModel
    {
        /// <summary>
        /// 数据库ip
        /// </summary>
        public string host { get; set; }
        /// <summary>
        /// 端口
        /// </summary>
        public int port { get; set; }
        /// <summary>
        /// 数据包名称 英文逗号分隔
        /// </summary>
        public string database { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string user_id { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string pwd { get; set; }
        /// <summary>
        /// 表名
        /// </summary>
        public string table_name { get; set; }


    }
}
