using SQLite.Net;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using 客户端.Models;
using 客户端.Resource;
using 客户端.View.Privacy;


// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace 客户端.View
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class PrivacyPage : Page
    {
        public PrivacyPage()
        {
            this.InitializeComponent();
        }

        private void headiconR_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            var dialog = new ContentDialog()
            {
                Title = "消息提示",
                Content = "当前设置尚未保存，你确认要退出该页面吗?",
                PrimaryButtonText = "确定",
                SecondaryButtonText = "取消",
                FullSizeDesired = false,
            };

        }

        private void BackB_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (this.Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

        string this_account = "";

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                this_account = e.Parameter.ToString();
            }
        }

        private void dataload() //用于打开页面加载路程数据等方法
        {

            using (SQLiteConnection conn = UserDatabase.GetDbConnection())
            {
                TableQuery<UserAccount> t = conn.Table<UserAccount>();
                var q = from s in t.AsParallel<UserAccount>()
                        orderby s.user_id
                        where s.user_id == this_account
                        select s;


                foreach (var item in q)
                {
                    nicknameText.Text = item.nickname.ToString();
                    nameText.Text = item.name.ToString();
                    phoneText.Text = item.phonenumber.ToString();
                    //headpictureE  如何设置头像？
                }

            }
        }

        private void nickB_Click(object sender, RoutedEventArgs e)
        {
            #region
            //var contendialog = new ContentDialog()
            //{
            //    Content = new MyContentDialog(),
            //    PrimaryButtonText = "确定",
            //    SecondaryButtonText = "取消",
            //    FullSizeDesired = false
            //};
            //contendialog.PrimaryButtonClick += (_s, _e) =>
            //{
            //};
            //contendialog.ShowAsync();
            #endregion
            Frame.Navigate(typeof(NickName), this_account);

        }

        private void nameB_Click(object sender, RoutedEventArgs e)
        {

        }

        private void phoneB_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            dataload();
        }
    }
}
