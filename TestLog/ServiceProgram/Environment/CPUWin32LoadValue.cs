using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace ServiceProgram.Environment
{
    /// <summary>
    /// 
    /// </summary>
    public class CPUWin32LoadValue
    {
        private static ulong g_tsSysDeltaTime = 0;
        private static ulong g_tsSysLastTime = 0;

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetProcessTimes(IntPtr hProcess, out FILETIME
            lpCreationTime, out FILETIME lpExitTime, out FILETIME lpKernelTime,
            out FILETIME lpUserTime);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern void GetSystemTimeAsFileTime(out FILETIME lpExitTime);

        private static DateTime FiletimeToDateTime(FILETIME fileTime)
        {
            //NB! uint conversion must be done on both fields before ulong conversion
            ulong hFT2 = unchecked((((ulong)(uint)fileTime.dwHighDateTime) << 32) | (uint)fileTime.dwLowDateTime);
            return DateTime.FromFileTimeUtc((long)hFT2);
        }

        private static TimeSpan FiletimeToTimeSpan(FILETIME fileTime)
        {
            //NB! uint conversion must be done on both fields before ulong conversion
            ulong hFT2 = unchecked((((ulong)(uint)fileTime.dwHighDateTime) << 32) |
                (uint)fileTime.dwLowDateTime);
            return TimeSpan.FromTicks((long)hFT2);
        }

        private static ulong FiletimeToUlong(FILETIME fileTime)
        {
            //NB! uint conversion must be done on both fields before ulong conversion
            ulong hFT2 = unchecked((((ulong)(uint)fileTime.dwHighDateTime) << 32) |
                (uint)fileTime.dwLowDateTime);
            return hFT2;
        }

        private static double QUERY_CPULOAD()
        {
            if (!SystemEnvironment.IsWindows())
            {
                return 0;
            }

            GetSystemTimeAsFileTime(out FILETIME ftNow);

            if (!GetProcessTimes(Process.GetCurrentProcess().Handle,
                out FILETIME ftCreation,
                out FILETIME ftExit,
                out FILETIME ftKernel,
                out FILETIME ftUser))
            {
                return 0;
            }

            ulong tsCpuUsageTime = (FiletimeToUlong(ftKernel) +
                FiletimeToUlong(ftUser));
            if (g_tsSysDeltaTime == 0)
            {
                g_tsSysDeltaTime = tsCpuUsageTime;
                return QUERY_CPULOAD();
            }

            ulong ftSystemNowTime = FiletimeToUlong(ftNow);
            ulong tsSysTimeDelta = ftSystemNowTime - g_tsSysLastTime;
            ulong tsSystemTimeDelta = tsCpuUsageTime - g_tsSysDeltaTime;

            double cpu_load = (tsSystemTimeDelta * 100.00d + tsSysTimeDelta / 2.00d) / tsSysTimeDelta;
            g_tsSysLastTime = ftSystemNowTime;
            g_tsSysDeltaTime = tsCpuUsageTime;

            cpu_load = cpu_load / System.Environment.ProcessorCount;
            if (cpu_load <= 0 ||
                double.IsInfinity(cpu_load) ||
                double.IsNaN(cpu_load) ||
                double.IsNegativeInfinity(cpu_load) ||
                double.IsPositiveInfinity(cpu_load))
            {
                cpu_load = 0;
            }

            return Math.Round(cpu_load * 100.00, 2);
        }

        public static double CPULOAD { get; private set; }

        public static void Refresh() => CPULOAD = QUERY_CPULOAD();


        /// <summary>
        /// 内存使用率
        /// </summary>
        public static double UsedRatePhysicalMemory
        {
            get
            {
                MEMORY_INFO mi = GetMemoryStatus();
                return Math.Round(100 - (mi.ullAvailPhys * 100.00 / mi.ullTotalPhys), 2);
            }
        }
        private static MEMORY_INFO _memoryStatusInfo = new MEMORY_INFO();
        private static readonly Stopwatch _getMemoryStatusWath = new Stopwatch();
        private unsafe static MEMORY_INFO GetMemoryStatus()
        {
            lock (_getMemoryStatusWath)
            {
                if (!_getMemoryStatusWath.IsRunning || _getMemoryStatusWath.ElapsedMilliseconds >= 500)
                {
                    _getMemoryStatusWath.Restart();
                    _memoryStatusInfo.dwLength = (uint)sizeof(MEMORY_INFO);

                    GlobalMemoryStatusEx(ref _memoryStatusInfo);

                }
                return _memoryStatusInfo;
            }
        }
        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GlobalMemoryStatusEx(ref MEMORY_INFO mi);
        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct MEMORY_INFO
        {
            public uint dwLength; // 当前结构体大小
            public uint dwMemoryLoad; // 当前内存使用率
            public long ullTotalPhys; // 总计物理内存大小
            public long ullAvailPhys; // 可用物理内存大小
            public long ullTotalPageFile; // 总计交换文件大小
            public long ullAvailPageFile; // 总计交换文件大小
            public long ullTotalVirtual; // 总计虚拟内存大小
            public long ullAvailVirtual; // 可用虚拟内存大小
            public long ullAvailExtendedVirtual; // 保留 这个值始终为0
        }
    }
}
