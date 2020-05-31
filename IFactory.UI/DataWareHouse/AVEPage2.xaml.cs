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
using IFactory.Domain.Models;
using IFactory.Platform.Common.Response.User;
using IFactory.Platform.Common.Request.User;

namespace IFactory.UI.DataWareHouse
{
    /// <summary>
    /// Page1.xaml 的交互逻辑
    /// </summary>
    public partial class AVEPage2 : BasePage, IComponentConnector
    {
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeComponent();
            Simon.Children.Clear();
            CreateChartSpline("尺寸平均值", arrayDate, arrayNo);
        }

        private Double[] arrayNo { get; set; }

        private DateTime[] arrayDate { get; set; }

        public DateTime[] arrayDatePoint { get; set; }

        private Double[] arrayAve { get; set; }

        private string[] arrayType { get; set; }

        DateTime[] arrayDateZeroM = { DateTime.Now.Date.AddHours(8),DateTime.Now.Date.AddHours(9), DateTime.Now.Date.AddHours(10), DateTime.Now.Date.AddHours(11), DateTime.Now.Date.AddHours(12), DateTime.Now.Date.AddHours(13), DateTime.Now.Date.AddHours(14), DateTime.Now.Date.AddHours(15), DateTime.Now.Date.AddHours(16), DateTime.Now.Date.AddHours(17), DateTime.Now.Date.AddHours(18), DateTime.Now.Date.AddHours(19), DateTime.Now.Date.AddHours(20) };
        DateTime[] arrayDateZeroE = { DateTime.Now.Date.AddHours(20),DateTime.Now.Date.AddHours(21), DateTime.Now.Date.AddHours(22), DateTime.Now.Date.AddHours(23), DateTime.Now.Date.AddHours(24), DateTime.Now.Date.AddDays(1).AddHours(1), DateTime.Now.Date.AddDays(1).AddHours(2), DateTime.Now.Date.AddDays(1).AddHours(3), DateTime.Now.Date.AddDays(1).AddHours(4), DateTime.Now.Date.AddDays(1).AddHours(5), DateTime.Now.Date.AddDays(1).AddHours(6), DateTime.Now.Date.AddDays(1).AddHours(7), DateTime.Now.Date.AddDays(1).AddHours(8) };

        DateTime arrayDateLast { get; set; }

        private DateTime[] arrayTime { get; set; }

        Double arrayTotal { get; set; }

        string time;
        //string timec;

        //DateTime Dates;
        DateTime Datee;

        public UserModel model;
        private void LoadCraftChartData()
        {

            string result = ddlType.Text;
            string type = ddlType.Text;
            time = ddlTime.Text;
            //timec = ddlTimes.Text;

            if (Date.Text != "")
            {
                Datee = DateTime.Parse(Date.Text);
            }
            else
            {
                //Dates = DateTime.Now;
                Datee = DateTime.Now.Date;
            }
            if(time == "")
            {
                time = "早";
            }
            this.model = new UserModel();
            this.DataContext = this.model;
            result = ChangeLanguage(ddlSize.Text,result);
            UserSaveResponse userSaveResponse = LocalApi.ExecuteAve(
                       new UserSaveRequest() { });
            AVEResponse aveResponse = LocalApi.ExcuteAVE2(new AVERequest() { side = result,datee = Datee,type = type});
            AVEResponse aveResponses = LocalApi.ExcuteType(new AVERequest() { datee = Datee });
            this.ddlSize.ItemsSource = SizeMeas2.MainBody_Width_Top.ToArrayList();
            this.ddlTime.ItemsSource = TimeC.Mornings.ToArrayList();
            //this.ddlTimes.ItemsSource = TimeS.Mins.ToArrayList();
            arrayNo = aveResponse.Aves.Select(m => m.Size).ToArray();
            arrayDate = aveResponse.Aves.Select(m => m.TimeStart).ToArray();
            arrayType = aveResponses.Aves.Select(n => n.Type).ToArray();
            this.ddlType.Items.Clear();
            //this.ddlType.DisplayMemberPath = arrayType[0];
            for (int a = 0; a < arrayType.Length; a ++)
            {       
                this.ddlType.Items.Add(arrayType[a]);
                if(arrayType[a] == type)
                {
                    this.ddlType.IsEditable = true;
                    this.ddlType.Text = type;
                }
            }
            //SetArrayDateLast(time,0);
        }

