using CZGL.SystemInfo.Linux;
using Mono.Unix.Native;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Timers;

namespace ServiceProgram.Environment
{
    /// <summary>
    /// 
    /// </summary>
    public class SystemEnvironment
    {
        private readonly DynamicInfo info;
        private static readonly PerformanceCounter cpup = new PerformanceCounter("Processor", "% Processor Time", "_Total");

        public SystemEnvironment()
        {
            if (IsWindows())
            {

            }
            else
            {
                info = new DynamicInfo();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool IsWindows()
        {
            var platform = System.Environment.OSVersion.Platform;
            return platform == PlatformID.Win32NT;
        }
        /// <summary>
        /// 获取内存使用率
        /// </summary>
        /// <returns></returns>
        public double GetMemRate()
        {
            double rete = 0;
            if (IsWindows())
            {
                rete = CPUWin32LoadValue.UsedRatePhysicalMemory;
            }
            else
            {
                Mem mem = info.GetMem();
                rete = Math.Round((mem.Used * 1.00 / mem.Total) * 100.00, 2);
            }
            return rete;
        }
        /// <summary>
        /// 获取Cpu使用率
        /// </summary>
        /// <returns></returns>
        public double GetCpuRate()
        {
            double rete = 0;
            if (IsWindows())
            {
                AAA:
                rete = Math.Round(cpup.NextValue(), 2);
                if (rete <= 0)
                    goto AAA;

            }
            else
            {
                rete = Math.Round((100.00 - info.GetCpuState().Idolt), 2);
                if (rete >= 100)
                {
                    return 99.99;
                }
            }
            return rete;

        }

        /// <summary>
        /// 磁盘占用使用率
        /// </summary>
        /// <returns></returns>
        public double GetDriverRate()
        {
            if (IsWindows() == false)
            {
                var path = "/";
                string shellPathLine = string.Format("cd {0}", path);
                string printLine = " awk '{print $2,$3,$4,$5}'";
                string shellLine = string.Format("df -k {0} |", path) + printLine;

                Process p = new Process();
                p.StartInfo.FileName = "sh";
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardInput = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.CreateNoWindow = true;
                p.Start();
                p.StandardInput.WriteLine(shellPathLine);
                p.StandardInput.WriteLine(shellLine);
                p.StandardInput.WriteLine("exit");

                string strResult = p.StandardOutput.ReadToEnd();
                string[] arr = strResult.Split('\n');
                if (arr.Length == 0)
                {
                    return 0;
                }
                string[] resultArray = arr[1].TrimStart().TrimEnd().Split(' ');
                if (resultArray == null || resultArray.Length == 0)
                {
                    return 0;
                }

                //var TotalSize = Convert.ToInt32(resultArray[0]);
                //var UsedSize = Convert.ToInt32(resultArray[1]);
                //var AvailableSize = Convert.ToInt32(resultArray[2]);
                var Use = resultArray[3];
                //Console.WriteLine("================================");
                //Console.WriteLine(string.Format("Linux获取目录：{0},总大小:{1},已用:{2},未用:{3},使用率:{4}", path, TotalSize, UsedSize, AvailableSize, Use));
                //Console.WriteLine("================================");
                return Math.Round(Convert.ToDouble(Use.Replace("%", "")), 2);
            }
            else
            {


                long lsum = 0, ldr = 0;
                long gb = 1024 * 1024 * 1024;

                foreach (DriveInfo drive in DriveInfo.GetDrives())
                {
                    //判断是否是固定磁盘
                    //if (drive.DriveType == DriveType.Fixed)
                    //{
                    lsum += drive.TotalSize / gb;
                    ldr += drive.TotalFreeSpace / gb;
                    //}
                }
                if (ldr <= 0)
                {
                    return 0;
                }
                return Math.Round(ldr * 100.00 / lsum, 2);
            }
        }

    }
}
