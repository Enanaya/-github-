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
    class RouteDataBase
    {
        public readonly static string DbPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "用户.db");

        public static SQLiteConnection GetDbConnection()
        {
            // 连接数据库，如果数据库文件不存在则创建一个空数据库。
            var conn = new SQLiteConnection(new SQLitePlatformWinRT(), DbPath);
            // 创建 Person 模型对应的表，如果已存在，则忽略该操作。
            conn.CreateTable<Route>();
            dataWrite(conn);
            return conn;
        }

        private static void dataWrite(SQLiteConnection conn)
        {
            var add = new List<Route>()
            {
                new Route (){route_id="1",user_id="001",bicycle_id="002", 
                    last_location ="23.0451771289,113.3829456568",
                    start_Time =Convert.ToDateTime("2017-4-10 16:57"),
                    current_location="23.0423435989,113.3889216185",
                    end_time=Convert.ToDateTime("2017-4-10 17:07"),
                    money_consume=1.0M,calorie_cousume=50,carbon_save=151,
                },
                new Route (){route_id="2",user_id="002",bicycle_id="001", 
                    last_location ="23.0456263416,113.3893883228",
                    start_Time =Convert.ToDateTime("2017-4-12 9:01"),
                    current_location="23.0381623057,113.3949887753",
                    end_time=Convert.ToDateTime("2017-4-12 9:15"),
                    money_consume=0,calorie_cousume=47,carbon_save=153
                },
                new Route (){route_id="3",user_id="001",bicycle_id="003",
                    last_location ="23.0402307538,113.38355183606",
                    start_Time =Convert.ToDateTime("2017-4-15 20:45"),
                    current_location="23.0456362148,113.3791315556",
                    end_time=Convert.ToDateTime("2017-4-15 21:00"),
                    money_consume=0.5M,calorie_cousume=60,carbon_save=200
                },
                new Route (){route_id="3",user_id="003",bicycle_id="002",
                    last_location ="23.0468258802,113.3821195364",
                    start_Time =Convert.ToDateTime("2017-4-20 16:57"),
                    current_location="23.0475760156,113.3913195134",
                    end_time=Convert.ToDateTime("2017-4-20 17:07"),
                    money_consume=0.5M,calorie_cousume=34,carbon_save=100,
                }
            };

            foreach (var item in add)
            {
                conn.InsertOrReplace(item);
            }
        }
    }
}
