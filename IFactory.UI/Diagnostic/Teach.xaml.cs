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
    /// Teach.xaml 的交互逻辑
    /// </summary>
    public partial class Teach : BasePage, IComponentConnector
    {
       
        private string[] ArrDistance;
        private string[] ArrPosition;
        private int iValue;

        private double X;
        private double Y;
        private double Z;
        private double U;
        private double SZ;

        //ATL_MC.EpsonScaraRobotController.EpsonScaraRobotController scara = new ATL_MC.EpsonScaraRobotController.EpsonScaraRobotController();
        //ATL_MC.MainCtrl.SystemConfig config = new ATL_MC.MainCtrl.SystemConfig();
     

        public Teach()
        {
            InitializeComponent();
        }

        public void Page_Loaded(object sender, RoutedEventArgs e)
        {
            
            iValue = 0;
            ArrDistance = new string[] {"1", "10", "100"};
            ArrPosition = new string[] { "零位", "抓料位", "放料位"};
            //comboBox_Distance.ItemsSource = ArrDistance;
            //comboBox_Position.ItemsSource = ArrPosition;
        }

        //移动到自定义位置按
        private void button_MoveToPosition_Click(object sender, RoutedEventArgs e)
        {
            //if (string.IsNullOrEmpty(textBox_MoveX.Text) || string.IsNullOrEmpty(textBox_MoveY.Text) || string.IsNullOrEmpty(textBox_MoveZ.Text) || string.IsNullOrEmpty(textBox_MoveU.Text) || string.IsNullOrEmpty(textBox_MoveSZ.Text))
            //{
            //    MessageBox.Show("X，Y，Z，U，SZ均不能为空");
            //}
            //else
            //{
                //double doubleX = double.Parse(textBox_MoveX.Text);
                //double doubleY = double.Parse(textBox_MoveY.Text);
                //double doubleZ = double.Parse(textBox_MoveZ.Text);
                //double doubleU = double.Parse(textBox_MoveU.Text);
                //double doubleSZ = double.Parse(textBox_MoveSZ.Text);

                //scara.MoveToPos(doubleX, doubleY, doubleZ, doubleU, doubleSZ);
            //}
        }

        

        //增大X坐标按键
        private void button_AddX_Click(object sender, RoutedEventArgs e)
        {
            //if (string.IsNullOrEmpty(textBox_PositionX.Text))
            //{
            //    MessageBox.Show("X不能为空");
            //}
            //else
            //{
            //    textBox_PositionX.Text = (double.Parse(textBox_PositionX.Text) + iValue).ToString();
            //    X = double.Parse(textBox_PositionX.Text);
            //    //scara.MoveToPos(X, Y, Z, U, SZ);
            //}
        }

        //减小X坐标按键
        private void button_CutX_Click(object sender, RoutedEventArgs e)
        {
            //if (string.IsNullOrEmpty(textBox_PositionX.Text))
            //{
            //    MessageBox.Show("X不能为空");
            //}
            //else
            //{
            //    textBox_PositionX.Text = (double.Parse(textBox_PositionX.Text) - iValue).ToString();
            //    X = double.Parse(textBox_PositionX.Text);
            //    //scara.MoveToPos(X, Y, Z, U, SZ);
            //}
        }

        //增大Y坐标按键
        private void button_AddY_Click(object sender, RoutedEventArgs e)
        {
            //if (string.IsNullOrEmpty(textBox_PositionY.Text))
            //{
            //    MessageBox.Show("Y不能为空");
            //}
            //else
            //{
            //    textBox_PositionY.Text = (double.Parse(textBox_PositionY.Text) + iValue).ToString();
            //    Y = double.Parse(textBox_PositionY.Text);
            //    //scara.MoveToPos(X, Y, Z, U, SZ);
            //}
        }

        //减小Y坐标按键
        private void button_CutY_Click(object sender, RoutedEventArgs e)
        {
            //if (string.IsNullOrEmpty(textBox_PositionY.Text))
            //{
            //    MessageBox.Show("Y不能为空");
            //}
            //else
            //{
            //    textBox_PositionY.Text = (double.Parse(textBox_PositionY.Text) - iValue).ToString();
            //    Y = double.Parse(textBox_PositionY.Text);
            //    //scara.MoveToPos(X, Y, Z, U, SZ);
            //}
        }

        //增大Z坐标按键
        private void button_AddZ_Click(object sender, RoutedEventArgs e)
        {
            //if (string.IsNullOrEmpty(textBox_PositionZ.Text))
            //{
            //    MessageBox.Show("Z不能为空");
            //}
            //else
            //{
            //    textBox_PositionZ.Text = (double.Parse(textBox_PositionZ.Text) + iValue).ToString();
            //    Z = double.Parse(textBox_PositionZ.Text);
            //    //scara.MoveToPos(X, Y, Z, U, SZ);
            //}
        }

        //减小Z坐标按键
        private void button_CutZ_Click(object sender, RoutedEventArgs e)
        {
            //if (string.IsNullOrEmpty(textBox_PositionZ.Text))
            //{
            //    MessageBox.Show("Z不能为空");
            //}
            //else
            //{
            //    textBox_PositionZ.Text = (double.Parse(textBox_PositionZ.Text) - iValue).ToString();
            //    Z = double.Parse(textBox_PositionZ.Text);
            //    //scara.MoveToPos(X, Y, Z, U, SZ);
            //}
        }

        //增大U坐标按键
        private void button_AddU_Click(object sender, RoutedEventArgs e)
        {
            //if (string.IsNullOrEmpty(textBox_PositionU.Text))
            //{
            //    MessageBox.Show("U不能为空");
            //}
            //else
            //{
            //    textBox_PositionU.Text = (double.Parse(textBox_PositionU.Text) + iValue).ToString();
            //    SZ = double.Parse(textBox_PositionU.Text);
            //    //scara.MoveToPos(X, Y, Z, U, SZ);
            //}
        }

        //减小U坐标按键
        private void button_CutU_Click(object sender, RoutedEventArgs e)
        {
            //if (string.IsNullOrEmpty(textBox_PositionU.Text))
            //{
            //    MessageBox.Show("U不能为空");
            //}
            //else
            //{
            //    textBox_PositionU.Text = (double.Parse(textBox_PositionU.Text) - iValue).ToString();
            //    SZ = double.Parse(textBox_PositionU.Text);
            //    //scara.MoveToPos(X, Y, Z, U, SZ);
            //}
        }

        private void button_AddSZ_Click(object sender, RoutedEventArgs e)
        {
            //if (string.IsNullOrEmpty(textBox_PositionSZ.Text))
            //{
            //    MessageBox.Show("SZ不能为空");
            //}
            //else
            //{
            //    textBox_PositionSZ.Text = (double.Parse(textBox_PositionSZ.Text) + iValue).ToString();
            //    SZ = double.Parse(textBox_PositionSZ.Text);
            //    //scara.MoveToPos(X, Y, Z, U, SZ);
            //}
        }

        private void button_CutSZ_Click(object sender, RoutedEventArgs e)
        {
            //if(string.IsNullOrEmpty(textBox_PositionSZ.Text))
            //{
            //    MessageBox.Show("SZ不能为空");
            //}else
            //{
            //    textBox_PositionSZ.Text = (double.Parse(textBox_PositionSZ.Text) - iValue).ToString();
            //    SZ = double.Parse(textBox_PositionSZ.Text);
            //    //scara.MoveToPos(X, Y, Z, U, SZ);
            //}
        }

    


    }
}
