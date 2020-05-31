using System;
using System.Windows;
using System.Windows.Input;
using IFactory.UI.Controls;
using IFactory.Domain.Models;
using IFactory.UI.Core;
using System.Windows.Markup;
using System.Xml;
using System.Windows.Data;
using System.Windows.Controls;

namespace IFactory.UI.Argument
{
    /// <summary>
    /// BtyArgumentPage2.xaml 的交互逻辑
    /// </summary>
    public partial class BtyArgumentPage2 : BasePage, IComponentConnector
    {
        public BtyArgumentPage2()
        {
            InitializeComponent();
            RefreshData();
        }

        public static string strName { get; set; }

        public void RefreshData()
        {
            System.Data.DataSet ds = new System.Data.DataSet();
            string strAddr = "C:\\Users\\junxi\\Desktop\\bat\\" + strName + "\\imageProcessParameters.xml";
            ds.ReadXml(strAddr);
            System.Data.DataTable dt = ds.Tables[0];
            this.dataGrid.ItemsSource = dt.DefaultView;
        }
    }
}

