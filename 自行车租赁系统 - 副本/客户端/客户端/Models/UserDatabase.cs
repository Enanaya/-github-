using SQLite.Net;
using SQLite.Net.Platform.WinRT;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace 客户端.Models
{
    public static class UserDatabase
    {
        public readonly static string DbPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "用户.db");

        public static SQLiteConnection GetDbConnection()
        {
            // 连接数据库，如果数据库文件不存在则创建一个空数据库。
            var conn = new SQLiteConnection(new SQLitePlatformWinRT(), DbPath);
            // 创建 Person 模型对应的表，如果已存在，则忽略该操作。
            conn.CreateTable<UserAccount>();
            dataWrite(conn);
            return conn;
        }

        private static void dataWrite(SQLiteConnection conn)
        {
            var add = new List<UserAccount>()
            {
                new UserAccount (){ user_id="001",password="qwerty",name="张三",phonenumber="123456789012",in_distance=50.5 } ,
                new UserAccount (){ user_id="002",password="asdfgh",name="李四",phonenumber="123456789012",in_distance=23 },
                new UserAccount (){ user_id="003",password="zxcvbn",name="王五",phonenumber="123456789012" }
            };

            foreach (var item in add)
            {
                conn.InsertOrReplace(item);
            }
        }
    }
}
