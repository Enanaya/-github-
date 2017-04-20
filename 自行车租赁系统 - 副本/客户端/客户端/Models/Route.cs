using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Services.Maps;

namespace 客户端.Models
{
    [Table("Routes")]
    public class Route
    {
        [Column("route_id"), PrimaryKey, NotNull]
        public string route_id { get; set; }

        [Column("user_id"), NotNull]
        public string user_id { get; set; }

        [Column("bicycle_id"), NotNull]
        public string bicycle_id { get; set; }

        [Column("start_time"), NotNull]
        public DateTime start_Time { get; set; }

        [Column("last_location"), NotNull]
        public string last_location { get; set; } //上一位置，可认为出发位置

        [Column("end_time"), NotNull]
        public DateTime end_time { set; get; }

        [Column("current_location"), NotNull]
        public string current_location { get; set; } //当前位置

        /// <summary>
        /// 一次路程花费的时间，无需初始化，计算而来
        /// </summary>
        [Column("time_consume")]
        public TimeSpan time_consume  
        {
            get { return time_consume; }
            set { value = Time_consume(); }
        }

        public TimeSpan Time_consume()
        {
            return new TimeSpan(end_time.Ticks) - new TimeSpan(start_Time.Ticks);
        }

        /// <summary>
        /// 一次路程的距离，同花费时间
        /// </summary>
        [Column("distance")]
        public string distance
        {
            get { return distance; }
            set { value = Distance().ToString(); }
        }

        public async Task<string> Distance()
        {
            MapRouteFinderResult routeResult =
             await MapRouteFinder.GetDrivingRouteAsync(
             new Geopoint(new BasicGeoposition()
           {
            Latitude = double.Parse(last_location.Split(',')[0]),
            Longitude = double.Parse(last_location.Split(',')[1])
           }),
             new Geopoint(new BasicGeoposition()
           {
            Latitude = double.Parse(current_location.Split(',')[0]),
            Longitude = double.Parse(current_location.Split(',')[1])
           }),
              MapRouteOptimization.Time,
              MapRouteRestrictions.None);

            return routeResult.Route.LengthInMeters.ToString();
        }

        [Column("money_consume"), NotNull]
        public Decimal money_consume { get; set; }

        [Column("calorie_consume"), NotNull]
        public long calorie_cousume { get; set; }

        [Column("carbon_save"), NotNull]
        public double carbon_save { get; set; }

    }

    //public static class Cal 
    //{

    //}
}
