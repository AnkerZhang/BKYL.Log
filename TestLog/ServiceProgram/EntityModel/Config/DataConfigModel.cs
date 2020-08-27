using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceProgram.EntityModel
{
    /// <summary>
    /// 数据库连接
    /// </summary>
    public class DataConfigModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string pg_name { get; set; }
        /// <summary>
        /// 端口
        /// </summary>
        public int port { get; set; }
        /// <summary>
        /// 数据包名称
        /// </summary>
        public string database { get; set; }
        /// <summary>
        /// 数据库ip
        /// </summary>
        public string host { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string user_id { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string pwd { get; set; }

        /// <summary>
        /// 数据路径
        /// </summary>
        public string data_path { get; set; }
        /// <summary>
        /// 日志路径
        /// </summary>
        public string log_path { get; set; }
    }
}
