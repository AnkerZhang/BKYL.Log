# BKYL.Log
日志统一格式采集管理设计说明书












版    本：V1.0
密    级：□绝密□机密□秘密■普通
发布日期：2020年8月26日



目录
1. 前言	1
1.1 编写目的	1
1.2 日志统一格式采集管理开发背景	1
2. 组件介绍	1
2.1 BKYL.Log介绍	1
2.2 环境要求	1
3. 日志格式	1
3.1 Web项目日志格式	2
3.2控制台项目日志格式	2
3.3注意事项	2
4. 配置文件介绍	12
4.1 NLog介绍	12
4.2 NLog.confg文件介绍	12
4.3 头部配置文件模块	16
4.4 本地日志存储配置模块	17
4.5 日志中心收集配置模块	19
5. 日志组件使用方法	21
5.1 BKYL.Log组件介绍	21
5.2 ILog接口对象介绍	21
5.3 Web项目使用方式	21
5.4 控制台项目使用方式	22
5.5 Log日志对象最佳使用方式	22
5.6 本地日志存储	23
5.7 日志中心存储	24
5.8 日志中心查看存储日志方式	24
6. 服务器指标收集	24
6.1服务器指标	24
6.2 Redis指标	24
6.3 Kafaka指标	24
6.4 Postgres指标	24
7. 收集日志服务端拓展	24
7.1日志中心介绍	24
7.2 ElasticSearch介绍	25
7.3 Kibana使用介绍	26
7.4 Logstash使用介绍	27
8. 意见和反馈	28
8.1源码解析与反馈	27



1.前言
1.1 编写目的
本文详细阐述了日志格式规范和日志统一收集设计，为开发人员、实施人员、测试人员发现问题快速排查和出现问并题预警及相关人员处理，使其能够快速的非侵入式嵌入，做到开箱即用组件。
1.2日志统一格式采集管理开发背景
随着技术的成熟，应用开发方式也在不断变化。开发各类功能的专用程序段已经成为主要趋势。
在微服务架构中，每个服务运行在独立的进程中，服务与服务间通过轻量级的通信机制（一般是基于HTTP协议的API）互相协作。API主要依赖于ESB、基于文件系统的资源、云解决方案及保留系统的集成。、可维护性差。管理员无法方便的查看系统运行状态和运行日志。在系统出现故障时，没有快速恢复的方法。
根据“微服务”设计思路，大量底层服务能够通过tcp将日志发送到统一地方存储。
不同组件在提供应用服务的过程中都能产生日志，但日志格式和结构各不相同，因此需要建立统一日志管理机制。
出现故障后，开发人员可根据统一格式的日志文件查找问题，不需要在不同系统之间来回切换。
2.组件介绍
主要为了统一日志格式和日志统一采集，针对日志当下需求开发了一套日志组件BKYL.Log，组件已经发布到Nuget中，只需要项目中引用BKYL.Log即可。

2.1 BKYL.Log组件介绍
本组件目标框架使用.NET Standard 2.0类库，类库中引用了 Nlog.(4.7.4)和Nlog.Web.AspNetCore(4.9.3)包。
同时BKYL.Log同样集成了Nlog的常用方法，在.NET Standard 2.0框架下还兼容了Dotnet Core 项目实现日志阔平台记录。
2.2开发环境
组件是由C#语言编写，只能由C#项目引用使用，目前支持.Net Framework3.1以上版本和dotnet core版本，控制台程序与Web程序同兼容，这样做到统一日志组件。
3.日志格式
Web日志与控制台日志最大区别是，web项目有相关Http协议相关参数
日志参数通过#号分隔，日志记录注意事项查看3.3注意事项
3.1 Web项目日志格式
格式如下：
时间#项目名称#环境变量#服务器名称#进程id#日志等级#记录器#请求url#控制器#请求动作#http请求类型#客户端ip#请求body#请求quwey#http状态码#记录的消息#堆栈报错信息#
例如：
2020-08-06 11:17:52.8686#服务名称web_test_log#计算机主机名称Host#进程10001#环境变量dev#日志级别INFO#记录器Bkyl.Log.LogGateway.Imp.WebGateway#预警邮箱793087382@qq.com#请求URLhttp://localhost/api/log/web#控制器Test#方法WebLogTest#Http请求类型GET#Http请求Ip39.96.160.43#Http请求Body{a:"a"}#请求Querya=a#a=a#响应状态码200#日志信息我是info级别日志#0#报错的堆栈信息#


