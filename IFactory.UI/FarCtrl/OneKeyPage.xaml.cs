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
using IFactory.Platform.Common.Response.Product;
using IFactory.Platform.Common.Request.Product;
using System.Windows.Media;

namespace IFactory.UI.FarCtrl
{
    /// <summary>
    /// Page1.xaml 的交互逻辑
    /// </summary>
    public partial class OneKey : BasePage, IComponentConnector
    {
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeComponent();
            Init();
        }

        static public bool oneKey_flag = false;

        public int isReady { get; set; }

        static public int isReady_change { get; set; }

        private void Init()
        {
            OneKeyResponse oneKeyResponse = LocalApi.OneKey(new OneKeyRequest() {});

            isReady = oneKeyResponse.oneKeys.Select(m=>m.OneKey_flag).ToArray()[0];

            if(isReady == 0)
            {
                oneKey_flag = true;
                label.Content = "启动中。。。";
                //btnOneKey.Content = "停止";
                button.Content = "启动中。。。";
                RecGO_DOWN.Fill = new SolidColorBrush(Color.FromRgb(0, 255, 64));
            }
            else if(isReady == 1)
            {
                oneKey_flag = false;
                label.Content = "停止中。。。";
                //btnOneKey.Content = "启动";
                //button.Content = "启动中。。。";
                RecGO_DOWN.Fill = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            }
        }

        /*
        private void btnOneKey_Click(object sender, RoutedEventArgs e)
        {
            if(oneKey_flag == false)
            { 
                oneKey_flag = true;
                label.Content = "启动中。。。";
                btnOneKey.Content = "停止";
                isReady_change = 0;
            }
            else
            {
                oneKey_flag = false;
                label.Content = "停止中。。。";
                btnOneKey.Content = "启动";
                isReady_change = 1;
            }

            OneKeyResponse oneKeyResponse = LocalApi.IsReady(new OneKeyRequest() { OneKey_flag = isReady_change});
        }
        */
    }
}
