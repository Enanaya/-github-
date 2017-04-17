using SQLite.Net;
using SQLite.Net.Platform.WinRT;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Storage;

namespace 客户端.Models
{
    public static class BicycleDatabase
    {
        public readonly static string DbPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "自行车.db");

        public static SQLiteConnection GetDbConnection()
        {
            // 连接数据库，如果数据库文件不存在则创建一个空数据库。
            var conn = new SQLiteConnection(new SQLitePlatformWinRT(), DbPath);
            // 创建 Person 模型对应的表，如果已存在，则忽略该操作。
            conn.CreateTable<Bicycle>();
            //dataWrite(conn);
            return conn;
        }

        private static void dataWrite(SQLiteConnection conn)
        {

            var add = new Bicycle()
            {
                bicycle_id = "003",
                supply_id = "001",
                password = "009135",
                current_location = "23.0425980042,113.3881682065",
                //23.0425484162,113.3875000477
            };
            //暂时不插入重复数据

            conn.InsertOrReplace(add);
            //conn.Delete(new Bicycle() { bicycle_id = "002" });
        }
    }
}
