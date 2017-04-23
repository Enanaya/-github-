using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Services.Maps;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using 客户端.Models;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace 客户端.View.Individual.RouteInfo
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class RouteItemMap : Page
    {
        private Route item_route;

        public RouteItemMap()
        {
            this.InitializeComponent();
        }

        string this_account;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                item_route = (Route)e.Parameter;
            }

        }

        private void BackB_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

        private async Task routeAccess()
        {
            var access = await Geolocator.RequestAccessAsync();
            switch (access)
            {
                case GeolocationAccessStatus.Unspecified:
                    //定位未开启提示是否跳转到 设置 页面            
                    return;
                case GeolocationAccessStatus.Allowed:           //允许获取        

                    await routeCreateAsync(
                        new Geopoint(new BasicGeoposition()
                        {
                            Latitude = double.Parse(item_route.last_location.Split(',')[0]),
                            Longitude = double.Parse(item_route.last_location.Split(',')[1])
                        }),
                       new Geopoint(new BasicGeoposition()
                       {
                           Latitude = double.Parse(item_route.current_location.Split(',')[0]),
                           Longitude = double.Parse(item_route.current_location.Split(',')[1])
                       }));

                    break;
                case GeolocationAccessStatus.Denied:            //不允许获取位置信息时 给予提示 然后根据情况选择是否跳转到 设置 界面           
                    await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings://privacy/location"));
                    return;
                default:
                    break;
            }
        }


        private async Task routeCreateAsync(Geopoint g1, Geopoint g2)
        {
            var access = await Geolocator.RequestAccessAsync();
            switch (access)
            {
                case GeolocationAccessStatus.Unspecified:
                    //定位未开启提示是否跳转到 设置 页面            
                    return;

                case GeolocationAccessStatus.Allowed:           //允许获取        
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
                        routeMap.Routes.Add(viewOfRoute);

                        // Fit the MapControl to the route.
                        await routeMap.TrySetViewBoundsAsync(
                              routeResult.Route.BoundingBox,
                              null,
                              Windows.UI.Xaml.Controls.Maps.MapAnimationKind.None);
                    }
                    break;

                case GeolocationAccessStatus.Denied:            //不允许获取位置信息时 给予提示 然后根据情况选择是否跳转到 设置 界面           
                    await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings://privacy/location"));
                    return;

                default:
                    break;
            }

        }

        private async void routeMap_Loaded(object sender, RoutedEventArgs e)
        {
            var start = new Geopoint(new BasicGeoposition()
            {
                Latitude = double.Parse(item_route.last_location.Split(',')[0]),
                Longitude = double.Parse(item_route.last_location.Split(',')[1])
            });

            var end = new Geopoint(new BasicGeoposition()
            {
                Latitude = double.Parse(item_route.current_location.Split(',')[0]),
                Longitude = double.Parse(item_route.current_location.Split(',')[1])
            });

            routeMap.Visibility = Visibility.Visible;
            //await routeAccess();
            await routeCreateAsync(start,end              );

            MapIcon startIco = new MapIcon()
            {
                Location = start,
                Title="起始点"
            };
        }
    }
}
