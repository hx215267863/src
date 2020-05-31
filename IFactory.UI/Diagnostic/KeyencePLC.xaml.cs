using IFactory.UI.Controls;
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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace IFactory.UI.Diagnostic
{
    /// <summary>
    /// KeyencePLC.xaml 的交互逻辑
    /// </summary>
    public partial class KeyencePLC : BasePage, IComponentConnector
    {
        public KeyencePLC()
        {
            InitializeComponent();
        }
        public void Page_Loaded(object sender, RoutedEventArgs e)
        {
            comboBoxWriteBool.Items.Add("TRUE");
            comboBoxWriteBool.Items.Add("FALSE");
            comboBoxWriteBool.SelectedItem = comboBoxWriteBool.Items[0];
        }

        private void buttonReadBool_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string addr = textBoxRBoolAddr.Text.Trim();
                if (MainWindow.m_MainCtrl._netPLC.ReadBool(addr))
                {
                    labelBoolValue.Content = "TRUE";
                }
                else
                {
                    labelBoolValue.Content = "FALSE";
                }
            }
            catch (Exception)
            {
                labelBoolValue.Content = "Fail";
            }          
        }

        private void buttonWriteBool_Click(object sender, RoutedEventArgs e)
        {
            //写bool
            int value = (0 == comboBoxWriteBool.SelectedIndex) ? 1 : 0;
            string addr = textBoxRBoolAddr.Text.Trim();
            MainWindow.m_MainCtrl._netPLC.WriteUshort(addr, (ushort)value);
        }

        private void buttonReadReg_Click(object sender, RoutedEventArgs e)
        {
            string addr = textBoxRBoolAddr.Text.Trim();
            string value = MainWindow.m_MainCtrl._netPLC.ReadAscString(addr, 1);
            labelRegValue.Content = value;
        }

        private void buttonWriteReg_Click(object sender, RoutedEventArgs e)
        {
            string addr = textBoxRBoolAddr.Text.Trim();           
            MainWindow.m_MainCtrl._netPLC.WriteAscString(addr, "写入的消息", 1);//写寄存器            
        }

        //var bo1 = MainWindow.m_MainCtrl._netPLC.ReadBool("");//读bool
        //MainWindow.m_MainCtrl._netPLC.WriteUshort("", 1); //写bool
        //string str1 = MainWindow.m_MainCtrl._netPLC.ReadAscString("", 1);//读寄存器
        //MainWindow.m_MainCtrl._netPLC.WriteAscString("", "写入的消息", 0);//写寄存器
    }
}
