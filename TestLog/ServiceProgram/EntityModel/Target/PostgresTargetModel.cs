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
        public string pg_name { get; set; }
        /// <summary>
        /// 库中详细信息
        /// </summary>
        public IEnumerable<PgTableModel> tables_info { get; set; }
    }
    
}
