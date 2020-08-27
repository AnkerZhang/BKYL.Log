using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceProgram.EntityModel
{
    /// <summary>
    /// 消息队列
    /// </summary>
    public class MessageConfigModel: DataConfigModel
    {
        /// <summary>
        /// 任务堆积表明 默认bas_wait_handle_desc
        /// </summary>
        public string table_name { get; set; } = "bas_wait_handle_desc";

    }
}
