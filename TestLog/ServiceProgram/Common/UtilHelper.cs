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

        public static decimal ChangeDataToD(string strData)
        {
            decimal dData = 0.0M;
            if (strData.Contains("E")||strData.Contains("e"))
            {
                dData = Decimal.Parse(strData, System.Globalization.NumberStyles.Float);
            }
            return dData;
        }
    }
}
