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

namespace IFactory.UI.Debug
{
    /// <summary>
    /// KeyencePLC.xaml 的交互逻辑
    /// </summary>
    public partial class KeyencePLC : BasePage, IComponentConnector
    {
        public void Page_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeComponent();
            comboBoxWriteBool.Items.Add("TRUE");
            comboBoxWriteBool.Items.Add("FALSE");
            comboBoxWriteBool.SelectedItem = comboBoxWriteBool.Items[0];
        }

        private void buttonReadBool_Click(object sender, RoutedEventArgs e)
        {
            ushort addr = Convert.ToUInt16(textBoxRBoolAddr.Text);
            bool value = false;
            if (ATL_MC.MainCtrlDialog.MainWindow.m_MainWindow.m_MainCrtl.mKEYENCE_PLC.ReadBool(addr, out value))
            {
                if(value)
                    labelBoolValue.Content = "TRUE";
                else
                    labelBoolValue.Content = "FALSE";
            }
            else
            {
                labelBoolValue.Content = "Failed";
            }
        }

        private void buttonWriteBool_Click(object sender, RoutedEventArgs e)
        {
            bool value = (0== (comboBoxWriteBool.SelectedIndex + 1)) ?true:false;
            ushort addr = Convert.ToUInt16(textBoxWBoolAddr.Text);
            ATL_MC.MainCtrlDialog.MainWindow.m_MainWindow.m_MainCrtl.mKEYENCE_PLC.WriteBool(addr, value);
        }

        private void buttonReadReg_Click(object sender, RoutedEventArgs e)
        {
            ushort addr = Convert.ToUInt16(textBoxRRegAddr.Text);
            uint value;
            ATL_MC.MainCtrlDialog.MainWindow.m_MainWindow.m_MainCrtl.mKEYENCE_PLC.ReadRegister(addr, out value);
            labelRegValue.Content = value;
        }

        private void buttonWriteReg_Click(object sender, RoutedEventArgs e)
        {
            ushort addr = Convert.ToUInt16(textBoxWRegAddr.Text);
            uint value = Convert.ToUInt16(textBoxWvalue.Text);
            ATL_MC.MainCtrlDialog.MainWindow.m_MainWindow.m_MainCrtl.mKEYENCE_PLC.WriteRegister(addr, value);
            labelRegValue.Content = value;
        }
    }
}
