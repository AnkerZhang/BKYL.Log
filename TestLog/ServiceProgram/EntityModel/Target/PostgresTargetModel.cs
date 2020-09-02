using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceProgram.EntityModel.Target
{
    public class PostgresTargetModel
    {
        /// <summary>
        /// pg名称
        /// </summary>
        public string pg_name
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_pg_name))
                {
                    return "-";
                }
                return _pg_name;
            }
            set { _pg_name = value; }
        }
        private string _pg_name { get; set; }
        /// <summary>
        /// 库名
        /// </summary>
        public string database
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_database))
                {
                    return "-";
                }
                return _database;
            }
            set
            {
                _database = value;
            }
        }
        private string _database { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<PostgresTargetInfo> infos { get; set; }

    }


    public class PostgresTargetInfo {
        /// <summary>
        /// 
        /// </summary>
        public string table_name { get; set; }
        /// <summary>
        /// 块数
        /// </summary>
        public int num_chunks { get; set; }
        /// <summary>
        /// 超表使用的磁盘空间 单位KB
        /// </summary>
        public long table_size { get; set; }
        /// <summary>
        /// 索引使用的磁盘空间 单位KB
        /// </summary>
        public long index_size { get; set; }
        /// <summary>
        /// 烤面包表的磁盘空间  单位KB
        /// </summary>
        public long toast_size { get; set; }
        /// <summary>
        /// 指定表使用的总磁盘空间，包括所有索引和TOAST数据  单位KB
        /// </summary>
        public long total_size { get; set; }
        /// <summary>
        /// 获取所有超表的近似行数
        /// </summary>
        public long row_estimate { get; set; }
    }
}
