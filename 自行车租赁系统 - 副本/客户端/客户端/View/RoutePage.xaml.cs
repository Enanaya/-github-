using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using 客户端.Models;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace 客户端.View
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class RoutePage : Page
    {
        public RoutePage()
        {
            this.InitializeComponent();
        }

        private void BackB_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage), this_account);
        }

        string this_account = "";

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                this_account = e.Parameter.ToString();
            }
        }

        public ObservableCollection<Route> routes { get; set; }
            = new ObservableCollection<Route>();


        private void Routetest() //用户数据库测试
        {
            using (SQLiteConnection conn = RouteDataBase.GetDbConnection())
            {
                //Debug.WriteLine(conn.DatabasePath.ToString());//找出数据库位置
                TableQuery<Route> t = conn.Table<Route>();
                var q = from s in t.AsParallel<Route>()
                        orderby s.route_id
                        select s;

                routes.Clear();

                foreach (var temp in q)
                {
                    routes.Add(temp);
                }

            }
        }
    }
}
