using IFactory.UI.Core;
using IFactory.Common.Logs;
using LiveCharts.Wpf;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using IFactory.LocalMap;

namespace IFactory.UI
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        static App()
        {
            App.SeriesColors.AddRange(App.Colors);
        }

        public static readonly List<Color> Colors = new List<Color>()
        {
          Color.FromRgb( 243,  67,  54),
          Color.FromRgb( 254,  192,  7),
          Color.FromRgb( 96,  125,  138),
          Color.FromRgb( 0,  187,  211),
          Color.FromRgb( 232,  30,  99),
          Color.FromRgb( 254,  87,  34),
          Color.FromRgb( 63,  81,  180),
          Color.FromRgb( 204,  219,  57),
          Color.FromRgb( 0,  149,  135),
          Color.FromRgb( 76,  174,  80) 
        };

        public static ColorsCollection SeriesColors = new ColorsCollection();

        //开始运行程序
        protected override void OnStartup(StartupEventArgs e)
        {
            if (!AppContext.Current.LoadLocalConfig())
            {
                MessageBox.Show("加载本地配置文件失败！", "提示");
            }
            if (AppContext.Current.LocalConfig.ClientType == LocalConfig.ClientTypeEnum.Live)
                //获取路径
                this.StartupUri = new Uri("Live.xaml", UriKind.Relative);

            base.OnStartup(e);
            LogUtil.Setup();

            MapperBootstrapper.Run();

            //全局捕获异常
            //this.DispatcherUnhandledException += new DispatcherUnhandledExceptionEventHandler(this.Current_DispatcherUnhandledException);
            //AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(this.CurrentDomain_UnhandledException);
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show("我们很抱歉，当前应用程序遇到一些问题，该操作已经终止，请进行重试，如果问题继续存在，请联系管理员.", "意外的操作", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
        }

        private void Current_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("我们很抱歉，当前应用程序遇到一些问题，该操作已经终止，请进行重试，如果问题继续存在，请联系管理员.", "意外的操作", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            e.Handled = true;
        }
    }
}
