using System;
using System.Collections.Generic;
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

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace 客户端.View
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class InformationPage : Page
    {
        private List<Information> Informations;

        public InformationPage()
        {
            this.InitializeComponent();
            Informations = InfoManager.GetInfo();
        }

        private void BackB_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

        private async void infoList_ItemClick(object sender, ItemClickEventArgs e)
        {
            //无法显示出来
            var info = (Information)e.ClickedItem;
            var cd = new ContentDialog()
            {
                Title = info.Title,
                Content = info.Content,
                PrimaryButtonText = "确定",
                IsSecondaryButtonEnabled = false
            };
            cd.PrimaryButtonClick += (_s, _e) => { };
            await cd.ShowAsync();
        }
    }
}
