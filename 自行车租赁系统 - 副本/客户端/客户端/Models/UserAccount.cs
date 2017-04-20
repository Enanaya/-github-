using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 客户端.Models
{
    [Table("user")]
    public class UserAccount
    {
        [Column("userid")]
        [PrimaryKey, NotNull]
        public string user_id { get; set; }

        [Column("password")]
        [NotNull, MaxLength(6)]
        public string password { get; set; }

        [Column("amount")]
        public decimal amount { get; set; } //金钱余额

        public enum state { online, offline, freeze } //状态

        public enum rank { domestic, campus, vip } //客户级别

        [Column("ranknumber")]
        public string ranknumber { get; set; }  //vip id或 学生证id

        [Column("phonenumber")]
        [NotNull]
        public string phonenumber { get; set; }

        [Column("name")]
        [NotNull]
        public string name { get; set; } //真实姓名

        [Column("nickname")]
        public string nickname { get; set; } //昵称

        [Column("headpicture")]
        public string headpicture { get; set; } //头像路径

        [Column("individual_distance")]
        public double in_distance { get; set; }

        [Column("carbon_save")]
        public double carbon_save { get; set; }

        [Column("calorie_cousume")]
        public long calorie_cousume { get; set; }

        [Column("route")]     //以"1,3,5...."的形式存放该账户的route id
        public string routes { get; set; }
    }
}
