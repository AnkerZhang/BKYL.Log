using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceProgram.EntityModel.Target
{
    public class RedisTargetModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string redis_name { get; set; }
        /// <summary>
        ///  内存使用空间
        /// </summary>
        public decimal used_bytes_val { get; set; }
        /// <summary>
        /// 总的内存使用空间
        /// </summary>
        public decimal max_bytes_val { get; set; }
        /// <summary>
        /// 已使用内存占比
        /// </summary>
        public double mem_rate
        {
            get
            {
                return Math.Round(double.Parse(used_bytes_val.ToString()) / double.Parse(max_bytes_val.ToString()) * 100.00, 2);
            }
        }
        /// <summary>
        /// 客户端连接数量
        /// </summary>
        public long connected_clients_val { get; set; }
        /// <summary>
        /// key的数量
        /// </summary>
        public long redis_db_keys { get; set; }

    }

}
