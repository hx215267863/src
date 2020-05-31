using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using Visifire.Charts;
using IFactory.UI.Controls;
using System.Windows.Markup;
using IFactory.Platform.Common.Response.Crafts;
using IFactory.Platform.Common.Request.Crafts;


namespace IFactory.UI.DataWareHouse
{
    /// <summary>
    /// Page1.xaml 的交互逻辑
    /// </summary>
    public partial class DataProduction : BasePage, IComponentConnector
    {
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeComponent();
            Simon.Children.Clear();
            CreateChartSpline("过程数据曲线", arrayDate, arrayNo);
        }
        private int[] arrayNo { get; set; }

        private DateTime[] arrayDate { get; set; }

        private void LoadCraftChartData()
        {
            DataPicResponse datapicResponse = LocalApi.ExcutePicCapacity(new DataPicRequest() { });
            arrayNo = datapicResponse.DataPics.Select(m => m.Iden).ToArray();
            arrayDate = datapicResponse.DataPics.Select(m => m.ProductTime).ToArray();
        }

        #region 折线图
        public void CreateChartSpline(string name, DateTime[] lsTime, int[] cherry)
        {

            LoadCraftChartData();
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
            yAxis.Suffix = "%";
            chart.AxesY.Add(yAxis);


            // 创建一个新的数据线。               
            DataSeries dataSeries = new DataSeries();
            // 设置数据线的格式。               
            dataSeries.LegendText = "编号";

            dataSeries.RenderAs = RenderAs.Line;//折线图

            dataSeries.XValueType = ChartValueTypes.DateTime;
            // 设置数据点              
            DataPoint dataPoint;
            for (int i = 0; i < arrayDate.Length; i++)
            {
                if(i == 0)
                {
                    // 创建一个数据点的实例。                   
                    dataPoint = new DataPoint();
                    // 设置X轴点                    
                    dataPoint.XValue = arrayDate[i];
                    //设置Y轴点                   
                    dataPoint.YValue = arrayNo[i];
                    dataPoint.MarkerSize = 8;
                    //dataPoint.Tag = tableName.Split('(')[0];
                    //设置数据点颜色                  
                    // dataPoint.Color = new SolidColorBrush(Colors.LightGray);                   
                    dataPoint.MouseLeftButtonDown += new MouseButtonEventHandler(dataPoint_MouseLeftButtonDown);
                    //添加数据点                   
                    dataSeries.DataPoints.Add(dataPoint);
                }
                if (i < arrayDate.Length - 1 && i > 0)
                {
                    if (arrayDate[i].Day != arrayDate[i - 1].Day )
                    {
                        // 创建一个数据点的实例。                   
                        dataPoint = new DataPoint();
                        // 设置X轴点                    
                        dataPoint.XValue = arrayDate[i];
                        //设置Y轴点                   
                        dataPoint.YValue = arrayNo[i];
                        dataPoint.MarkerSize = 8;
                        //dataPoint.Tag = tableName.Split('(')[0];
                        //设置数据点颜色                  
                        // dataPoint.Color = new SolidColorBrush(Colors.LightGray);                   
                        dataPoint.MouseLeftButtonDown += new MouseButtonEventHandler(dataPoint_MouseLeftButtonDown);
                        //添加数据点                   
                        dataSeries.DataPoints.Add(dataPoint);
                    }
                }
                if(i == arrayDate.Length - 1)
                {
                    if (arrayDate[i].Day != arrayDate[ i - 1].Day)
                    {
                        // 创建一个数据点的实例。                   
                        dataPoint = new DataPoint();
                        // 设置X轴点                    
                        dataPoint.XValue = arrayDate[i];
                        //设置Y轴点                   
                        dataPoint.YValue = arrayNo[i];
                        dataPoint.MarkerSize = 8;
                        //dataPoint.Tag = tableName.Split('(')[0];
                        //设置数据点颜色                  
                        // dataPoint.Color = new SolidColorBrush(Colors.LightGray);                   
                        dataPoint.MouseLeftButtonDown += new MouseButtonEventHandler(dataPoint_MouseLeftButtonDown);
                        //添加数据点                   
                        dataSeries.DataPoints.Add(dataPoint);
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
        #endregion

        #region 点击事件
        //点击事件
        void dataPoint_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //DataPoint dp = sender as DataPoint;
            //MessageBox.Show(dp.YValue.ToString());
        }
        #endregion

        private void btnSearchChart_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
