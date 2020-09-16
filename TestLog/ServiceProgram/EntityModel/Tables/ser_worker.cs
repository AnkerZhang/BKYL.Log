using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceProgram.EntityModel.Tables
{
    /// <summary>
    /// 服务端运行情况表
    /// </summary>
    public class ser_worker
    {
        /// <summary>
        /// 
        /// </summary>
        public string b_worker_id { get; set; }
        /// <summary>
        /// IP
        /// </summary>
        public string b_ip { get; set; }
        /// <summary>
        /// 待处理任务数
        /// </summary>
        public int b_wait_handle_count { get; set; }
        /// <summary>
        /// 状态更新时间
        /// </summary>
        public DateTime b_update_time { get; set; }
        /// <summary>
        /// 运行状态：0/未运行，1/正在运行
        /// </summary>
        public int b_is_runing { get; set; }
        /// <summary>
        /// 当前使用的内存数
        /// </summary>
        public double b_use_memory { get; set; }
        /// <summary>
        /// 当前使用的CPU百分比
        /// </summary>
        public double b_use_cpu { get; set; }
        /// <summary>
        /// 安装路径
        /// </summary>
        public string b_installation_path { get; set; }
        /// <summary>
        /// 状态：0/正常，1/停止，2/异常停止
        /// </summary>
        public int b_state { get; set; }
        /// <summary>
        /// 工作组ID
        /// </summary>
        public string b_group_id { get; set; }
        /// <summary>
        /// 工作者类型：0/接收端、1/处理端2采集端
        /// </summary>
        public int b_worker_type { get; set; }
    }
}
