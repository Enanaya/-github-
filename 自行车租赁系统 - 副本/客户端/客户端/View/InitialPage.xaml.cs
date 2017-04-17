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
using 客户端.ViewModels;
using Windows.Devices.Geolocation; //地图用
using Windows.Storage;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace 客户端
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {

        public MainPage()
        {
            this.InitializeComponent();
            myFrame.Navigate(typeof(MainPage));
        }

        private void SearchButon_Click(object sender, RoutedEventArgs e)
        {



        }

        //private void Page_Loaded(object sender, RoutedEventArgs e)
        //{

        //}

        //private  void map_Loaded(object sender, RoutedEventArgs e)
        //{
        //    //var access = await Windows.Devices.Geolocation.Geolocator.RequestAccessAsync();
        //    //switch (access)
        //    //{
        //    //    case GeolocationAccessStatus.Unspecified:
        //    //        //定位未开启提示是否跳转到 设置 页面            
        //    //        return;
        //    //    case GeolocationAccessStatus.Allowed:           //允许获取        
        //    //        Geolocator geolocator = new Geolocator();
        //    //        geolocator.DesiredAccuracyInMeters = 10;
        //    //        geolocator.DesiredAccuracy = PositionAccuracy.High;
        //    //        Geoposition pos = await geolocator.GetGeopositionAsync();
                    
                   
        //    //        Geopoint myLocation = pos.Coordinate.Point;

        //    //        // Set the map location.
        //    //        map.Center = myLocation;
                   
        //    //        break;
        //    //    case GeolocationAccessStatus.Denied:            //不允许获取位置信息时 给予提示 然后根据情况选择是否跳转到 设置 界面           
        //    //        await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings://privacy/location"));
        //    //        return;
        //    //    default:
        //    //        break;
        //    //}
        //}

       
    }
}
