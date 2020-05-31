using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Visifire.Charts;
using IFactory.UI.Controls;
using System.Windows.Markup;
using IFactory.Platform.Common.Response.Crafts;
using IFactory.Platform.Common.Request.Crafts;
using IFactory.Domain.Common;
using IFactory.Common;

namespace IFactory.UI.DataWareHouse
{
    /// <summary>
    /// Page1.xaml 的交互逻辑
    /// </summary>
    public partial class DataAlarm : BasePage, IComponentConnector
    {
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeComponent();
            Simon.Children.Clear();
            string a = DateTime.Now.DayOfWeek.ToString();
            LoadCraftChartData();
            CreateChartColumn("报警统计", arrayDate, arrayNo);
        }

        private int[] arrayNo { get; set; }

        private string[] arrayDate { get; set; }

        DateTime[] arrayDateZero { get; set; }

        DateTime[] arrayDateZeroByDay = { DateTime.Now.AddDays(-6).Date, DateTime.Now.AddDays(-5).Date, DateTime.Now.AddDays(-4).Date, DateTime.Now.AddDays(-3).Date, DateTime.Now.AddDays(-2).Date, DateTime.Now.AddDays(-1).Date, DateTime.Now.Date };
        DateTime[] arrayDateZeroByWeek = new DateTime[7];
        DateTime[] arrayDateZeroByMonth = { DateTime.Now.AddMonths(-(DateTime.Now.Month-1)).AddDays(-(DateTime.Now.AddMonths(-(DateTime.Now.Month-1)).Day-1)).Date, DateTime.Now.AddMonths(-(DateTime.Now.Month-2)).AddDays(-(DateTime.Now.AddMonths(-(DateTime.Now.Month-2)).Day-1)).Date,
                                            DateTime.Now.AddMonths(-(DateTime.Now.Month-3)).AddDays(-(DateTime.Now.AddMonths(-(DateTime.Now.Month-3)).Day-1)).Date, DateTime.Now.AddMonths(-(DateTime.Now.Month-4)).AddDays(-(DateTime.Now.AddMonths(-(DateTime.Now.Month-4)).Day-1)).Date,
                                            DateTime.Now.AddMonths(-(DateTime.Now.Month-5)).AddDays(-(DateTime.Now.AddMonths(-(DateTime.Now.Month-5)).Day-1)).Date, DateTime.Now.AddMonths(-(DateTime.Now.Month-6)).AddDays(-(DateTime.Now.AddMonths(-(DateTime.Now.Month-6)).Day-1)).Date,
                                            DateTime.Now.AddMonths(-(DateTime.Now.Month-7)).AddDays(-(DateTime.Now.AddMonths(-(DateTime.Now.Month-7)).Day-1)).Date, DateTime.Now.AddMonths(-(DateTime.Now.Month-8)).AddDays(-(DateTime.Now.AddMonths(-(DateTime.Now.Month-8)).Day-1)).Date,
                                            DateTime.Now.AddMonths(-(DateTime.Now.Month-9)).AddDays(-(DateTime.Now.AddMonths(-(DateTime.Now.Month-9)).Day-1)).Date, DateTime.Now.AddMonths(-(DateTime.Now.Month-10)).AddDays(-(DateTime.Now.AddMonths(-(DateTime.Now.Month-10)).Day-1)).Date,
                                            DateTime.Now.AddMonths(-(DateTime.Now.Month-11)).AddDays(-(DateTime.Now.AddMonths(-(DateTime.Now.Month-11)).Day-1)).Date, DateTime.Now.AddMonths(-(DateTime.Now.Month-12)).AddDays(-(DateTime.Now.AddMonths(-(DateTime.Now.Month-12)).Day-1)).Date };
        DateTime arrayDateLast { get; set; }

        DateTime Dates { get; set; }

        DateTime Datee { get; set; }

        int Flag = 0;
        private void LoadCraftChartData()
        {
            if (DateS.Text == "")
            {
                Dates = DateTime.Now.AddDays(-6).Date;
            }
            else
            {
                Dates = DateTime.Parse(DateS.Text);
            }

            if (DateE.Text == "")
            {
                Datee = DateTime.Now.Date;
            }
            else
            {
                Datee = DateTime.Parse(DateE.Text).AddDays(1);
            }
            DataPicResponse datapicResponse = LocalApi.ExcutePicAlarmA(new DataPicRequest() {datee = Datee , dates = Dates});
            arrayNo = datapicResponse.DataPics.Select(m => m.Count).ToArray();
            arrayDate = datapicResponse.DataPics.Select(m => m.Keyword).ToArray(); 
        }

