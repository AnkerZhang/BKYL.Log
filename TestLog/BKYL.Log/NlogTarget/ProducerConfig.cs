using NLog.Config;
using System;
using System.Collections.Generic;
using System.Text;

namespace BKYL.Log.NlogTarget
{
    /// <summary>
    /// 
    /// </summary>
    [NLogConfigurationItem]
    public class ProducerConfig
    {
        /// <summary>
        /// 
        /// </summary>
        [RequiredParameter]
        public string Key { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [RequiredParameter]
        public string value { set; get; }
    }
}
