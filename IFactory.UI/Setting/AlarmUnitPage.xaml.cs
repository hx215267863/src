using IFactory.UI.Controls;
using IFactory.UI.Core;
using IFactory.Domain.Models;
using IFactory.Platform.Common.Request;
using IFactory.Platform.Common.Request.Product;
using IFactory.Platform.Common.Response.Product;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace IFactory.UI.Setting
{
    /// <summary>
    /// AlarmUnitPage.xaml 的交互逻辑
    /// </summary>
    public partial class AlarmUnitPage : BasePage, IComponentConnector
    {
        public AlarmUnitPage()
        {
            InitializeComponent();

            this.EditCommand = (ICommand)new RouteCommand<UnitModel>(new Action<UnitModel>(this.EditUnit));
            this.DeleteCommand = (ICommand)new RouteCommand<UnitModel>(new Action<UnitModel>(this.DeleteUnit));
            this.DataContext = this;
        }

        public ICommand DeleteCommand { get; set; }

        public ICommand EditCommand { get; set; }
        
        private void EditUnit(UnitModel user)
        {
            bool? nullable = new AlarmUnitAddDialog() { UnitDID = user.UnitDID }.ShowDialog();
            bool flag = true;
            if ((nullable.GetValueOrDefault() == flag ? (nullable.HasValue ? 1 : 0) : 0) == 0)
                return;
            this.RefreshData();
        }

        private void DeleteUnit(UnitModel model)
        {
            if (MessageBox.Show("确认删除该部件吗？", "提示", MessageBoxButton.OKCancel) != MessageBoxResult.OK)
                return;
            UnitDeleteResponse unitDeleteResponse = LocalApi.Execute(new UnitDeleteRequest() { UnitDID = model.UnitDID });
            this.RefreshData();
        }

        public void RefreshData()
        {
            this.dataGrid.ItemsSource = (LocalApi.Execute(new UnitListRequest())).Units;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.RefreshData();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            bool? nullable = new AlarmUnitAddDialog().ShowDialog();
            bool flag = true;
            if ((nullable.GetValueOrDefault() == flag ? (nullable.HasValue ? 1 : 0) : 0) == 0)
                return;
            this.RefreshData();
        }
    }
}