3.2 控制台项目日志格式

格式如下：
时间#项目名称#服务器名称#进程id#环境变量#日志等级#记录器#预警邮箱#记录的消息#是否预警#堆栈报错信息#
例如：
2020-08-06 11:17:52.8686#服务名称web_test_log#计算机主机名称Host#进程10001#环境变量dev#日志级别INFO#记录器Bkyl.Log.LogGateway.Imp.WebGateway#预警邮箱793087382@qq.com#日志信息我是info级别日志#0#报错的堆栈信息#
3.3 注意事项
关于日志格式注意事项：
1.记录日志内容不能包含“#”符号
2.记录日志中不能有换行 /r/n  日志会自动过滤掉换行






4.配置文件介绍
4.1 NLog介绍
NLog是一个基于.NET平台编写的类库，我们可以使用NLog在应用程序中添加极为完善的跟踪调试代码。
NLog是一个简单灵活的.NET日志记录类库。通过使用NLog，我们可以在任何一种.NET语言中输出带有上下文的（contextual information）调试诊断信息，根据喜好配置其表现样式之后发送到一个或多个输出目标（target）中。
NLog的API非常类似于log4net，且配置方式非常简单。NLog使用路由表（routing table）进行配置，这样就让NLog的配置文件非常容易阅读，并便于今后维护。
4.2 NLog.Config文件介绍
使用BKYL.Log组件需要创建一个nlog.config文件，是由xml格式配置，配置了日志格式和日志输出方式，下面将介绍nlog.config文件节点用途和说明。
4.3 头部配置文件模块
<variable name=”key” value=”vlue”> 	为配置变量赋值
 节点属于自定义变量key value形式，可以定义多个，但Key不能重复，当使用key对应的value时 ${key} 
在这里我使用variable当作全局变量使用，后面整个配置文件只需要根据程序的类型修改variable节点即可

4.4 头部配置文件模块
<targets async="true"> </targets>		声明目标  syync代表异步方式
<target xsi:type="File" name="" fileName="" keepFileOpen="false"maxArchiveFiles="50" archiveAboveSize="52428800" layout=""/>
target为字目标标签 
name 目标名称
type="File" 存储方式文件格式  
fileName 文件路径和名称
KeepFileOpen:保持文件打开
maxArchiveFiles 生成文件最大数量
ArchiveAboveSize每个文件存储最大容量
Layout输出日志格式
-longdate 当前时间
-nodeName 服务器名称
-hostname 主机名称
-processid 进程id
-uppercase 日志级别
-logger 日志记录器
-aspnet-request-url请求URL
-aspnet-mvc-controller 控制器
-aspnet-mvc-action 方法
-aspnet-request-method Http请求类型
-aspnet-request-ip Http请求Ip
-aspnet-request-posted-body Http请求Body
-aspnet-request-posted-form Http请求Form
-aspnet-request-querystring Http请求Query
-aspnet-response-statuscode Http响应状态码
-message 日志信息
-exception 报错信息

4.5 日志中心收集模块
<rules></rules>声明规则
 <logger name="web" level="Debug,Info,Error,Warn,Fatal" writeTo="weblogfile,weblogtcp" />
Logger 记录器节点
Name 名称
Level 日志级别
WriteTo 日志写入的节点名称