        public void CreateChartSpline(string name, DateTime[] lsTime, Double[] cherry)
        {
            int j = 0;
            int k = 0;
            arrayTotal = 0;
            DateTime[] arrayDatePoint = new DateTime[102400];
            Double[] arrayAve = new Double[102400];
            arrayAve.Initialize();
            DateTime[] arrayDateMnE = MorE(time);
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
            //xaxis.IntervalType = IntervalTypes.Days;
            //图表的X轴坐标按什么来分类，如时分秒
            xaxis.IntervalType = IntervalTypes.Hours;
            //图表的X轴坐标间隔如2,3,20等，单位为xAxis.IntervalType设置的时分秒。
            xaxis.Interval = 1;
            //设置X轴的时间显示格式为7-10 11：20           
            xaxis.ValueFormatString = "MM月dd日 HH:mm";
            //给图标添加Axis            
            chart.AxesX.Add(xaxis);

            Axis yAxis = new Axis();
            //设置图标中Y轴的最小值永远为0           
            yAxis.AxisMinimum = 0;
            //设置图表中Y轴的后缀          
            yAxis.Suffix = "mm";
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
                for (int i = 1; i < 13; i++)
                {
                    dataPoint = new DataPoint();
                    SetZero(dataPoint, dataSeries, i);
                }
            }
            else
            {
                arrayDateLast = arrayDateMnE[0];
                int num = 1;
                while (DateTime.Compare(arrayDate[0], arrayDateLast) > 0)
                {
                    //SetArrayDateLast("天", 0);
                    arrayDateLast = arrayDateLast.AddHours(1);
                    if(DateTime.Compare(arrayDate[0], arrayDateLast) > 0)
                    {
                        dataPoint = new DataPoint();
                        SetZero(dataPoint, dataSeries, num);
                    }
                    num++;
                }
                for (int i = 0; i < arrayDate.Length; i++)
                {
                    if (i <= arrayDate.Length - 1)
                    {
                        if (DateTime.Compare(arrayDate[i], arrayDateLast) > 0)
                        {
                            arrayDatePoint[k] = arrayDateLast;
                            if(j != 0)
                            {
                                arrayAve[k] = arrayTotal / j;
                            }
                            else
                            {
                                arrayAve[k] = 0;
                            }
                            // 创建一个数据点的实例。                   
                            dataPoint = new DataPoint();
                            // 设置X轴点                    
                            dataPoint.XValue = arrayDatePoint[k];
                            //设置Y轴点
                            dataPoint.YValue = arrayAve[k];
                            dataPoint.MarkerSize = 8;
                            //dataPoint.Tag = tableName.Split('(')[0];
                            //设置数据点颜色
                            // dataPoint.Color = new SolidColorBrush(Colors.LightGray);
                            dataPoint.MouseLeftButtonDown += new MouseButtonEventHandler(dataPoint_MouseLeftButtonDown);
                            //添加数据点
                            dataSeries.DataPoints.Add(dataPoint);
                            k++;
                            arrayTotal = 0;
                            j = 0;
                            while (DateTime.Compare(arrayDate[i], arrayDateLast) > 0)
                            {
                                arrayDateLast = arrayDateLast.AddHours(1);
                                if(DateTime.Compare(arrayDate[i], arrayDateLast) > 0)
                                {
                                    dataPoint = new DataPoint();
                                    SetZero(dataPoint, dataSeries, num);
                                }
                                num++;
                                i--;
                            }
                            
                        }
                        else
                        {
                            j++;
                            arrayTotal += arrayNo[i];
                            if(i == (arrayDate.Length-1))
                            {
                                arrayAve[k] = arrayTotal / j;
                                arrayDatePoint[k] = arrayDateLast;
                                // 创建一个数据点的实例。                   
                                dataPoint = new DataPoint();
                                // 设置X轴点                    
                                dataPoint.XValue = arrayDatePoint[k];
                                //设置Y轴点
                                dataPoint.YValue = arrayAve[k];
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
                }
                k = 0;
                for (int i = 1;i<13;i++)
                {
                    if (DateTime.Compare(arrayDate[(arrayDate.Length - 1)],arrayDateMnE[i].AddHours(-1))<0)
                    {
                        dataPoint = new DataPoint();
                        SetZero(dataPoint, dataSeries, i);
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

        //点击事件
        void dataPoint_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //DataPoint dp = sender as DataPoint;
            //MessageBox.Show(dp.YValue.ToString());
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            CreateChartSpline("尺寸平均值", arrayDate, arrayNo);
        }

        
        private void SetArrayDateLast(string time,int i)
        {
            switch (time) {
                case "分":
                    arrayDateLast = arrayDateLast.AddMinutes(10);
                    break;
                case "时":
                    arrayDateLast = arrayDateLast.AddHours(1);
                    break;
                case "天":
                    arrayDateLast = arrayDateLast.AddDays(1);
                    break;
            }     
        }

        private void SetZero(DataPoint dataPoint, DataSeries dataSeries, int i)
        {
            // 设置X轴点     
            if(time == "早")
            {
                dataPoint.XValue = arrayDateZeroM[i];
            }
            else
            {
                dataPoint.XValue = arrayDateZeroE[i];
            }
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

        private string ChangeLanguage(string eng,string result)
        {
            switch(eng)
            {
                case "":
                    result = "MainBody_Width_Top";
                    break;
                case "主体顶部宽度":
                    result = "MainBody_Width_Top";
                    break;
                case "主体底部宽度":
                    result = "MainBody_Width_Bottom";
                    break;
                case "主体高度":
                    result = "MainBody_Height";
                    break;
                case "顶封高度":
                    result = "Topseal_Height";
                    break;
                case "Tab1与左气袋的距离":
                    result = "Distance_Between_Tab1_and_left_edge";
                    break;
                case "Tab2与左气袋的距离":
                    result = "Distance_Between_Tab2_and_left_edge";
                    break;
                case "Tab之间的距离":
                    result = "Distance_Between_Tabs";
                    break;
                case "左折边顶部高度":
                    result = "Side_LeftFoldingHight_Top";
                    break;
                case "左折边底部高度":
                    result = "Side_LeftFoldingHight_Botton";
                    break;
                case "右折边顶部高度":
                    result = "Side_RightFoldingHight_Top";
                    break;
                case "右折边底部高度":
                    result = "Side_RightFoldingHight_Botton";
                    break;
            }
            return result;
        }

        private DateTime[] MorE(string time)
        {
            DateTime[] arrayDateZeroMnE = { };
            if(time == "早")
            {
                arrayDateZeroMnE = arrayDateZeroM;
            }
            else
            {
                arrayDateZeroMnE = arrayDateZeroE;
            }
            return arrayDateZeroMnE;
        }
    }
}
