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
using System.Windows.Media;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Windows.Threading;

namespace IFactory.UI.DataWareHouse
{
    /// <summary>
    /// Page1.xaml 的交互逻辑
    /// </summary>
    public partial class WorkCalendar2 : BasePage, IComponentConnector
    {

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeComponent();
            label_Month.Content = DateTime.Now.Month.ToString();
            //Load();
            Simon.Children.Clear();
            WorkTimes();
            Workdays();
            Workhours();
            WorkDates();
        }

        public string shift { get; set; }
        private DateTime[] arrayDate { get; set; }
        private DateTime[] arrayDays { get; set; }
        //private DateTime[] arrayHours { get; set; }

        private DateTime[] arrayHours = new DateTime [102400];

        private void LoadCraftChartData()
        {
            DataPicResponse datapicResponse = LocalApi.WorkCalendar2(new DataPicRequest() { });
            arrayDate = datapicResponse.DataPics.Select(m => m.ProductTime).ToArray();
        }

        private void LoadWorkDays()
        {
            DataPicResponse datapicResponse2 = LocalApi.WorkDays2(new DataPicRequest() { });
            arrayDays = datapicResponse2.DataPics.Select(m => m.ProductTime).ToArray();
        }

        private void LoadWorkhours()
        {
            DataPicResponse datapicResponse2 = LocalApi.Workhours2(new DataPicRequest() { });
            arrayHours = datapicResponse2.DataPics.Select(m => m.ProductTime).ToArray();
        }

        private void Workhours()
        {
            int a = 0;
            int b = 0;
            int h;
            LoadWorkhours();
            if(arrayHours.Length > 0)
            {
                h = arrayHours[0].Hour;
                for (int i = 0; i < arrayHours.Length - 1; i++)
                {
                    if(arrayHours[i].Year == DateTime.Now.Year)
                    {
                        if (arrayHours[i].Month == arrayHours[i + 1].Month)
                        {
                            if (arrayHours[i].Day != arrayHours[i + 1].Day)
                            {
                                a = arrayHours[i].Hour - h;
                                h = arrayHours[i + 1].Hour;
                                b = b + a;
                            }
                            else if (((i + 1) == arrayHours.Length - 1) && arrayHours[i].Day == arrayHours[i + 1].Day)
                            {
                                a = arrayHours[i + 1].Hour - h;
                                b = b + a;
                                PrintB(i + 1, b);
                            }
                        }
                        else
                        {
                            PrintB(i, b);
                            h = arrayHours[i + 1].Hour;
                            b = 0;
                        }
                    }
                }
            }      
        }

        private void Workdays()
        {
            int a = 1;
            LoadWorkDays();
            for (int i = 0; i < arrayDays.Length - 1; i++)
            {
                if(arrayDays[i].Year == DateTime.Now.Year)
                {
                    if (int.Parse(arrayDays[i + 1].Month.ToString()) == int.Parse(arrayDays[i].Month.ToString()))
                    {
                        if (int.Parse(arrayDays[i + 1].Day.ToString()) != int.Parse(arrayDays[i].Day.ToString()))
                        {
                            a++;
                        }
                    }

                    if ((i + 1) != arrayDays.Length - 1)
                    {
                        if (int.Parse(arrayDays[i + 1].Month.ToString()) != int.Parse(arrayDays[i].Month.ToString()))
                        {
                            PrintA(i, a);
                            a = 1;
                        }
                    }
                    else
                    {
                        if (int.Parse(arrayDays[i + 1].Month.ToString()) != int.Parse(arrayDays[i].Month.ToString()))
                        {
                            PrintA(i, a);
                            a = 1;
                            PrintA(i + 1, a);
                        }
                        else
                        {
                            PrintA(i, a);
                        }
                    }
                }
            }
            if (arrayDays.Length == 1)
            {
                PrintA(0, 1);
            }
        }

