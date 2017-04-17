using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;


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
    }
}
