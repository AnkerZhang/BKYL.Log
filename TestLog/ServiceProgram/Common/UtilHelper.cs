using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceProgram.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class UtilHelper
    {
        public static BKYL.Log.LogFactory.ILog log;

        public static decimal ChangeDataToD(string strData)
        {
            decimal dData = 0.0M;
            if (strData.Contains("E") || strData.Contains("e"))
            {
                dData = Decimal.Parse(strData, System.Globalization.NumberStyles.Float);
            }
            return dData;
        }

        /// <summary>
        /// 单位换算 
        /// </summary>
        /// <param name="strSite"></param>
        /// <returns>返回KB单位</returns>
        public static long UnitsChange(string strSite)
        {
            strSite = strSite.ToLower();
            long data = 0;
            if (strSite.Contains("kb"))
            {
                strSite = strSite.Replace("kb", "");
                long.TryParse(strSite, out data);
            }
            else if (strSite.Contains("mb"))
            {
                strSite = strSite.Replace("mb", "");
                if (long.TryParse(strSite, out data))
                {
                    data = data * 1024;
                }
            }
            else if (strSite.Contains("gb"))
            {
                strSite = strSite.Replace("gb", "");
                if (long.TryParse(strSite, out data))
                {
                    data = data * 1024 * 1024;
                }
            }
            else if (strSite.Contains("tb"))
            {
                strSite = strSite.Replace("tb", "");
                if (long.TryParse(strSite, out data))
                {
                    data = data * 1024 * 1024 * 1024;
                }
            }
            else if (strSite.Contains("b"))
            {
                strSite = strSite.Replace("b", "");
                if (long.TryParse(strSite, out data))
                {
                    data = data / 1024;
                }
            }
            return data;
        }

    }
}