        private void WorkTimes()
        {
            LoadCraftChartData();
            for (int i = 0;i <= arrayDate.Length - 1; i++)
            {
                int b = int.Parse(arrayDate[i].Day.ToString());
                int c = int.Parse(arrayDate[i].Hour.ToString());
                
                    if (c >= 0 && c < 2)
                    {
                        if (b == 1)
                            Rec12_1.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 2)
                            Rec12_2.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 3)
                            Rec12_3.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 4)
                            Rec12_4.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 5)
                            Rec12_5.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 6)
                            Rec12_6.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 7)
                            Rec12_7.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 8)
                            Rec12_8.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 9)
                            Rec12_9.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 10)
                            Rec12_10.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 11)
                            Rec12_11.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 12)
                            Rec12_12.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 13)
                            Rec12_13.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 14)
                            Rec12_14.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 15)
                            Rec12_15.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 16)
                            Rec12_16.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 17)
                            Rec12_17.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 18)
                            Rec12_18.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 19)
                            Rec12_19.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 20)
                            Rec12_20.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 21)
                            Rec12_21.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 22)
                            Rec12_22.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 23)
                            Rec12_23.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 24)
                            Rec12_24.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 25)
                            Rec12_25.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 26)
                            Rec12_26.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 27)
                            Rec12_27.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 28)
                            Rec12_28.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 29)
                            Rec12_29.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 30)
                            Rec12_30.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 31)
                            Rec12_31.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                    }

                    if (c >= 2 && c < 4)
                    {
                        if (b == 1)
                            Rec11_1.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 2)
                            Rec11_2.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 3)
                            Rec11_3.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 4)
                            Rec11_4.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 5)
                            Rec11_5.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 6)
                            Rec11_6.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 7)
                            Rec11_7.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 8)
                            Rec11_8.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 9)
                            Rec11_9.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 10)
                            Rec11_10.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 11)
                            Rec11_11.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 12)
                            Rec11_12.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 13)
                            Rec11_13.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 14)
                            Rec11_14.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 15)
                            Rec11_15.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 16)
                            Rec11_16.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 17)
                            Rec11_17.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 18)
                            Rec11_18.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 19)
                            Rec11_19.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 20)
                            Rec11_20.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 21)
                            Rec11_21.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 22)
                            Rec11_22.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 23)
                            Rec11_23.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 24)
                            Rec11_24.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 25)
                            Rec11_25.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 26)
                            Rec11_26.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 27)
                            Rec11_27.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 28)
                            Rec11_28.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 29)
                            Rec11_29.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 30)
                            Rec11_30.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 31)
                            Rec11_31.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                    }

                    if (c >= 4 && c < 6)
                    {
                        if (b == 1)
                            Rec10_1.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 2)
                            Rec10_2.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 3)
                            Rec10_3.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 4)
                            Rec10_4.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 5)
                            Rec10_5.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 6)
                            Rec10_6.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 7)
                            Rec10_7.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 8)
                            Rec10_8.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 9)
                            Rec10_9.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 10)
                            Rec10_10.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 11)
                            Rec10_11.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 12)
                            Rec10_12.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 13)
                            Rec10_13.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 14)
                            Rec10_14.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 15)
                            Rec10_15.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 16)
                            Rec10_16.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 17)
                            Rec10_17.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 18)
                            Rec10_18.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 19)
                            Rec10_19.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 20)
                            Rec10_20.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 21)
                            Rec10_21.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 22)
                            Rec10_22.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 23)
                            Rec10_23.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 24)
                            Rec10_24.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 25)
                            Rec10_25.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 26)
                            Rec10_26.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 27)
                            Rec10_27.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 28)
                            Rec10_28.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 29)
                            Rec10_29.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 30)
                            Rec10_30.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 31)
                            Rec10_31.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                    }

                    if (c >= 6 && c < 8)
                    {
                        if (b == 1)
                            Rec9_1.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 2)
                            Rec9_2.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 3)
                            Rec9_3.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 4)
                            Rec9_4.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 5)
                            Rec9_5.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 6)
                            Rec9_6.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 7)
                            Rec9_7.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 8)
                            Rec9_8.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 9)
                            Rec9_9.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 10)
                            Rec9_10.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 11)
                            Rec9_11.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 12)
                            Rec9_12.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 13)
                            Rec9_13.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 14)
                            Rec9_14.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 15)
                            Rec9_15.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 16)
                            Rec9_16.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 17)
                            Rec9_17.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 18)
                            Rec9_18.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 19)
                            Rec9_19.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 20)
                            Rec9_20.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 21)
                            Rec9_21.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 22)
                            Rec9_22.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 23)
                            Rec9_23.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 24)
                            Rec9_24.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 25)
                            Rec9_25.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 26)
                            Rec9_26.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 27)
                            Rec9_27.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 28)
                            Rec9_28.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 29)
                            Rec9_29.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 30)
                            Rec9_30.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 31)
                            Rec9_31.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                    }

                    if (c >= 8 && c < 10)
                    {
                        if (b == 1)
                            Rec8_1.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 2)
                            Rec8_2.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 3)
                            Rec8_3.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 4)
                            Rec8_4.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 5)
                            Rec8_5.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 6)
                            Rec8_6.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 7)
                            Rec8_7.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 8)
                            Rec8_8.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 9)
                            Rec8_9.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 10)
                            Rec8_10.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 11)
                            Rec8_11.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 12)
                            Rec8_12.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 13)
                            Rec8_13.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 14)
                            Rec8_14.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 15)
                            Rec8_15.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 16)
                            Rec8_16.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 17)
                            Rec8_17.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 18)
                            Rec8_18.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 19)
                            Rec8_19.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 20)
                            Rec8_20.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 21)
                            Rec8_21.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 22)
                            Rec8_22.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 23)
                            Rec8_23.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 24)
                            Rec8_24.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 25)
                            Rec8_25.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 26)
                            Rec8_26.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 27)
                            Rec8_27.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 28)
                            Rec8_28.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 29)
                            Rec8_29.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 30)
                            Rec8_30.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 31)
                            Rec8_31.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                    }

                    if (c >= 10 && c < 12)
                    {
                        if (b == 1)
                            Rec7_1.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 2)
                            Rec7_2.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 3)
                            Rec7_3.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 4)
                            Rec7_4.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 5)
                            Rec7_5.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 6)
                            Rec7_6.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 7)
                            Rec7_7.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 8)
                            Rec7_8.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 9)
                            Rec7_9.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 10)
                            Rec7_10.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 11)
                            Rec7_11.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 12)
                            Rec7_12.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 13)
                            Rec7_13.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 14)
                            Rec7_14.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 15)
                            Rec7_15.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 16)
                            Rec7_16.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 17)
                            Rec7_17.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 18)
                            Rec7_18.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 19)
                            Rec7_19.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 20)
                            Rec7_20.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 21)
                            Rec7_21.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 22)
                            Rec7_22.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 23)
                            Rec7_23.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 24)
                            Rec7_24.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 25)
                            Rec7_25.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 26)
                            Rec7_26.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 27)
                            Rec7_27.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 28)
                            Rec7_28.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 29)
                            Rec7_29.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 30)
                            Rec7_30.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 31)
                            Rec7_31.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                    }

                    if (c >= 12 && c < 14)
                    {
                        if (b == 1)
                            Rec6_1.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 2)
                            Rec6_2.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 3)
                            Rec6_3.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 4)
                            Rec6_4.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 5)
                            Rec6_5.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 6)
                            Rec6_6.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 7)
                            Rec6_7.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 8)
                            Rec6_8.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 9)
                            Rec6_9.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 10)
                            Rec6_10.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 11)
                            Rec6_11.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 12)
                            Rec6_12.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 13)
                            Rec6_13.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 14)
                            Rec6_14.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 15)
                            Rec6_15.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 16)
                            Rec6_16.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 17)
                            Rec6_17.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 18)
                            Rec6_18.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 19)
                            Rec6_19.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 20)
                            Rec6_20.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 21)
                            Rec6_21.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 22)
                            Rec6_22.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 23)
                            Rec6_23.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 24)
                            Rec6_24.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 25)
                            Rec6_25.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 26)
                            Rec6_26.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 27)
                            Rec6_27.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 28)
                            Rec6_28.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 29)
                            Rec6_29.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 30)
                            Rec6_30.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 31)
                            Rec6_31.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                    }

                    if (c >= 14 && c < 16)
                    {
                        if (b == 1)
                            Rec5_1.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 2)
                            Rec5_2.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 3)
                            Rec5_3.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 4)
                            Rec5_4.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 5)
                            Rec5_5.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 6)
                            Rec5_6.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 7)
                            Rec5_7.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 8)
                            Rec5_8.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 9)
                            Rec5_9.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 10)
                            Rec5_10.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 11)
                            Rec5_11.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 12)
                            Rec5_12.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 13)
                            Rec5_13.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 14)
                            Rec5_14.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 15)
                            Rec5_15.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 16)
                            Rec5_16.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 17)
                            Rec5_17.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 18)
                            Rec5_18.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 19)
                            Rec5_19.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 20)
                            Rec5_20.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 21)
                            Rec5_21.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 22)
                            Rec5_22.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 23)
                            Rec5_23.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 24)
                            Rec5_24.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 25)
                            Rec5_25.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 26)
                            Rec5_26.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 27)
                            Rec5_27.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 28)
                            Rec5_28.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 29)
                            Rec5_29.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 30)
                            Rec5_30.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 31)
                            Rec5_31.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                    }

                    if (c >= 16 && c < 18)
                    {
                        if (b == 1)
                            Rec4_1.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 2)
                            Rec4_2.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 3)
                            Rec4_3.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 4)
                            Rec4_4.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 5)
                            Rec4_5.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 6)
                            Rec4_6.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 7)
                            Rec4_7.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 8)
                            Rec4_8.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 9)
                            Rec4_9.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 10)
                            Rec4_10.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 11)
                            Rec4_11.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 12)
                            Rec4_12.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 13)
                            Rec4_13.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 14)
                            Rec4_14.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 15)
                            Rec4_15.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 16)
                            Rec4_16.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 17)
                            Rec4_17.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 18)
                            Rec4_18.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 19)
                            Rec4_19.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 20)
                            Rec4_20.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 21)
                            Rec4_21.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 22)
                            Rec4_22.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 23)
                            Rec4_23.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 24)
                            Rec4_24.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 25)
                            Rec4_25.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 26)
                            Rec4_26.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 27)
                            Rec4_27.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 28)
                            Rec4_28.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 29)
                            Rec4_29.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 30)
                            Rec4_30.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 31)
                            Rec4_31.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                    }

                    if (c >= 18 && c < 20)
                    {
                        if (b == 1)
                            Rec3_1.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 2)
                            Rec3_2.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 3)
                            Rec3_3.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 4)
                            Rec3_4.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 5)
                            Rec3_5.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 6)
                            Rec3_6.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 7)
                            Rec3_7.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 8)
                            Rec3_8.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 9)
                            Rec3_9.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 10)
                            Rec3_10.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 11)
                            Rec3_11.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 12)
                            Rec3_12.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 13)
                            Rec3_13.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 14)
                            Rec3_14.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 15)
                            Rec3_15.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 16)
                            Rec3_16.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 17)
                            Rec3_17.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 18)
                            Rec3_18.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 19)
                            Rec3_19.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 20)
                            Rec3_20.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 21)
                            Rec3_21.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 22)
                            Rec3_22.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 23)
                            Rec3_23.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 24)
                            Rec3_24.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 25)
                            Rec3_25.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 26)
                            Rec3_26.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 27)
                            Rec3_27.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 28)
                            Rec3_28.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 29)
                            Rec3_29.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 30)
                            Rec3_30.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 31)
                            Rec3_31.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                    }

                    if (c >= 20 && c < 22)
                    {
                        if (b == 1)
                            Rec2_1.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 2)
                            Rec2_2.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 3)
                            Rec2_3.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 4)
                            Rec2_4.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 5)
                            Rec2_5.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 6)
                            Rec2_6.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 7)
                            Rec2_7.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 8)
                            Rec2_8.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 9)
                            Rec2_9.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 10)
                            Rec2_10.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 11)
                            Rec2_11.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 12)
                            Rec2_12.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 13)
                            Rec2_13.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 14)
                            Rec2_14.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 15)
                            Rec2_15.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 16)
                            Rec2_16.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 17)
                            Rec2_17.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 18)
                            Rec2_18.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 19)
                            Rec2_19.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 20)
                            Rec2_20.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 21)
                            Rec2_21.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 22)
                            Rec2_22.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 23)
                            Rec2_23.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 24)
                            Rec2_24.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 25)
                            Rec2_25.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 26)
                            Rec2_26.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 27)
                            Rec2_27.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 28)
                            Rec2_28.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 29)
                            Rec2_29.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 30)
                            Rec2_30.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 31)
                            Rec2_31.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                    }

                    if (c >=22  && c < 24)
                    {
                        if (b == 1)
                            Rec1_1.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 2)
                            Rec1_2.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 3)
                            Rec1_3.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 4)
                            Rec1_4.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 5)
                            Rec1_5.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 6)
                            Rec1_6.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 7)
                            Rec1_7.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 8)
                            Rec1_8.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 9)
                            Rec1_9.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 10)
                            Rec1_10.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 11)
                            Rec1_11.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 12)
                            Rec1_12.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 13)
                            Rec1_13.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 14)
                            Rec1_14.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 15)
                            Rec1_15.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 16)
                            Rec1_16.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 17)
                            Rec1_17.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 18)
                            Rec1_18.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 19)
                            Rec1_19.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 20)
                            Rec1_20.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 21)
                            Rec1_21.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 22)
                            Rec1_22.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 23)
                            Rec1_23.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 24)
                            Rec1_24.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 25)
                            Rec1_25.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 26)
                            Rec1_26.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 27)
                            Rec1_27.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 28)
                            Rec1_28.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 29)
                            Rec1_29.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 30)
                            Rec1_30.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                        if (b == 31)
                            Rec1_31.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                    }
                }
    }

        private void WorkDates()
        {
            LoadCraftChartData();
            for (int i = 0; i <= arrayDate.Length - 1; i++)
            {
                int b = int.Parse(arrayDate[i].Day.ToString());

                if (b == 1)
                    Rec1.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                if (b == 2)
                    Rec2.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                if (b == 3)
                    Rec3.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                if (b == 4)
                    Rec4.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                if (b == 5)
                    Rec5.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                if (b == 6)
                    Rec6.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                if (b == 7)
                    Rec7.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                if (b == 8)
                    Rec8.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                if (b == 9)
                    Rec9.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                if (b == 10)
                    Rec10.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                if (b == 11)
                    Rec11.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                if (b == 12)
                    Rec12.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                if (b == 13)
                    Rec13.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                if (b == 14)
                    Rec14.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                if (b == 15)
                    Rec15.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                if (b == 16)
                    Rec16.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                if (b == 17)
                    Rec17.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                if (b == 18)
                    Rec18.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                if (b == 19)
                    Rec19.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                if (b == 20)
                    Rec20.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                if (b == 21)
                    Rec21.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                if (b == 22)
                    Rec22.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                if (b == 23)
                    Rec23.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                if (b == 24)
                    Rec24.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                if (b == 25)
                    Rec25.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                if (b == 26)
                    Rec26.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                if (b == 27)
                    Rec27.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                if (b == 28)
                    Rec28.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                if (b == 29)
                    Rec29.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                if (b == 30)
                    Rec30.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
                if (b == 31)
                    Rec31.Fill = new SolidColorBrush(Color.FromRgb(7, 129, 253));
            }
        }

        private void PrintA(int i,int a)
        {
            if (int.Parse(arrayDays[i].Month.ToString()) == 1)
            {
                labeld_1.Content = a;
            }
            if (int.Parse(arrayDays[i].Month.ToString()) == 2)
            {
                labeld_2.Content = a;
            }
            if (int.Parse(arrayDays[i].Month.ToString()) == 3)
            {
                labeld_3.Content = a;
            }
            if (int.Parse(arrayDays[i].Month.ToString()) == 4)
            {
                labeld_4.Content = a;
            }
            if (int.Parse(arrayDays[i].Month.ToString()) == 5)
            {
                labeld_5.Content = a;
            }
            if (int.Parse(arrayDays[i].Month.ToString()) == 6)
            {
                labeld_6.Content = a;
            }
            if (int.Parse(arrayDays[i].Month.ToString()) == 7)
            {
                labeld_7.Content = a;
            }
            if (int.Parse(arrayDays[i].Month.ToString()) == 8)
            {
                labeld_8.Content = a;
            }
            if (int.Parse(arrayDays[i].Month.ToString()) == 9)
            {
                labeld_9.Content = a;
            }
            if (int.Parse(arrayDays[i].Month.ToString()) == 10)
            {
                labeld_10.Content = a;
            }
            if (int.Parse(arrayDays[i].Month.ToString()) == 11)
            {
                labeld_11.Content = a;
            }
            if (int.Parse(arrayDays[i].Month.ToString()) == 12)
            {
                labeld_12.Content = a;
            }
        }

        private void PrintB(int i, int a)
        {
            if (int.Parse(arrayHours[i].Month.ToString()) == 1)
            {
                labelh_1.Content = a;
            }
            if (int.Parse(arrayHours[i].Month.ToString()) == 2)
            {
                labelh_2.Content = a;
            }
            if (int.Parse(arrayHours[i].Month.ToString()) == 3)
            {
                labelh_3.Content = a;
            }
            if (int.Parse(arrayHours[i].Month.ToString()) == 4)
            {
                labelh_4.Content = a;
            }
            if (int.Parse(arrayHours[i].Month.ToString()) == 5)
            {
                labelh_5.Content = a;
            }
            if (int.Parse(arrayHours[i].Month.ToString()) == 6)
            {
                labelh_6.Content = a;
            }
            if (int.Parse(arrayHours[i].Month.ToString()) == 7)
            {
                labelh_7.Content = a;
            }
            if (int.Parse(arrayHours[i].Month.ToString()) == 8)
            {
                labelh_8.Content = a;
            }
            if (int.Parse(arrayHours[i].Month.ToString()) == 9)
            {
                labelh_9.Content = a;
            }
            if (int.Parse(arrayHours[i].Month.ToString()) == 10)
            {
                labelh_10.Content = a;
            }
            if (int.Parse(arrayHours[i].Month.ToString()) == 11)
            {
                labelh_11.Content = a;
            }
            if (int.Parse(arrayHours[i].Month.ToString()) == 12)
            {
                labelh_12.Content = a;
            }
        }
        //点击事件
        void dataPoint_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //DataPoint dp = sender as DataPoint;
            //MessageBox.Show(dp.YValue.ToString());
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            int time = DateTime.Now.Hour;
            if(8 <= time && time < 20)
            {
                shift = "M";
            }
            else
            {
                shift = "E";
            }
        }
    }
}
