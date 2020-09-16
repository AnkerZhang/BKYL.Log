using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceProgram.EntityModel.Config
{
    /// <summary>
    /// 读取配置文件
    /// </summary>
    public class DataAppConfig
    {
        public string host { get; set; }
        public int port { get; set; }
        public string database { get; set; }
        public string user_id { get; set; }
        public string pwd { get; set; }
    }
}
