using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceProgram.EntityModel.RuleTarget
{
    /// <summary>
    /// 
    /// </summary>
    public class PgTargetConfig
    {
        /// <summary>
        /// 一天 24小时单库增N MB大小进行预警   ---单位MB
        /// </summary>
        public double total_size { get; set; }
    }
}
