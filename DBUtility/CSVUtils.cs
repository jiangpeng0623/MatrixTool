using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace DBUtility
{
    public class CSVUtils
    {
        public static String createCSV(List<List<String>> valuesList)
        {
            StringBuilder builder = new StringBuilder();
            foreach (List<String> values in valuesList)
            {
                foreach (String value in values)
                {
                    String result = "\"" + value + "\"";
                    ///修改csv 逗号分隔符
                    builder.Append(result).Append("\t");
                }
                builder.AppendLine();
            }
            return builder.ToString();
        }
    }
}