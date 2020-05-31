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
    public partial class DataNGType2 : BasePage, IComponentConnector
    {
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeComponent();
            Simon.Children.Clear();
            LoadCraftChartData();
            //CreateChartSpline("报警统计", arrayDate, arrayNo);
        }

        private int[] arrayNo { get; set; }

        private string[] arrayDate { get; set; }

        DateTime arrayDateLast { get; set; }

        DateTime Dates { get; set; }

        DateTime Datee { get; set; }

        int Flag = 0;
        private void LoadCraftChartData()
        {
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
            LoadByData(DateTime.Now.Date.AddDays((-DateTime.Now.Day) + 1), DateTime.Now.Date);
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
            LoadByData(DateTime.Now.Date.AddDays(-6), DateTime.Now.Date);
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
            LoadByData(DateTime.Now.Date, DateTime.Now.Date);
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

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            LoadCraftChartData();
            CreateChartColumn("报警统计", arrayDate, arrayNo);
            Flag = 0;
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            LoadCraftChartData();
            CreateChartPie("报警统计", arrayDate, arrayNo);
            Flag = 1;
        }

    }
}
