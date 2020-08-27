using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BKYL.Log.LogFactory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebTestLog.Common;

namespace WebTestLog.Controllers
{
    /// <summary>
    /// 日志记录控制器
    /// 文件日志存放在 =》应用程序的根目录/logs/
    /// 日志集中收集到http://62.234.155.90:5601/
    /// </summary>
    [ApiController]
    [Route("api/log")]
    public class TestController : ControllerBase
    {
        private readonly ILog _log;

        public TestController() {
            _log = LogHelp.log;
        }


        /// <summary>
        /// 测试webLog日志记录
        /// </summary>
        /// <returns></returns>
        [HttpGet("web")]
        public string WebLogTest()
        {
            _log.Error("这个出错了", new Exception("我是一个错误信息"), true, true);
            return "ok";
        }
    }
}