        private void LoadByData(DateTime Dates, DateTime Datee)
        {
            DataPicResponse datapicResponse = LocalApi.ExcutePicAlarm(new DataPicRequest() { datee = Datee, dates = Dates });
            arrayNo = datapicResponse.DataPics.Select(m => m.Count).ToArray();
            arrayDate = datapicResponse.DataPics.Select(m => m.Keyword).ToArray();
        }
        //柱
        public void CreateChartColumn(string name, string[] valuex, int[] valuey)
        {
            //创建一个图标
            Chart chart = new Chart();

            //设置图标的宽度和高度
            chart.Width = 1130;
            chart.Height = 480;
            chart.Margin = new Thickness(350, 250, 10, 5);
            //是否启用打印和保持图片
            chart.ToolBarEnabled = false;

            //设置图标的属性
            chart.ScrollingEnabled = false;//是否启用或禁用滚动
            chart.View3D = true;//3D效果显示

            //创建一个标题的对象
            Title title = new Title();

            //设置标题的名称
            title.Text = Name;
            title.Padding = new Thickness(0, 10, 5, 0);

            //向图标添加标题
            chart.Titles.Add(title);

            Axis yAxis = new Axis();
            //设置图标中Y轴的最小值永远为0           
            yAxis.AxisMinimum = 0;
            //设置图表中Y轴的后缀          
            yAxis.Suffix = "次";
            chart.AxesY.Add(yAxis);

            // 创建一个新的数据线。               
            DataSeries dataSeries = new DataSeries();

            // 设置数据线的格式
            dataSeries.RenderAs = RenderAs.StackedColumn;//柱状Stacked


            // 设置数据点              
            DataPoint dataPoint;
            for (int i = 0; i < arrayNo.Length; i++)
            {
                // 创建一个数据点的实例。                   
                dataPoint = new DataPoint();
                // 设置X轴点                    
                dataPoint.AxisXLabel = arrayDate[i];
                //设置Y轴点                   
                dataPoint.YValue = double.Parse(arrayNo[i].ToString());
                //添加一个点击事件        
                dataPoint.MouseLeftButtonDown += new MouseButtonEventHandler(dataPoint_MouseLeftButtonDown);
                //添加数据点                   
                dataSeries.DataPoints.Add(dataPoint);
            }

            // 添加数据线到数据序列。                
            chart.Series.Add(dataSeries);

            //将生产的图表增加到Grid，然后通过Grid添加到上层Grid.           
            Grid gr = new Grid();
            gr.Children.Add(chart);
            Simon.Children.Add(gr);
        }
        //饼
        public void CreateChartPie(string name, string[] valuex, int[] valuey)
        {
            //创建一个图标
            Chart chart = new Chart();

            //设置图标的宽度和高度
            chart.Width = 1130;
            chart.Height = 480;
            chart.Margin = new Thickness(350, 250, 10, 5);
            //是否启用打印和保持图片
            chart.ToolBarEnabled = false;

            //设置图标的属性
            chart.ScrollingEnabled = false;//是否启用或禁用滚动
            chart.View3D = true;//3D效果显示

            //创建一个标题的对象
            Title title = new Title();

            //设置标题的名称
            title.Text = name;
            title.Padding = new Thickness(0, 10, 5, 0);

            //向图标添加标题
            chart.Titles.Add(title);

            //Axis yAxis = new Axis();
            ////设置图标中Y轴的最小值永远为0           
            //yAxis.AxisMinimum = 0;
            ////设置图表中Y轴的后缀          
            //yAxis.Suffix = "斤";
            //chart.AxesY.Add(yAxis);

            // 创建一个新的数据线。               
            DataSeries dataSeries = new DataSeries();

            // 设置数据线的格式
            dataSeries.RenderAs = RenderAs.Pie;//柱状Stacked


            // 设置数据点              
            DataPoint dataPoint;
            for (int i = 0; i < arrayNo.Length; i++)
            {
                // 创建一个数据点的实例。                   
                dataPoint = new DataPoint();
                // 设置X轴点                    
                dataPoint.AxisXLabel = arrayDate[i];

                dataPoint.LegendText = "##" + arrayDate[i];
                //设置Y轴点                   
                dataPoint.YValue = double.Parse(arrayNo[i].ToString());
                //添加一个点击事件        
                dataPoint.MouseLeftButtonDown += new MouseButtonEventHandler(dataPoint_MouseLeftButtonDown);
                //添加数据点                   
                dataSeries.DataPoints.Add(dataPoint);
            }

            // 添加数据线到数据序列。                
            chart.Series.Add(dataSeries);

            //将生产的图表增加到Grid，然后通过Grid添加到上层Grid.           
            Grid gr = new Grid();
            gr.Children.Add(chart);
            Simon.Children.Add(gr);
        }


