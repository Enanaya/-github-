using SQLite.Net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using 客户端.Models;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace 客户端.View
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            this.InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string account = accountBox.Text;
            string password = passwordbox.Password;
            using (SQLiteConnection conn = UserDatabase.GetDbConnection())
            {
                TableQuery<UserAccount> t = conn.Table<UserAccount>();
                var q = from s in t.AsParallel<UserAccount>()
                        orderby s.user_id
                        select s;

                if (q.Any(temp => account == temp.user_id && password == temp.password))
                {
                    await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                    {
                        ((Frame)Window.Current.Content).Navigate(typeof(MainPage)
                            ,account);
                    });
                }
                else
                {
                    accountBox.Text = "";
                    passwordbox.Password = "";
                    new MessageDialog("账户或密码错误").ShowAsync();
                }
            }
        }

        private void StackPanel_KeyUp(object sender, KeyRoutedEventArgs e)
        {

        }

        private void Page_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key== VirtualKey.Enter)
            {
                Button_Click(sender, e);
            }
        }
    }
}
