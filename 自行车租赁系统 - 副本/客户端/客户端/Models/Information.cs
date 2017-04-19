using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 客户端.Models
{
    public class Information
    {
        public int infoId { get; set; }
        public string Title { get; set; }
        public string CoverImage { get; set; }
        public DateTime PublishedTime { get; set; }
        public string Content { get; set; }
    }

    public class InfoManager
    {
        public static List<Information> GetInfo()
        {
            var infomations = new List<Information>()
            {
                new Information(){infoId=1,Title="欢迎使用本自行车租赁系统！",
                    PublishedTime =DateTime.Now,Content="就当是第一次的信息内容吧",
                    CoverImage ="ms-appx:///Assets/Welcome_Logo.png" }
            };
            return infomations;
        }
    }
}
