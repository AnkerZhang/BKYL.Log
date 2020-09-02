using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceProgram.EntityModel
{
    /// <summary>
    ///  
    /// </summary>
    public class PgTableModel
    {
        /// <summary>
        /// 表名称
        /// </summary>
        public string table_name { get; set; }
        /// <summary>
        /// 块数
        /// </summary>
        public int num_chunks { get; set; }
        /// <summary>
        /// 超表使用的磁盘空间 单位KB
        /// </summary>
        public string table_size { get; set; }
        /// <summary>
        /// 索引使用的磁盘空间 单位KB
        /// </summary>
        public string index_size { get; set; }
        /// <summary>
        /// 烤面包表的磁盘空间  单位KB
        /// </summary>
        public string toast_size { get; set; }
        /// <summary>
        /// 指定表使用的总磁盘空间，包括所有索引和TOAST数据  单位KB
        /// </summary>
        public string total_size { get; set; }
        /// <summary>
        /// 获取所有超表的近似行数
        /// </summary>
        public long row_estimate { get; set; }
    }
}
