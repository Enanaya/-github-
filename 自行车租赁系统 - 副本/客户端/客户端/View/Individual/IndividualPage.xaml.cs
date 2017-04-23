using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
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
    public sealed partial class IndividualPage : Page
    {
        public IndividualPage()    
        {
            this.InitializeComponent();
        }

        string this_account = "";

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                this_account = e.Parameter.ToString();
            }
        }

        private void BackB_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage), this_account);
        }



        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
        }

        private void privacyB_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PrivacyPage),this_account);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            dataload();
        }
        //private void Ellipse_PointerPressed(object sender, PointerRoutedEventArgs e)
        //{

        //}
       


        private void dataload() //用于打开页面加载路程数据等方法
        {

            using (SQLiteConnection conn = UserDatabase.GetDbConnection())
            {
                TableQuery<UserAccount> t = conn.Table<UserAccount>();
                var q = from s in t.AsParallel<UserAccount>()
                        orderby s.user_id where s.user_id==this_account
                        select s;

                ///如何将用户登陆后的账户通信到这些页面呢？？这个分支采用导航传参数
                
                foreach (var item in q)
                {
                    distanceText.Text = item.in_distance.ToString();
                    savecarbonText.Text = item.carbon_save.ToString();
                    calText.Text = item.calorie_cousume.ToString();
                    numberText.Text = item.phonenumber.ToString();
                }

            }
        }

        private void informationB_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(InformationPage),this_account);
        }

        private void settingB_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SettingPage),this_account);
        }

        private void myRounteB_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(RoutePage), this_account);
        }
    }
}
