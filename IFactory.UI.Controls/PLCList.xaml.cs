using IFactory.Domain.Models;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace IFactory.UI.Controls
{
    /// <summary>
    /// PLCList.xaml 的交互逻辑
    /// </summary>
    public partial class PLCList : Grid, IComponentConnector
    {
        public PLCList()
        {
            InitializeComponent();
        }

        public void BindData(IList<PLCStateModel> plcStates)
        {
            if (plcStates == null || plcStates.Count == 0)
            {
                this.Visibility = Visibility.Collapsed;
            }
            else
            {
                if (plcStates.Select(m => m.State).Distinct().Count() == 1)
                {
                    this.gridContainer1.Visibility = Visibility.Visible;
                    this.gridContainer2.Visibility = Visibility.Collapsed;
                    this.gridContainerN.Visibility = Visibility.Collapsed;
                    this.plcItems.Children.Clear();
                    foreach (PLCStateModel model in plcStates)
                    {
                        Button element = new Button
                        {
                            Content = model.PLCName,
                            Style = (Style)FindResource("PLCItemStyle")
                        };
                        this.plcItems.Children.Add(element);
                    }
                    if (plcStates.First<PLCStateModel>().State == 1)
                    {
                        this.imgPLCState.Source = new BitmapImage(new Uri("pack://application:,,,/IFactory.UI.Controls;component/Assets/plc_circle2.png", UriKind.Absolute));
                    }
                    else
                    {
                        this.imgPLCState.Source = new BitmapImage(new Uri("pack://application:,,,/IFactory.UI.Controls;component/Assets/plc_circle1.png", UriKind.Absolute));
                    }
                }
                else if (plcStates.GroupBy<PLCStateModel, int>(m => m.State).Max<IGrouping<int, PLCStateModel>>(m => m.Count<PLCStateModel>()) < 4)
                {
                    this.gridContainer1.Visibility = Visibility.Collapsed;
                    this.gridContainer2.Visibility = Visibility.Visible;
                    this.gridContainerN.Visibility = Visibility.Collapsed;
                    this.plcItems21.Children.Clear();
                    foreach (PLCStateModel plcStateModel in plcStates.Where<PLCStateModel>(m => m.State == 0))
                    {
                        Button button = new Button();
                        button.Content = plcStateModel.PLCName;
                        button.Style = (Style)this.FindResource("PLCItemStyle");
                        this.plcItems21.Children.Add((UIElement)button);
                    }
                    this.plcItems22.Children.Clear();
                    foreach (PLCStateModel plcStateModel in plcStates.Where<PLCStateModel>(m => m.State == 1))
                    {
                        Button button = new Button();
                        button.Content = plcStateModel.PLCName;
                        button.Style = (Style)this.FindResource("PLCItemStyle");
                        this.plcItems22.Children.Add((UIElement)button);
                    }
                }
                else
                {
                    this.gridContainer1.Visibility = Visibility.Collapsed;
                    this.gridContainer2.Visibility = Visibility.Collapsed;
                    this.gridContainerN.Visibility = Visibility.Visible;
                    this.plcItemsN1.Children.Clear();
                    foreach (PLCStateModel plcStateModel in plcStates.Where<PLCStateModel>(m => m.State == 0))
                    {
                        Button button = new Button();
                        button.Content = plcStateModel.PLCName;
                        button.Style = (Style)this.FindResource("PLCItemStyle");
                        this.plcItemsN1.Children.Add((UIElement)button);
                    }
                    this.plcItemsN2.Children.Clear();
                    foreach (PLCStateModel plcStateModel in plcStates.Where<PLCStateModel>(m => m.State == 1))
                    {
                        Button button = new Button();
                        button.Content = plcStateModel.PLCName;
                        button.Style = (Style)this.FindResource("PLCItemStyle");
                        this.plcItemsN2.Children.Add((UIElement)button);
                    }
                }
                this.Visibility = Visibility.Visible;
            }
        }
    }
}
