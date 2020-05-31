using System.Collections.Generic;

namespace IFactory.UI
{
    public class CityInfo
    {
        //定义一个变量
        public string AddrName { get; set; }

        //定义一个变量
        public string CityName { get; set; }

        //定义一个变量
        public string TelNum { get; set; }

        //定义一个变量
        public double TotalSum { get; set; }

        public static List<CityInfo> GetInfo()
        {
            return new List<CityInfo>()
            {
                new CityInfo() { AddrName = "湖北", CityName = "武汉", TelNum = "123", TotalSum = 1.23 },
                new CityInfo() { AddrName = "广东", CityName = "广州", TelNum = "234", TotalSum = 1.23 },
                new CityInfo() { AddrName = "广西", CityName = "南宁", TelNum = "0152", TotalSum = 1.23 },
                new CityInfo() { AddrName = "湖南", CityName = "长沙", TelNum = "0123", TotalSum = 1.23 },
                new CityInfo() { AddrName = "江西", CityName = "南昌", TelNum = "123", TotalSum = 10.23 }
            };
        }
    }
}
