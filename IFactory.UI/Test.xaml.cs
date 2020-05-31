using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;


namespace IFactory.UI
{
    /// <summary>
    /// Test.xaml 的交互逻辑
    /// </summary>
    public partial class Test : Window, IComponentConnector
    {
        public Test()
        {
            InitializeComponent();
        }

        private int colorIndex = -1;

        public static T GetVisualChild<T>(Visual parent) where T : Visual
        {
            T obj = default(T);
            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int childIndex = 0; childIndex < childrenCount; ++childIndex)
            {
                Visual parent1 = (Visual)VisualTreeHelper.GetChild(parent, childIndex);
                obj = parent1 as T;
                if (obj == null)
                    obj = Test.GetVisualChild<T>(parent1);
                if (obj != null)
                    break;
            }
            return obj;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            this.colorIndex = (this.colorIndex + 1) % App.Colors.Count;
            this.border.Background = new SolidColorBrush(App.Colors[this.colorIndex]);
            this.txtValue.Content = this.colorIndex;
        }
    }
}
