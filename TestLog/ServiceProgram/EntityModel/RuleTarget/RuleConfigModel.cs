using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceProgram.EntityModel.RuleTarget
{
    public class RuleConfigModel
    {
        public ServerTargetConfig server_node { get; set; }
        public MessageTargetConfig message_node { get; set; }
        public RedisTargetConfig redis_node { get; set; }
        public string notice { get; set; }
        public bool is_console { get; set; }
    }
}