5.日志组件使用方法
BKYL.Log组件源码和Web程序demo和Console程序demo
下载地址https://github.com/AnkerZhang/BKYL.Log.git
5.1BKYL.Log组件介绍
安装BKYL.Log最新版类库
install-package BKYL.Log 如图5-1-1

图5-1-1



或者用过NuGet包管理工具安装如图 5-1-2

图5-1-2
类库依赖
.Net Standard Version=v2.0
NLog(>=4.7.4)
NLog.WebAspNetCore(>=4.9.3)
Bkyl.Log组件主要封装Nlog核心功能，使得开发人员可以开箱即用
5.2ILog接口对象介绍
ILog接口命名空间是BKYL.Log.LogFactory
里面定义了 Info、Debug 、Warn、Error、Fatal 一些方法
Info级别日志（记录的消息，是否预警，是否打印控制台）
void Info(string message, bool is_warning = false, bool is_console = false);
Info级别日志（包含格式项的字符串，是否预警，是否打印控制台,要格式化的参数）
void Info(string format, bool is_warning = false, bool is_console = false, params object[] args);
Info级别日志(日志信息,异常对象,是否预警,是否打印控制台)
void Info(string message, Exception exception, bool is_warning = false, bool is_console = false);
Debug级别日志（记录的消息，是否预警，是否打印控制台）
void Debug(string message, bool is_warning = false, bool is_console = false);
Debug级别日志（包含格式项的字符串，是否预警，是否打印控制台,要格式化的参数）
void Debug(string format, bool is_warning = false, bool is_console = false, params object[] args);
Debug级别日志(日志信息,异常对象,是否预警,是否打印控制台)
void Debug(string message, Exception exception, bool is_warning = false, bool is_console = false);
Warn级别日志（记录的消息，是否预警，是否打印控制台）
void Warn(string message, bool is_warning = false, bool is_console = false);
Warn级别日志（包含格式项的字符串，是否预警，是否打印控制台,要格式化的参数）
void Warn(string format, bool is_warning = false, bool is_console = false, params object[] args);
Warn级别日志(日志信息,异常对象,是否预警,是否打印控制台)
void Warn(string message, Exception exception, bool is_warning = false, bool is_console = false);
Error级别日志（记录的消息，是否预警，是否打印控制台）
void Error(string message, bool is_warning = false, bool is_console = false);
Error级别日志（包含格式项的字符串，是否预警，是否打印控制台,要格式化的参数）
void Error(string format, bool is_warning = false, bool is_console = false, params object[] args);
Error级别日志(日志信息,异常对象,是否预警,是否打印控制台)
void Error(string message, Exception exception, bool is_warning = false, bool is_console = false);
Fatal级别日志（记录的消息，是否预警，是否打印控制台）
void Fatal(string message, bool is_warning = false, bool is_console = false);
Fatal级别日志（包含格式项的字符串，是否预警，是否打印控制台,要格式化的参数）
void Fatal(string format, bool is_warning = false, bool is_console = false, params object[] args);
Fatal级别日志(日志信息,异常对象,是否预警,是否打印控制台)
void Fatal(string message, Exception exception, bool is_warning = false, bool is_console = false);
同时组件在命名空间是BKYL.Log.LogFactory.Imp.Log对象实现了ILog接口
命名空间BKYL.Log中由静态类LogExtension
LogExtension包含GetGlobalLog静态方法 返回ILog对象
public static ILog GetGlobalLog(BKYL.Log.LogFactory.LogEnum log_type)
日志枚举 LogEnum 包含web：Web项目日志   console：控制台日志

5.3Web项目使用方式
Asp.dotnet core项目
在项目的根目录创建名为 nlog.config文件并将属性设置成始终复制
将源码文件中根目录的Web.nlog.config里面内容复制到nlog.config文件中
根据程序情况适当修改nlog.config配置文件。
以下配置只需要根据当前程序修改value即可。
<!--可根据environment key名称区分程序当前环境-->
 <variable name="environment" value="pro"/>   
