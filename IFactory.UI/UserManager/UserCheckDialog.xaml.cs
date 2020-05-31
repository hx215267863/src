using IFactory.Domain.Models;
using IFactory.Platform.Common.Request.Product;
using IFactory.Platform.Common.Request.User;
using IFactory.Platform.Common.Response.Product;
using IFactory.Platform.Common.Response.User;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;
using System;
using Kel.IFactory.Mqtt.Client;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using IFactory.UI.Core;
using System.Linq;
using System.Threading;
using System.Windows.Threading;
using System.Runtime.InteropServices;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
using System.IO;

namespace IFactory.UI.UserManager
{
    /// <summary>
    /// UserCheckDialog.xaml 的交互逻辑
    /// </summary>
    public partial class UserCheckDialog : Window, IComponentConnector
    {
        IFactoryMqttClient _ifactoryClient = new IFactoryMqttClient();

        [DllImport("kernel32")]
        private static extern long GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        public UserCheckDialog()
        {
            InitializeComponent();
            _ifactoryClient.OnReceiveResult += _ifactoryClient_OnReceiveResult;
            MainWindow.m_MainWindow.userCheckFlag = false;
            this.DataContext = this;
            StringBuilder EndProductNo_temp = new StringBuilder(1024);
            StringBuilder ProductNo_temp = new StringBuilder(1024);
            StringBuilder DeviceGroupDID_temp = new StringBuilder(1024);
            string filePath = new DirectoryInfo(@"../dbinfo.ini").FullName;
            readIni("UserVerify", "EndProductNo", "no key", EndProductNo_temp, 1024, filePath);
            readIni("UserVerify", "ProductNo", "no key", ProductNo_temp, 1024, filePath);
            readIni("IFactoryMqtt", "deviceGroupDID", "no key", DeviceGroupDID_temp, 1024, filePath);

            this.ENDPRODUCTNO.Text = EndProductNo_temp.ToString();
            this.PRODUCTNO.Text = ProductNo_temp.ToString();
            //连接中控
            if (!_ifactoryClient.IsOpen)
            {
                BaseResult rst = _ifactoryClient.Connect(DeviceGroupDID_temp.ToString());
                if (!_ifactoryClient.IsOpen)
                {
                    MessageBox.Show("连接中控失败！！！", "失败");
                    this.Close();
                }
            }
            else
            {
                _ifactoryClient.Close();
            }
        }

        private void _ifactoryClient_OnReceiveResult(RequestResultPair args)
        { 
            ThreadPool.QueueUserWorkItem(o =>
            {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.SystemIdle, new Action<RequestResultPair>((RequestResultPair rst) =>
                {
                    string msg = args.Result != null ? args.Result.msg : ""; 
                }), args);

            });
            throw new NotImplementedException();
        }

        public class authorization
        {
            public string Code { get; set; }
            public string Msg { get; set; }
            public Data Data { get; set; }
        }

        public class Data
        {
            public string Result { get; set; }
        }

        public class CraftStateItem
        {
            public int CraftDID { get; set; }
            public bool Selected { get; set; }
            public string CraftName { get; set; }
        }

        public class JsonData
        {
            //读取
            public string AssetsNO { get; set; }            //资产编号  P9-2724
            public string ProductLineNO { get; set; }       //产线编号  M5-3F-S-STF01
            public string DeviceGroupDID { get; set; }       //设备组ID    COL#1

            //输入
            public string ProductNO { get; set; }       //产品型号
            public string EndProductNO { get; set; }    //成品编码
            public string CraftWork { get; set; }      //工艺
            public string Process { get; set; }         //工序
            public string FactoryID { get; set; }       //工厂编号
            public string Segment { get; set; }        //岗位名称
            public string Quarters { get; set; }        //工段
            public string StaffID { get; set; }         //工号
        }

        private JsonData json_input = new JsonData() { };
        private UserModel model;
        public List<CraftStateItem> craftStates;
        public int UserId { get; set; }

