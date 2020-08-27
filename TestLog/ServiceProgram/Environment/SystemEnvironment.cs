using CZGL.SystemInfo.Linux;
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
        private static readonly PerformanceCounter cpup= new PerformanceCounter("Processor", "% Processor Time", "_Total");

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
                rete =Math.Round(cpup.NextValue(),2);
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
