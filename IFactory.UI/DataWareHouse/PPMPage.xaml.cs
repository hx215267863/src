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
    public partial class PPM : BasePage, IComponentConnector
    {
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeComponent();
            Simon.Children.Clear();
            string a = DateTime.Now.DayOfWeek.ToString();
            LoadCraftChartData();
            CreateChartSpline("PPM曲线", arrayDate, arrayNo,"day");
        }

        private int[] arrayNo { get; set; }

        private DateTime[] arrayDate { get; set; }

        DateTime[] arrayDateZero { get; set; }
        DateTime[] arrayDateZeroLast { get; set; }
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
                Datee = DateTime.Parse(DateE.Text);
            }
            DataPicResponse datapicResponse = LocalApi.ExcutePicPPM(new DataPicRequest() { datee = Datee, dates = Dates });
            arrayNo = datapicResponse.DataPics.Select(m => m.PPM).ToArray();
            arrayDate = datapicResponse.DataPics.Select(m => m.ProductTime).ToArray();
        }

        private void LoadByData(DateTime Dates, DateTime Datee)
        {
            DataPicResponse datapicResponse = LocalApi.ExcutePicPPM(new DataPicRequest() { datee = Datee, dates = Dates });
            arrayNo = datapicResponse.DataPics.Select(m => m.PPM).ToArray();
            arrayDate = datapicResponse.DataPics.Select(m => m.ProductTime).ToArray();
        }

        public void CreateChartSpline(string name, DateTime[] lsTime, int[] cherry,string by)
        {
            int j = 0;
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

            //初始化一个新的Axis
            Axis xaxis = new Axis();
            //设置Axis的属性
            //图表的X轴坐标按什么来分类，如时分秒
            xaxis.IntervalType = IntervalTypes.Days;
            //图表的X轴坐标间隔如2,3,20等，单位为xAxis.IntervalType设置的时分秒。
            xaxis.Interval = 1;
            //设置X轴的时间显示格式为7-10 11：20           
            xaxis.ValueFormatString = "MM月dd日";
            //给图标添加Axis            
            chart.AxesX.Add(xaxis);

            Axis yAxis = new Axis();
            //设置图标中Y轴的最小值永远为0           
            yAxis.AxisMinimum = 0;
            //设置图表中Y轴的后缀          
            yAxis.Suffix = "";
            chart.AxesY.Add(yAxis);


            // 创建一个新的数据线。               
            DataSeries dataSeries = new DataSeries();
            // 设置数据线的格式。               
            dataSeries.LegendText = "编号";

            dataSeries.RenderAs = RenderAs.Line;//折线图

            dataSeries.XValueType = ChartValueTypes.DateTime;
            // 设置数据点              
            DataPoint dataPoint;

            if (arrayDate.Length == 0)
            {
                dataPoint = new DataPoint();
                for (int i = 0; i < 7; i++)
                {
                    dataPoint = new DataPoint();
                    SetZero(dataPoint, dataSeries, i, by);
                }
            }
            else
            {
                arrayDateZero = XValueOfZero(by);
                while (DateTime.Compare(arrayDate[0].Date,arrayDateZero[j])>0)
                {
                    dataPoint = new DataPoint();
                    SetZero(dataPoint, dataSeries, j, by);
                    j++;
                    if (j == 7)
                        break;
                }
                for (int i = 0; i < arrayDate.Length; i++)
                {
                    if (i < arrayDate.Length-1)
                    {
                        if (arrayDate[i].Day != arrayDate[i + 1].Day)
                        {
                            dataPoint = new DataPoint();
                            SetPoint(dataPoint, dataSeries, i);
                        }
                    }
                    while (DateTime.Compare(arrayDate[i].Date, arrayDateZero[j]) > 0)
                    {
                        j++;
                        if (j == 7)
                            break;
                        if (DateTime.Compare(arrayDate[i].Date, arrayDateZero[j]) > 0)
                        {
                            dataPoint = new DataPoint();
                            SetZero(dataPoint, dataSeries, j, by);
                        }
                    }
                    if (i == arrayDate.Length - 1 && i != 0)
                    {
                        dataPoint = new DataPoint();
                        SetPoint(dataPoint, dataSeries, i);
                    }
                   
                }
                for(int k = 0;k<7;k++)
                {
                    if(DateTime.Compare(arrayDate[arrayDate.Length - 1].Date, arrayDateZero[k]) < 0)
                    {
                        dataPoint = new DataPoint();
                        SetZero(dataPoint, dataSeries, k, by);
                    }
                }
            }

            // 添加数据线到数据序列。                
            chart.Series.Add(dataSeries);

            //将生产的图表增加到Grid，然后通过Grid添加到上层Grid.           
            Grid gr = new Grid();
            gr.Children.Add(chart);

            Simon.Children.Add(gr);
        }


        public void CreateChartSpline_Month(string name, DateTime[] lsTime, int[] cherry)
        {
            int arrayTotal = arrayNo[0];
            int j = 0;
            int i = 0;
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

            //初始化一个新的Axis
            Axis xaxis = new Axis();
            //设置Axis的属性
            //图表的X轴坐标按什么来分类，如时分秒
            xaxis.IntervalType = IntervalTypes.Months;
            //图表的X轴坐标间隔如2,3,20等，单位为xAxis.IntervalType设置的时分秒。
            xaxis.Interval = 1;
            //设置X轴的时间显示格式为7-10 11：20           
            xaxis.ValueFormatString = "MM月";
            //给图标添加Axis            
            chart.AxesX.Add(xaxis);

            Axis yAxis = new Axis();
            //设置图标中Y轴的最小值永远为0           
            yAxis.AxisMinimum = 0;
            //设置图表中Y轴的后缀          
            yAxis.Suffix = "";

            chart.AxesY.Add(yAxis);


            // 创建一个新的数据线。               
            DataSeries dataSeries = new DataSeries();
            // 设置数据线的格式。               
            dataSeries.LegendText = "编号";

            dataSeries.RenderAs = RenderAs.Line;//折线图

            dataSeries.XValueType = ChartValueTypes.DateTime;
            // 设置数据点              
            DataPoint dataPoint;

            if (arrayDate.Length == 0)
            {
                dataPoint = new DataPoint();
                for (int k = 0; k < 13; k++)
                {
                    dataPoint = new DataPoint();
                    SetZero(dataPoint, dataSeries, k,"month");
                }
            }
            else
            {
                while (arrayDate[0].Month> arrayDateZeroByMonth[j].Month)
                {
                    dataPoint = new DataPoint();
                    SetZero(dataPoint, dataSeries, j, "month");
                    j++;
                    if (j == 12)
                        break;
                }
                for (; i < arrayNo.Length; i++)
                {
                    if(i < arrayNo.Length - 1)
                    {
                        if (arrayDate[i].Month == arrayDate[i + 1].Month)
                        {
                            arrayTotal = arrayTotal + arrayNo[i + 1];  
                        }
                        else
                        {
                            dataPoint = new DataPoint();
                            SetPoint_Month(dataPoint, dataSeries, j, arrayTotal);
                            arrayTotal = arrayNo[i+1];
                            j++;
                        }
                        while (arrayDate[i + 1].Month > arrayDateZeroByMonth[j].Month)
                        {
                            dataPoint = new DataPoint();
                            SetZero(dataPoint, dataSeries, j, "month");
                            j++;
                            if (j == 11)
                                break;
                        }
                    }
                    else if (i == arrayDate.Length - 1)
                    {
                        dataPoint = new DataPoint();
                        SetPoint_Month(dataPoint, dataSeries, j, arrayTotal);
                        if(j<11)
                        {
                            j++;
                        }
                    }
                   
                }
                while (arrayDate[arrayDate.Length - 1].Month < arrayDateZeroByMonth[j].Month)
                {
                    dataPoint = new DataPoint();
                    SetZero(dataPoint, dataSeries, j, "month");
                    j++;
                    if (j == 12)
                        break;
                }
            }

            // 添加数据线到数据序列。                
            chart.Series.Add(dataSeries);

            //将生产的图表增加到Grid，然后通过Grid添加到上层Grid.           
            Grid gr = new Grid();
            gr.Children.Add(chart);

            Simon.Children.Add(gr);
        }

        private void SetZero(DataPoint dataPoint, DataSeries dataSeries, int i,string by)
        {
            arrayDateZero = XValueOfZero(by);
            // 设置X轴点
            dataPoint.XValue = arrayDateZero[i];            
            //设置Y轴点
            dataPoint.YValue = 0;
            dataPoint.MarkerSize = 8;
            //dataPoint.Tag = tableName.Split('(')[0];
            //设置数据点颜色
            // dataPoint.Color = new SolidColorBrush(Colors.LightGray);
            dataPoint.MouseLeftButtonDown += new MouseButtonEventHandler(dataPoint_MouseLeftButtonDown);
            //添加数据点
            dataSeries.DataPoints.Add(dataPoint);
        }
        private void SetZeroRandom(DataPoint dataPoint, DataSeries dataSeries, int i,DateTime[] arrayRandom)
        {
            // 设置X轴点
            dataPoint.XValue = arrayRandom[i];
            //设置Y轴点
            dataPoint.YValue = 0;
            dataPoint.MarkerSize = 6;
            //dataPoint.Tag = tableName.Split('(')[0];
            //设置数据点颜色
            // dataPoint.Color = new SolidColorBrush(Colors.LightGray);
            dataPoint.MouseLeftButtonDown += new MouseButtonEventHandler(dataPoint_MouseLeftButtonDown);
            //添加数据点
            dataSeries.DataPoints.Add(dataPoint);
        }
        //点击事件
        void dataPoint_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //DataPoint dp = sender as DataPoint;
            //MessageBox.Show(dp.YValue.ToString());
        }

        private void SetPoint(DataPoint dataPoint, DataSeries dataSeries, int i)
        {
            // 设置X轴点                    
            dataPoint.XValue = arrayDate[i].Date;
            //设置Y轴点
            dataPoint.YValue = arrayNo[i];
            dataPoint.MarkerSize = 6;
            //dataPoint.Tag = tableName.Split('(')[0];
            //设置数据点颜色
            // dataPoint.Color = new SolidColorBrush(Colors.LightGray);
            dataPoint.MouseLeftButtonDown += new MouseButtonEventHandler(dataPoint_MouseLeftButtonDown);
            //添加数据点
            dataSeries.DataPoints.Add(dataPoint);
        }

        private void SetPoint_Month(DataPoint dataPoint, DataSeries dataSeries, int i,int yValue)
        {
            // 设置X轴点                    
            dataPoint.XValue = arrayDateZeroByMonth[i].Date;
            //设置Y轴点
            dataPoint.YValue = yValue;
            dataPoint.MarkerSize = 6;
            //dataPoint.Tag = tableName.Split('(')[0];
            //设置数据点颜色
            // dataPoint.Color = new SolidColorBrush(Colors.LightGray);
            dataPoint.MouseLeftButtonDown += new MouseButtonEventHandler(dataPoint_MouseLeftButtonDown);
            //添加数据点
            dataSeries.DataPoints.Add(dataPoint);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            LoadCraftChartData();
            CreateChartSplineRandom("PPM曲线", arrayDate, arrayNo);
        }

        private void BtnDay_Click(object sender, RoutedEventArgs e)
        {
            DateTime time = ByDay();
            LoadByData(time.Date, DateTime.Now.Date);
            CreateChartSpline("PPM曲线", arrayDate, arrayNo,"day");
        }

        private void BtnWeek_Click(object sender, RoutedEventArgs e)
        {
            string DayOfWeek = DateTime.Now.DayOfWeek.ToString();
            int count = DayOfWeekend(DayOfWeek);
            for(int i = 0;i<7;i++)
            {
                arrayDateZeroByWeek[i] = DateTime.Now.Date.AddDays(-(count - (i+1)));
            }
            LoadByData(DateTime.Now.Date.AddDays(-(count-1)), DateTime.Now.Date.AddDays(7-count));
            CreateChartSpline("PPM曲线", arrayDate, arrayNo,"week");
        }

        private void BtnMonth_Click(object sender, RoutedEventArgs e)
        {
            DateTime[] arrayDateOfYear = DateOfYear();
            LoadByData(arrayDateOfYear[0], arrayDateOfYear[1]);
            CreateChartSpline_Month("PPM曲线", arrayDate, arrayNo);
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
            if(month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 ||month == 12)
            {
                count = 31;
            }
            else if(month == 2)
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
        
        private DateTime[] XValueOfZero(string by)
        {
            DateTime[] arrayZero = { };
            switch(by)
            {
                case "day":
                    arrayZero = arrayDateZeroByDay;
                    break;
                case "week":
                    arrayZero = arrayDateZeroByWeek;
                    break;
                case "month":
                    arrayZero = arrayDateZeroByMonth;
                    break;
            }
            return arrayZero;
        }

        public void CreateChartSplineRandom(string name, DateTime[] lsTime, int[] cherry)
        {
            if (DateS.Text == "")
            {
                Dates = DateTime.Now.AddDays(-6);
            }
            else
            {
                Dates = DateTime.Parse(DateS.Text);
            }

            if (DateE.Text == "")
            {
                Datee = DateTime.Now;
            }
            else
            {
                Datee = DateTime.Parse(DateE.Text);
            }

            DateTime[] randomDate = new DateTime[12];
            int idistance = Datee.Date.Day - Dates.Date.Day;
            for(int i = 0;i<idistance+1;i++)
            {
                randomDate[i] = Dates.Date.AddDays(i);
            }

            int j = 0;
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

            //初始化一个新的Axis
            Axis xaxis = new Axis();
            //设置Axis的属性
            //图表的X轴坐标按什么来分类，如时分秒
            xaxis.IntervalType = IntervalTypes.Days;
            //图表的X轴坐标间隔如2,3,20等，单位为xAxis.IntervalType设置的时分秒。
            xaxis.Interval = 1;
            //设置X轴的时间显示格式为7-10 11：20           
            xaxis.ValueFormatString = "MM月dd日";
            //给图标添加Axis            
            chart.AxesX.Add(xaxis);

            Axis yAxis = new Axis();
            //设置图标中Y轴的最小值永远为0           
            yAxis.AxisMinimum = 0;
            //设置图表中Y轴的后缀          
            yAxis.Suffix = "";
            chart.AxesY.Add(yAxis);


            // 创建一个新的数据线。               
            DataSeries dataSeries = new DataSeries();
            // 设置数据线的格式。               
            dataSeries.LegendText = "编号";

            dataSeries.RenderAs = RenderAs.Line;//折线图

            dataSeries.XValueType = ChartValueTypes.DateTime;
            // 设置数据点              
            DataPoint dataPoint;

            if (arrayDate.Length == 0)
            {
                dataPoint = new DataPoint();
                for (int i = 0; i < idistance+1; i++)
                {
                    dataPoint = new DataPoint();
                    SetZeroRandom(dataPoint, dataSeries, i, randomDate);
                }
            }
            else
            {
                while (DateTime.Compare(arrayDate[0].Date, randomDate[j]) > 0)
                {
                    dataPoint = new DataPoint();
                    SetZeroRandom(dataPoint, dataSeries, j, randomDate);
                    j++;
                    if (j == idistance+1)
                        break;
                }
                for (int i = 0; i < arrayDate.Length; i++)
                {
                    if (i < arrayDate.Length - 1)
                    {
                        if (arrayDate[i].Day != arrayDate[i + 1].Day)
                        {
                            dataPoint = new DataPoint();
                            SetPoint(dataPoint, dataSeries, i);
                        }
                    }
                    while (DateTime.Compare(arrayDate[i].Date, randomDate[j]) > 0)
                    {
                        j++;
                        if (j == idistance+1)
                            break;
                        if (DateTime.Compare(arrayDate[i].Date, randomDate[j]) > 0)
                        {
                            dataPoint = new DataPoint();
                            SetZeroRandom(dataPoint, dataSeries, j, randomDate);
                        }
                    }
                    if (i == arrayDate.Length - 1 && i != 0)
                    {
                        dataPoint = new DataPoint();
                        SetPoint(dataPoint, dataSeries, i);
                    }

                }
                for (int k = 0; k < idistance+1; k++)
                {
                    if (DateTime.Compare(arrayDate[arrayDate.Length - 1].Date, randomDate[k]) < 0)
                    {
                        dataPoint = new DataPoint();
                        SetZeroRandom(dataPoint, dataSeries, k, randomDate);
                    }
                }
            }

            // 添加数据线到数据序列。                
            chart.Series.Add(dataSeries);

            //将生产的图表增加到Grid，然后通过Grid添加到上层Grid.           
            Grid gr = new Grid();
            gr.Children.Add(chart);

            Simon.Children.Add(gr);
        }

        private DateTime[] DateOfYear()
        {
            DateTime[] arrayDay = new DateTime[2];
            arrayDay[0] = DateTime.Now.AddDays(-DateTime.Now.DayOfYear + 1).Date;
            bool isLeapYear = DateTime.IsLeapYear(DateTime.Now.Year);
            if(isLeapYear)
            {
                arrayDay[1] = DateTime.Now.AddDays(-DateTime.Now.DayOfYear + 367).Date;
            }
            else
            {
                arrayDay[1] = DateTime.Now.AddDays(-DateTime.Now.DayOfYear + 366).Date;
            }
            return arrayDay;
        }
    }
}
