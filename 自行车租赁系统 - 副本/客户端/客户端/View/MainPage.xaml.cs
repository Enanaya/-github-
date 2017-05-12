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
using 客户端.View;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.ViewManagement;
using 客户端.Models;
using SQLite.Net;
using SQLite.Net.Platform.WinRT;
using System.Collections.ObjectModel;
using System.Text;
using Windows.UI.Popups;
using Windows.Services.Maps;
using Windows.UI;
using System.Diagnostics;
using Windows.Storage.Streams;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace 客户端
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {

        MainPageViewModels view = new MainPageViewModels();
        MapIcon current_mapicon = new MapIcon() { Title = "Your Location" };
        MapIcon end_mapicon = new MapIcon();

        //路线连接，geopoint声明
        Geopoint myLocation;
        Geopoint current_point;

        public MainPage()
        {
            this.InitializeComponent();
            DataContext = view;
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(500, 750));

        }

        public ObservableCollection<UserAccount> useracccount { get; set; }
            = new ObservableCollection<UserAccount>();


        private void Usertest() //用户数据库测试
        {
            using (SQLiteConnection conn = UserDatabase.GetDbConnection())
            {
                Debug.WriteLine(conn.DatabasePath.ToString());//找出数据库位置
                TableQuery<UserAccount> t = conn.Table<UserAccount>();
                var q = from s in t.AsParallel<UserAccount>()
                        orderby s.user_id
                        select s;

                useracccount.Clear();

                foreach (var temp in q)
                {
                    useracccount.Add(temp);
                }

                //StringBuilder msg = new StringBuilder();
                //msg.AppendLine($"数据库中总共 {useracccount.Count()} 个 user 对象。");

                /////测试数据库出来的实例location转换

                //foreach (var item in useracccount)
                //{
                //    msg.AppendLine($"Id：{item.user_id}；Name：{item.name}");
                //}

                //new MessageDialog(msg.ToString()).ShowAsync();
            }
        }


        public ObservableCollection<Bicycle> bicycle { get; set; }
            = new ObservableCollection<Bicycle>();

        private void Bicycletest() //单车数据库测试
        {

            using (SQLiteConnection conn = BicycleDatabase.GetDbConnection())
            {
                TableQuery<Bicycle> t = conn.Table<Bicycle>();
                var q = from s in t.AsParallel<Bicycle>()
                        orderby s.bicycle_id
                        select s;
                // 绑定

                bicycle.Clear();

                foreach (var temp in q)
                {
                    bicycle.Add(temp);
                }

                //StringBuilder msg = new StringBuilder();
                //msg.AppendLine($"数据库中总共 {bicycle.Count()} 个 bicycle 对象。");

                /////测试数据库出来的实例location转换

                //foreach (var item in bicycle)
                //{
                //    msg.AppendLine($"Id：{item.bicycle_id}；Location：{item.current_location}");
                //}

                //new MessageDialog(msg.ToString()).ShowAsync();


                foreach (var temp in bicycle.Select(temp => current_location_parse(temp.current_location))
                    .Select(temp => new Geopoint(new BasicGeoposition()
                    {
                        Latitude = temp.x,
                        Longitude = temp.y,
                    })).Select(temp => new MapIcon() { Location = temp,
                        Image = RandomAccessStreamReference.
                     CreateFromUri(new Uri("ms-appx:///Assets/Bicycle.png")),
                    }))
                {
                    map.MapElements.Add(temp);
                }

                #region
                //List<List<string>> lat_lon = new List<List<string>>();
                ////将现在位置存入
                //foreach (var item in bicycle)
                //{
                //    lat_lon.Add(item.current_location.Split(',').ToList());
                //}


                //List<Geopoint> bikepoint = new List<Geopoint>();

                //foreach (var item in lat_lon)
                //{
                //    bikepoint.Add(new Geopoint(new BasicGeoposition()
                //    {
                //        Latitude = double.Parse(item[0]),
                //        Longitude = double.Parse(item[1]),
                //    }));
                //}

                //List<MapIcon> bikeicon = new List<MapIcon>();

                //foreach (var item in bikepoint)
                //{
                //    bikeicon.Add(new MapIcon() { Location = item }); //单车图标未定

                //}

                //foreach (var item in bikeicon)
                //{
                //    map.MapElements.Add(item); 
                //}
                #endregion

            }

            (double x, double y) current_location_parse(string current_location)
            {
                var str = current_location.Split(',');
                double.TryParse(str[0], out var x);
                double.TryParse(str[1], out var y);
                return (x, y);
            }
        }

        private async void AppBarButton_ClickAsync(object sender, RoutedEventArgs e)
        {
            #region
            string[] lat_lon = bicycle[2].current_location.Split(',');
            current_point = new Geopoint(new BasicGeoposition()
            {
                Latitude = double.Parse(lat_lon[0]),
                Longitude = double.Parse(lat_lon[1])
            });

            #endregion
            await routeCreateAsync(myLocation, current_point);
        }

        private async System.Threading.Tasks.Task routeCreateAsync(Geopoint g1, Geopoint g2)
        {
            // Get the route between the points.
            MapRouteFinderResult routeResult =
                  await MapRouteFinder.GetWalkingRouteAsync(g1, g2);


            if (routeResult.Status == MapRouteFinderStatus.Success)
            {
                // Use the route to initialize a MapRouteView.
                MapRouteView viewOfRoute = new MapRouteView(routeResult.Route);
                viewOfRoute.RouteColor = Colors.Yellow;
                viewOfRoute.OutlineColor = Colors.Black;

                // Add the new MapRouteView to the Routes collection
                // of the MapControl.
                map.Routes.Add(viewOfRoute);

                // Fit the MapControl to the route.
                await map.TrySetViewBoundsAsync(
                      routeResult.Route.BoundingBox,
                      null,
                      Windows.UI.Xaml.Controls.Maps.MapAnimationKind.None);
            }
        }

        private void SearchButon_Click(object sender, RoutedEventArgs e)
        {

            //Usertest();
            //FindMapAddress(addressBox.Text);
        }

        private async void FindMapAddress(string address)
        {
            BasicGeoposition queryHint = new BasicGeoposition();
            queryHint.Latitude = 47.643;
            queryHint.Longitude = -122.131;
            Geopoint hintPoint = new Geopoint(queryHint);

            MapLocationFinderResult result =
         await MapLocationFinder.FindLocationsAsync(address, hintPoint,1);

            map.Center = result.Locations[0].Point;
        }

        private void map_Loaded(object sender, RoutedEventArgs e)
        {
            Locate(16);
            Bicycletest();
        }

        private async void Locate(double zoom)
        {
            var access = await Windows.Devices.Geolocation.Geolocator.RequestAccessAsync();
            switch (access)
            {
                case GeolocationAccessStatus.Unspecified:
                    //定位未开启提示是否跳转到 设置 页面            
                    return;
                case GeolocationAccessStatus.Allowed:           //允许获取        
                    Geolocator geolocator = new Geolocator();
                    geolocator.DesiredAccuracy = PositionAccuracy.High;
                    Geoposition pos = await geolocator.GetGeopositionAsync();

                    myLocation = pos.Coordinate.Point;

                    //Set the map icon
                    map.MapElements.Add(current_mapicon);
                    current_mapicon.Location = myLocation;
                    current_mapicon.NormalizedAnchorPoint = new Point(0.5, 1.0);
                    current_mapicon.CollisionBehaviorDesired = MapElementCollisionBehavior.RemainVisible;
                    current_mapicon.ZIndex = 0;

                    // Set the map location.
                    map.Center = current_mapicon.Location;
                    map.ZoomLevel = zoom;
                    //testblock.Text = myLocation.Position.Latitude.ToString() +
                    //    "  " + myLocation.Position.Longitude.ToString();

                    break;
                case GeolocationAccessStatus.Denied:            //不允许获取位置信息时 给予提示 然后根据情况选择是否跳转到 设置 界面           
                    await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings://privacy/location"));
                    return;
                default:
                    break;
            }
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MySpilt.IsPaneOpen = !MySpilt.IsPaneOpen;
        }

        private void IndividualB_Click(object sender, RoutedEventArgs e)
        {
            myFrame.Navigate(typeof(IndividualPage), this_account);

        }

        string this_account;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                this_account = e.Parameter.ToString();
            }
        }

        private void RouteB_Click(object sender, RoutedEventArgs e)
        {
            //myFrame.Navigate(typeof(RoutePage), this_account);
        }

        private void LocateB_Click(object sender, RoutedEventArgs e)
        {
            Locate(16);
        }

        private async void map_PointerPressed(object sender, PointerRoutedEventArgs e) //获得地图触摸的位置点
        {
            //Geolocator geolocator = new Geolocator();



            //end_mapicon.Title = "end here";
            //map.MapElements.Add(end_mapicon);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UseBicycleButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as ToggleButton;
            //button.IsChecked
        }

        private void map_MapTapped(MapControl sender, MapInputEventArgs args)
        {
            Debug.WriteLine(args.Location.Position.Latitude + ","
                + args.Location.Position.Longitude);
            
            
        }
    }
}
