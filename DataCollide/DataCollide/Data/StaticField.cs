using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollide.Data
{
    public static class StaticField
    {
        public static IntPtr MainHandler { get; set; }

        public static Dictionary<string, Encoding> SupportEncodings = new Dictionary<string, Encoding>()
        {
            {"UTF-8", Encoding.UTF8 },
            {"UTF-32", Encoding.UTF32 },
            {"GB2312", Encoding.GetEncoding("gb2312") },
            {"GBK", Encoding.GetEncoding("gbk") },
        };
    }
}