<!--nodeName 请配置你的项目名字 例如program_test_log 这个名将会成为es索引前缀-->
  <variable name="nodeName" value="web_test_log"/>
<!--tcpAddress 关于程序日志发送的logstash地址和端口 例如39.96.160.43:8103 -->
  <variable name="webtcpAddress" value="62.234.155.90:8103"/>
<!--tcpAddress 触发预警接收端的邮箱可使用英文逗号配置多个邮箱 -->
  <variable name="notice" value="xxxxxx@xx.com"/>
在Program.cs文件中 
引用 using Nlog.Web
添加IHostBuilder的扩展方法 .UseNLog();如图5-3-1

图5-3-1
在 Startup.cs文件中
引用 using BKYL.Log;
ConfigureServices方法中 获取ILog对象
 var _log= LogExtension.GetGlobalLog(BKYL.Log.LogFactory.LogEnum.web);
记录日志只需要_log.Info()方法即可
具体方法和重载方法请详情5.2ILog对象介绍

5.4控制台程序使用介绍
Console项目 .net framework 或 dotnet core 项目
在项目的根目录创建名为 nlog.config文件并将属性设置成始终复制
将源码文件中根目录的Console.nlog.config里面内容复制到nlog.config文件中
根据程序情况适当修改nlog.config配置文件。
以下配置只需要根据当前程序修改value即可。
<!--可根据environment key名称区分程序当前环境-->
 <variable name="environment" value="pro"/>   
<!--nodeName 请配置你的项目名字 例如program_test_log 这个名将会成为es索引前缀-->
  <variable name="nodeName" value="program_test_log"/>
<!--tcpAddress 关于程序日志发送的logstash地址和端口 例如62.234.155.90:8103 -->
  <variable name="programtcpAddress" value="62.234.155.90:8103"/>
<!--tcpAddress 触发预警接收端的邮箱可使用英文逗号配置多个邮箱 -->
  <variable name="notice" value="xxxxxx@xx.com"/>
在Program.cs文件中Main方法中
引用 using Nlog.Web
获取ILog对象
 var _log= LogExtension.GetGlobalLog(BKYL.Log.LogFactory.LogEnum.console);
记录日志只需要_log.Info()方法即可
具体方法和重载方法请详情5.2ILog对象介绍
5.5Log日志对象最佳使用方式
通过LogExtension.GetGlobalLog方法获取到log对象将它使用单例模式缓存在静态变量中，在程序的生命周期中只获取一次即可。
1.在程序启动时获取ILog对象
2.全局使用同一个ILog对象
3.通过LogExtension.GetGlobalLog方法获取ILog对象缓存在静态属性中

5.6本地日志存储
nlog.config配置文件中
<target></target>节点中 xsi:type="File" 代表存储本地日志 
fileName 代表储存物理路径和文件名称
${basedir} 运行时程序所在目录
如果不需要存储本地日志文件 删除该节点即可
如果需要修改日志存放位置修改fileName即可
默认会将日志生存在程序的根目录logs文件夹中
5.7日志中心存储
nlog.config配置文件中
<target></target>节点中 xsi:type="Network" 代表将日志发送到logstash中 
address tcp协议的logstash文件中
如果不需要将日志发送日志中心删除该节点即可
默认会将日志生发送到62.234.155.90:8103 然后解析日志格式后存储在elastic search中

5.8日志中心查看存储日志方式
日志将生成两份一份存储在本地logs文件夹中另一份发送到日志中心存储在es中
本地日志文件在程序根路径logs文件中  
文件名称：程序名称+日期+日志等级.log

日志中心查看
登录 http://62.234.155.90:5601/
左侧菜单中选择Management 管理界面
创建一个kibana 索引Index Patterns 如图

Create index pattern 创建索引模式
搜索nlog.config中配置的nodeName 程序名称

然后选择@timestamp为时间戳索引  Create index pattern

索引就创建完成了，左侧找到菜单Discover

即可查询到程序发送的日志
