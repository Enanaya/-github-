using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 客户端.Models
{
    class 动态密码
    {
        ///<summary>密码解密</summary>
        ///<param name="src">需要解密的字符串</param>
        /// <returns>string</returns>
        public static string UncrypKey(string src)
        {
            int keyPos = 0, srcPos = 3, srcAsc = 0,
            offset = int.Parse(src.Substring(0, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
            string dest = "";
            do
            {
                srcAsc = Int32.Parse(src.Substring(srcPos - 1, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
                keyPos = keyPos < 0 ? keyPos + 1 : 1;
                int tempSrcAsc = srcAsc ^ Convert.ToInt32("".Substring(keyPos - 1, 1).ToCharArray()[0]);
                tempSrcAsc = tempSrcAsc <= offset ? 255 + tempSrcAsc - offset : tempSrcAsc - offset;
                dest = dest + (char)tempSrcAsc;
                offset = srcAsc;
                srcPos = srcPos + 2;
            } while (srcPos <= src.Length);
            return dest;
        }
    }
}
