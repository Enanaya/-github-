using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;
using Windows.Devices.Geolocation;

namespace 客户端.Models
{
    [Table("Bicycle_info")]

    public class Bicycle
    {

        [Column("bicycleid")]
        [PrimaryKey, NotNull]
        public string bicycle_id { get; set; }

        //public string btype { get; set; }
        [Column("usedtime")]
        public int used_time { get; set; } //使用次数

        [Column("distance")]
        public decimal distance { get; set; }

        public enum state { busy, free, maintenance, anomaly }

        [Column("supplyid")]
        public string supply_id { get; set; }

        [Column("servicetime")]
        public DateTime service_time { get; set; } //服务时长

        //23.0454 113.382  目前位置

        [NotNull]
        [Column("currentlocation")]
        public string current_location { get; set; } //当前位置

        [Column("lastlocation")]
        public string last_location { get; set; } //上一位置，可认为出发位置

        [NotNull, MaxLength(6)]
        [Column("password")]
        //没有实体，所以二维码不知如何实现，采用动态密码
        public string password { get; set; } //用于交互的动态密码，以数据库中绑定的密码加密得来

        //[Column("bicycleIcon")]
        //public string bicycleIcon
        //{
        //    get { return bicycleIcon; }
        //    set { value = BicycleIcon(); }
        //}

        //public string BicycleIcon()
        //{
        //    return "ms-appx:///Assets/Bicycle.jpg";
        //}
    }
}
