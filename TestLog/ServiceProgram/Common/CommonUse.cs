using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace ServiceProgram.Common
{
    public static class CommonUse
    {
        private static readonly object lockComput = new object();
        /// <summary>
        /// 与当前时间比较,返回毫秒数
        /// </summary>
        /// <param name="t1">当前时间</param>
        /// <param name="t2">获取时间</param>
        /// <returns></returns>
        public static double GetResult(DateTime dt1, DateTime dt2)
        {
            lock (lockComput)
            {
                TimeSpan t1 = new TimeSpan(dt1.Ticks);
                TimeSpan t2 = new TimeSpan(dt2.Ticks);
                return t1.Subtract(t2).TotalMilliseconds;
            }
        }

        /// <summary>
        /// cpu百分比
        /// </summary>
        public static float PercentCPU { get; set; }
        /// <summary>
        /// 可用的内存
        /// </summary>
        public static float UsedMemory { get; set; }
        /// <summary>
        /// 应用程序路径
        /// </summary>
        public static string ApplicationPath { get; set; }
        /// <summary>
        /// 工作者唯一ID
        /// </summary>
        public static string WorkId { get; set; }
        public static string GetAppPath()
        {
            return ApplicationPath = Process.GetCurrentProcess().MainModule.FileName;
        }
        public static float GetPercentCPU()
        {
            try
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    var currentProcessName = Process.GetCurrentProcess().ProcessName;
                    var cpuCounter = new PerformanceCounter("Process", "% Processor Time", currentProcessName);
                    PercentCPU = cpuCounter.NextValue();
                }
                else
                {
                    var currentProcessPID = Process.GetCurrentProcess().Id;
                    var process = new Process
                    {
                        StartInfo = new ProcessStartInfo("top", "-b -n1")
                    };
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.UseShellExecute = false;
                    process.Start();
                    var cpuInfo = process.StandardOutput.ReadToEnd();
                    process.WaitForExit();
                    process.Dispose();
                    var lines = cpuInfo.Split('\n');
                    foreach (var item in lines)
                    {
                        if (item.Contains(currentProcessPID.ToString()))
                        {
                            var li = item.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            for (int i = 0; i < li.Length; i++)
                            {
                                if (li[i] == "R" || li[i] == "S")
                                {
                                    PercentCPU = float.Parse(li[i + 1]);
                                    break;
                                }
                            }
                            break;
                        }
                    }
                }
                return PercentCPU = float.Parse(Math.Round(PercentCPU, 3).ToString("F3"));
            }
            catch
            {
                return 0;
            }
        }
        /// <summary>
        /// 获取当前进程使用的内存以MB为单位
        /// </summary>
        /// <returns></returns>
        public static float GetUsedMemory()
        {
            try
            {
                Process currentProcess = Process.GetCurrentProcess();
                return UsedMemory = currentProcess.WorkingSet64 / 1024 / 1024;
            }
            catch
            {
                return 0;
            }
        }
        /// <summary>
        /// 删除日志文件
        /// </summary>
        /// <param name="fileDirect">文件夹目录:AppDomain.CurrentDomain.BaseDirectory + "BKLog";</param>
        /// <param name="saveDay">保留天数</param>
        public static void DeleteFile(string fileDirect, int saveDay)
        {
            try
            {
                DateTime nowTime = DateTime.Now;
                DirectoryInfo DllFolder1 = new DirectoryInfo(fileDirect);
                FileInfo[] fileInfosNew1 = DllFolder1.GetFiles("*.txt");
                foreach (FileInfo file in fileInfosNew1)
                {
                    TimeSpan t = nowTime - file.CreationTime;  //当前时间 减去文件创建时间
                    int day = t.Days;
                    if (day > saveDay)   //保存的时间；单位：天
                    {
                        File.Delete(file.FullName); //删除超过时间的文件夹
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("删除日志文件" + ex.Message + ex.StackTrace);
            }
        }
        public static string GetWorkerId()
        {
            return WorkId = Guid.NewGuid().ToString("N");
        }
    }
}