        //点击事件
        void dataPoint_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
        }

        private void BtnMonth_Click(object sender, RoutedEventArgs e)
        {
            DateTime[] arrayDateOfYear =  DateOfYear();
            LoadByData(arrayDateOfYear[0], arrayDateOfYear[1]);
            if (Flag == 0)
            {
                CreateChartColumn("报警统计", arrayDate, arrayNo);
            }
            else
            {
                CreateChartPie("报警统计", arrayDate, arrayNo);
            }

        }

        private void BtnWeek_Click(object sender, RoutedEventArgs e)
        {
            string DayOfWeek = DateTime.Now.DayOfWeek.ToString();
            int count = DayOfWeekend(DayOfWeek);
            for (int i = 0; i < 7; i++)
            {
                arrayDateZeroByWeek[i] = DateTime.Now.Date.AddDays(-(count - (i + 1)));
            }
            LoadByData(DateTime.Now.Date.AddDays(-(count - 1)), DateTime.Now.Date.AddDays(7 - count));
            if (Flag == 0)
            {
                CreateChartColumn("报警统计", arrayDate, arrayNo);
            }
            else
            {
                CreateChartPie("报警统计", arrayDate, arrayNo);
            }
        }

        private void BtnDay_Click(object sender, RoutedEventArgs e)
        {
            DateTime time = ByDay();
            LoadByData(time.Date, DateTime.Now.Date);
            if (Flag == 0)
            {
                CreateChartColumn("报警统计", arrayDate, arrayNo);
            }
            else
            {
                CreateChartPie("报警统计", arrayDate, arrayNo);
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            LoadCraftChartData();
            if (Flag == 0)
            {
                CreateChartColumn("报警统计", arrayDate, arrayNo);
            }
            else
            {
                CreateChartPie("报警统计", arrayDate, arrayNo);
            }
        }

        //竖
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            LoadCraftChartData();
            CreateChartColumn("报警统计", arrayDate, arrayNo);
            Flag = 0;
        }

        //饼
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            LoadCraftChartData();
            CreateChartPie("报警统计", arrayDate, arrayNo);
            Flag = 1;
        }

        private int DayOfWeekend(string day)
        {
            int index = 0;
            switch (day)
            {
                case "Monday":
                    index = 1;
                    break;
                case "Tuesday":
                    index = 2;
                    break;
                case "Wednesday":
                    index = 3;
                    break;
                case "Thursday":
                    index = 4;
                    break;
                case "Friday":
                    index = 5;
                    break;
                case "Saturday":
                    index = 6;
                    break;
                case "Sunday":
                    index = 7;
                    break;
            }
            return index;
        }

        private int DayCount(int month)
        {
            int count;
            if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
            {
                count = 31;
            }
            else if (month == 2)
            {
                count = 28;
            }
            else
            {
                count = 30;
            }
            return count;
        }

        private DateTime ByDay()
        {
            DateTime time = DateTime.Now.Date;
            int count = 0;
            int a = DateTime.Now.Day - 7;
            if (a < 0)
            {
                time = DateTime.Now.AddMonths(-1);
                time = time.AddDays(-(DateTime.Now.Day - 1));
                count = DayCount(time.Month);
                time = time.AddDays(count - (7 - DateTime.Now.Day));
            }
            else
            {
                time = DateTime.Now.Date.AddDays(-7);
            }
            return time;
        }
        private DateTime[] DateOfYear()
        {
            DateTime[] arrayDay = new DateTime[2];
            arrayDay[0] = DateTime.Now.AddDays(-DateTime.Now.DayOfYear + 1).Date;
            bool isLeapYear = DateTime.IsLeapYear(DateTime.Now.Year);
            if (isLeapYear)
            {
                arrayDay[1] = DateTime.Now.AddDays(-DateTime.Now.DayOfYear + 366).Date;
            }
            else
            {
                arrayDay[1] = DateTime.Now.AddDays(-DateTime.Now.DayOfYear + 365).Date;
            }
            return arrayDay;
        }
    }
}
