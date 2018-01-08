using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MatrixTool.View.Common
{
    public class Common
    {
        public static string Nottrained0 = "style=\"background-color:#FF2222\"";//红色 #FF2222  该人与该课程关联，状态为Missining/REVISION,或不存在该Intial完成该课程
        public static string Notrequired2 = "style=\"background-color:#DDDDDD\"";// 灰色 #DDDDDD  该人不与该课程关联。
        public static string Trained3 = "style=\"background-color:#11EE11\""; // #11EE11 绿色 该人与该课程关联，状态为TAKEN。
    }
}