        static public string sendStaffID { get; set; }

        static public string btnStatus { get; set; }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder AssetsNO_temp = new StringBuilder(1024);
            StringBuilder ProductLineNO_temp = new StringBuilder(1024);
            StringBuilder DeviceGroupDID_temp = new StringBuilder(1024);
            string filePath = new DirectoryInfo(@"../dbinfo.ini").FullName;
            readIni("IFactoryMqtt", "assetsNO", "no key", AssetsNO_temp, 1024, filePath);
            readIni("IFactoryMqtt", "productLineNO", "no key", ProductLineNO_temp, 1024, filePath);
            readIni("IFactoryMqtt", "deviceGroupDID", "no key", DeviceGroupDID_temp, 1024, filePath);

            RequestResultPair rst = null;
            BaseResult req_rst = null;
            if (this.model == null)
                return;
            if (string.IsNullOrEmpty(this.PRODUCTNO.Text))
            {
                MessageBox.Show("请输入产品型号", "提示");
            }
            else if (string.IsNullOrEmpty(this.ENDPRODUCTNO.Text))
            {
                MessageBox.Show("请输入成品编码", "提示");
            }
            else if (string.IsNullOrEmpty(this.CRAFTWORK.Text))
            {
                MessageBox.Show("请输入工艺", "提示");
            }
            else if (string.IsNullOrEmpty(this.PROCESS.Text))
            {
                MessageBox.Show("请输入工序", "提示");
            }
            else if (string.IsNullOrEmpty(this.FACTORYID.Text))
            {
                MessageBox.Show("请输入工厂编号", "提示");
            }
            else if (string.IsNullOrEmpty(this.QUARTERS.Text))
            {
                MessageBox.Show("请输入岗位名称", "提示");
            }
            else if (string.IsNullOrEmpty(this.SEGMENT.Text))
            {
                MessageBox.Show("请输入工段", "提示");
            }
            else if (string.IsNullOrEmpty(this.model.staffid))
            {
                MessageBox.Show("请输入工号", "提示");
            }
            else
            {
                json_input.AssetsNO = AssetsNO_temp.ToString();//"P9-2724";
                json_input.ProductLineNO = ProductLineNO_temp.ToString();// "M5-3F-S-STF01";
                json_input.DeviceGroupDID = DeviceGroupDID_temp.ToString();// "COL#1";
                json_input.ProductNO = this.PRODUCTNO.Text;
                json_input.EndProductNO = this.ENDPRODUCTNO.Text;
                json_input.CraftWork = this.CRAFTWORK.Text;
                json_input.Process = this.PROCESS.Text;
                json_input.FactoryID = this.FACTORYID.Text;
                json_input.Segment = this.SEGMENT.Text;
                json_input.Quarters = this.QUARTERS.Text;
                json_input.StaffID = this.model.staffid;
                sendStaffID = this.model.staffid;

                List<JsonData> list = new List<JsonData>();
                JsonData data = new JsonData()
                {
                  AssetsNO = json_input.AssetsNO,
                  ProductLineNO= json_input.ProductLineNO,
                  DeviceGroupDID= json_input.DeviceGroupDID,
                  ProductNO= json_input.ProductNO,
                  EndProductNO= json_input.EndProductNO,
                  CraftWork= json_input.CraftWork,
                  Process= json_input.Process,
                  FactoryID= json_input.FactoryID,
                  Segment = json_input.Segment,
                  Quarters = json_input.Quarters,
                  StaffID= json_input.StaffID
                };
                string jsconDatastr = JsonConvert.SerializeObject(data);
                req_rst = _ifactoryClient.RequstWithResult("produce.device.startup","validate", jsconDatastr, true,out rst);
                if(!File.Exists(@".\log.txt"))
                {
                    FileStream fs = new FileStream(@".\log.txt",FileMode.Create,FileAccess.Write);
                    StreamWriter sw = new StreamWriter(fs);
                    string msg = "MSG:" + req_rst.msg + "\r\nResult:" + rst.Result.ToString() + "\r\nJson:" + jsconDatastr;
                    sw.WriteLine(msg);
                    sw.Close();
                    fs.Close();
                }
                else
                {
                    FileStream fs = new FileStream(@".\log.txt", FileMode.Open, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(fs);
                    string msg = "MSG:" + req_rst.msg + "\r\nResult:" + rst.Result.ToString() + "\r\nJson:" + jsconDatastr;
                    sw.WriteLine(msg);
                    sw.Close();
                    fs.Close();
                }
                if (req_rst.code == 0)
                {
                    MainWindow.m_MainWindow.userCheckFlag = true;
                    OpenStatus();
                    btnStatus = "注销";
                    writeIni("UserVerify", "EndProductNo", json_input.EndProductNO, filePath);
                    writeIni("UserVerify", "ProductNo", json_input.ProductNO, filePath);
                    //LoginResponse loginResponse = LocalApi.Execute(new LoginRequest()
                    //{
                    //    UserName = "admin",
                    //    Password = "admin"

                    //});
                    //AppContext.Current.Reset();
                    //AppContext.Current.Name = loginResponse.Name;
                    //AppContext.Current.UserId = loginResponse.UserId;
                    //if (!string.IsNullOrEmpty(loginResponse.PermissionCodes))
                    //    AppContext.Current.PermissionCodes.AddRange(loginResponse.PermissionCodes.Split(','));
                    //if (!string.IsNullOrEmpty(loginResponse.CraftDIDs))
                    //    AppContext.Current.CraftDIDs.AddRange(((IEnumerable<string>)loginResponse.CraftDIDs.Split(',')).Select<string, int>(m => int.Parse(m)));
                    //AppContext.Current.Name = json_input.StaffID;
                    //MainWindow mainWindow = new MainWindow();
                    //mainWindow.Show();
                    //Application.Current.MainWindow = mainWindow;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("不存在此用户", "提示");
                    //this.Close();
                }
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!new Rect(new Point(20.0, 12.0), new Size(390.0, 28.0)).Contains(e.GetPosition(this))
                || e.ChangedButton != MouseButton.Left)
                return;
            this.DragMove();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RoleListResponse roleListResponse = LocalApi.Execute(new RoleListRequest() { PageNumber = 1, PageSize = int.MaxValue });
            //this.CRAFTWORK.ItemsSource = CraftWork.M6S.ToArrayList();
            //this.PROCESS.ItemsSource = Process.最终目检.ToArrayList();
            //this.QUARTERS.ItemsSource = Quarters.手动包胶.ToArrayList();
            //this.SEGMENT.ItemsSource = Segment.后工序.ToArrayList();
            this.model = new UserModel();
            CraftListResponse craftListResponse = LocalApi.GetCraftsList(new CraftListRequest());
            this.DataContext = this.model;
        }

        public void readIni(string section, string key, string def, StringBuilder retVal, int size, string filePath)
        {
            GetPrivateProfileString(section, key, def, retVal, size, filePath);
            //return;
        }

        public void writeIni(string section, string key, string val, string filePath)
        {
            WritePrivateProfileString(section, key, val, filePath);
            return;
        }

        public void OpenStatus()
        {
            DateTime now = DateTime.Now;
            string connectionString_ATL = @"server=127.0.0.1;database=IFactory;uid=root;pwd=root;Persist Security Info=True;Charset=utf8;";
            MySqlConnection connatl = new MySqlConnection(connectionString_ATL);
            connatl.Open();
            string sql = "";
            sql = " update If_Ifactory_Client_Open set Time = Now(), Flag = 1; ";
            MySqlDataAdapter b = new MySqlDataAdapter(sql, connatl);
            DataTable bt = new DataTable();
            b.Fill(bt);
            connatl.Close();
        }

    }
}
