/*
    实时看板界面;
    （未使用界面）
*/

using IFactory.UI.Core;
using IFactory.UI.Helpers;
using IFactory.Common;
using IFactory.Domain.Models;
using IFactory.Domain.Models.Report;
using IFactory.Platform.Common.Request;
using IFactory.Platform.Common.Request.Product;
using IFactory.Platform.Common.Request.Report;
using IFactory.Platform.Common.Response.Product;
using IFactory.Platform.Common.Response.Report;
using LiveCharts;
using LiveCharts.Definitions.Series;
using LiveCharts.Wpf;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace IFactory.UI
{
    /// <summary>
    /// Live.xaml 的交互逻辑
    /// </summary>
    public partial class Live : Window, IComponentConnector
    {
        public Live()
        {
            //初始化窗口控件
            InitializeComponent();
        }

        //定时器
        private DispatcherTimer refreshTimeTimer = new DispatcherTimer();
        private DispatcherTimer refreshStateTimer = new DispatcherTimer();
        private KanbanSettingModel kanbanSettingModel;

        //
        private async void RefreshChartData()
        {
            KanbanReportGetResponse reportGetResponse = await ClientHelper.ExecuteAsync<KanbanReportGetResponse>((IRequest<KanbanReportGetResponse>)new KanbanReportGetRequest());
            if (reportGetResponse.IsError)
                return;
            this.kanbanSettingModel = reportGetResponse.KanbanSetting;
            this.productionChart.SeriesColors = new Color[1]
            {
        Color.FromRgb( 120,  225,  0)
            }.ToColorsCollection();
            CartesianChart cartesianChart1 = this.productionChart;
            SeriesCollection seriesCollection1 = new SeriesCollection();
            LineSeries lineSeries = new LineSeries();
            string str1 = "";
            lineSeries.Title = str1;
            ChartValues<int> chartValues1 = new ChartValues<int>(reportGetResponse.ProductionReportData.Select<TextValueModel<int>, int>(m => m.Value));
            lineSeries.Values = (IChartValues)chartValues1;
            int num1 = 1;
            lineSeries.DataLabels = num1 != 0;
            seriesCollection1.Add((ISeriesView)lineSeries);
            string[] array1 = reportGetResponse.ProductionReportData.Select<TextValueModel<int>, string>(m => m.Text).ToArray<string>();
            var data1 = new
            {
                ProductionSeries = seriesCollection1,
                ProductionLabels = array1,
                ProductionFormatter = (Func<int, string>)(value => value.ToString() + "PPM")
            };
            cartesianChart1.DataContext = data1;
            this.excellentRateChart.SeriesColors = new Color[2]
            {
        Color.FromRgb( 32,  226,  64),
        Color.FromRgb( 253,  194,  11)
            }.ToColorsCollection();
            CartesianChart cartesianChart2 = this.excellentRateChart;
            SeriesCollection seriesCollection2 = new SeriesCollection();
            ColumnSeries columnSeries = new ColumnSeries();
            string str2 = "";
            columnSeries.Title = str2;
            ChartValues<double> chartValues2 = new ChartValues<double>(reportGetResponse.ExcellentRateReportData.Select<TextValueModel<double>, double>(m => m.Value));
            columnSeries.Values = (IChartValues)chartValues2;
            int num2 = 0;
            columnSeries.DataLabels = num2 != 0;
            seriesCollection2.Add((ISeriesView)columnSeries);
            string[] array2 = reportGetResponse.ExcellentRateReportData.Select<TextValueModel<double>, string>(m => m.Text).ToArray<string>();
            var data2 = new { ExcellentRateSeries = seriesCollection2, ExcellentRateLabels = array2, ExcellentRateFormatter = (Func<double, string>)(value => value.ToString("N2")) };
            cartesianChart2.DataContext = data2;
            Func<ChartPoint, string> func = chartPoint => string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);
            SeriesCollection seriesCollection3 = new SeriesCollection();
            foreach (TextValueModel<int> textValueModel in (IEnumerable<TextValueModel<int>>)reportGetResponse.AlarmReportData)
            {
                PieSeries pieSeries1 = new PieSeries();
                pieSeries1.Title = textValueModel.Text;
                pieSeries1.LabelPoint = func;
                PieSeries pieSeries2 = pieSeries1;
                ChartValues<int> chartValues3 = new ChartValues<int>();
                int num3 = textValueModel.Value;
                chartValues3.Add(num3);
                pieSeries2.Values = (IChartValues)chartValues3;
                pieSeries1.DataLabels = true;
                seriesCollection3.Add((ISeriesView)pieSeries1);
            }
            this.alarmChart.SeriesColors = App.SeriesColors;
            this.alarmChart.Series = seriesCollection3;
            this.refreshStateTimer.Interval = new TimeSpan(0, 0, this.kanbanSettingModel.RefreshInterval);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.refreshTimeTimer.Interval = new TimeSpan(0, 0, 1);
            this.refreshTimeTimer.Tick += new EventHandler(this.RefreshTimeTimer_Tick);
            this.refreshTimeTimer.Start();
            this.refreshStateTimer.Interval = new TimeSpan(0, 0, 15);
            this.refreshStateTimer.Tick += new EventHandler(this.RefreshStateTimer_Tick);
            this.refreshStateTimer.Start();
            this.RefreshStates();
            this.RefreshChartData();
            this.RefreshProduction();
        }

        private void RefreshStateTimer_Tick(object sender, EventArgs e)
        {
            this.RefreshStates();
            this.RefreshChartData();
            this.RefreshProduction();
        }

        public async void RefreshStates()
        {
            CraftListResponse craftListResponse = await ClientHelper.ExecuteAsync<CraftListResponse>((IRequest<CraftListResponse>)new CraftListRequest());
            if (craftListResponse.IsError)
                return;
            foreach (CraftModel craft in (IEnumerable<CraftModel>)craftListResponse.Crafts)
            {
                string craftShortNo = CommonHelper.GetCraftShortNO(craft.CraftNO);
                foreach (object child in this.stateContainer.Children)
                {
                    if (child is Path)
                    {
                        Path path = (Path)child;
                        if ((string)path.Tag == craftShortNo)
                        {
                            Color stateInnerColor = this.GetStateInnerColor(craft.State);
                            path.Fill = new SolidColorBrush(stateInnerColor);
                        }
                    }
                }
            }
        }

        public async void RefreshProduction()
        {
            ProductionLineProbablyGetResponse probablyGetResponse = await ClientHelper.ExecuteAsync<ProductionLineProbablyGetResponse>((IRequest<ProductionLineProbablyGetResponse>)new ProductionLineProbablyGetRequest() { DID = 1 });
            if (probablyGetResponse.IsError)
                return;
            this.gridProduction.DataContext = probablyGetResponse.ProductionLineProbably;
        }

        public Color GetStateInnerColor(int state)
        {
            switch (state)
            {
                case 0:
                    return Color.FromArgb(0, 0, 217, 0);
                case 1:
                    return Color.FromArgb(200, 166, 209, 217);
                case 2:
                    return Color.FromArgb(200, 217, 213, 0);
                default:
                    return Color.FromArgb(200, 213, 22, 34);
            }
        }

        private void RefreshTimeTimer_Tick(object sender, EventArgs e)
        {
            if (this.kanbanSettingModel == null)
            {
                string[] strArray = new string[7] { "星期天", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
                this.txtDate.Content = DateTime.Now.ToString("yyyy.MM.dd");
                this.txtWeekDay.Content = strArray[(int)DateTime.Now.DayOfWeek];
                this.txtTime.Content = DateTime.Now.ToString("HH:mm:ss");
            }
            else
            {
                string[] strArray = new string[7] { "星期天", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
                this.txtDate.Content = DateTime.Now.ToString(this.kanbanSettingModel.DateFormat);
                this.txtWeekDay.Content = strArray[(int)DateTime.Now.DayOfWeek];
                this.txtTime.Content = DateTime.Now.ToString(this.kanbanSettingModel.TimeFormat);
            }
        }
    }
}
