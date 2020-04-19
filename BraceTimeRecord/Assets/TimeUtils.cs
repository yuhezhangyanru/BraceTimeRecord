using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    class TimeUtils
    {
         
        //格式化时间的函数
        public static string GetTime(float time)
        {
            float h = Mathf.FloorToInt(time / 3600f);
            float m = Mathf.FloorToInt(time / 60f - h * 60f);
            float s = Mathf.FloorToInt(time - m * 60f - h * 3600f);
            return h.ToString("00") + "小时" + m.ToString("00") + "分" + s.ToString("00") +"秒";
        }
    }
}
