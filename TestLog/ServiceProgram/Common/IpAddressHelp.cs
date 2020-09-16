using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;

namespace ServiceProgram.Common
{
    public class IpAddressHelp
    {
        public static string HostIP = null;
        public static string GetLocalIP()
        {
            if (string.IsNullOrEmpty(HostIP))
            {
                try
                {
                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    {
                        string HostName = Dns.GetHostName(); //得到主机名
                        IPHostEntry IpEntry = Dns.GetHostEntry(HostName);
                        for (int i = 0; i < IpEntry.AddressList.Length; i++)
                        {
                            if (IpEntry.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                            {
                                HostIP = IpEntry.AddressList[i].ToString();
                            }
                        }
                    }
                    else
                    {
                        var process = new Process
                        {
                            StartInfo = new ProcessStartInfo("ifconfig")
                            {
                                RedirectStandardOutput = true,
                                UseShellExecute = false
                            }
                        };
                        process.Start();
                        var hddInfo = process.StandardOutput.ReadToEnd();
                        process.WaitForExit();
                        process.Dispose();
                        var lines = hddInfo.Split('\n');
                        foreach (var item in lines)
                        {
                            if (string.IsNullOrWhiteSpace(HostIP))
                            {
                                if (item.Contains("inet") && (item.Contains("广播") || item.Contains("broadcast")))
                                {
                                    var li = item.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                    HostIP = li[1];
                                }
                            }
                        }
                    }
                    return string.IsNullOrEmpty(HostIP) ? "" : HostIP;
                }
                catch
                {
                    return "";
                }
            }
            else
            {
                return HostIP;
            }
        }
    }
}
