using IFactory.Common;
using IFactory.Domain.Common;
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
using IFactory.UI.Core;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Linq;
using IFactory.UI;

namespace IFactory.UI.UserManager
{
    /// <summary>
    /// UserCheckDialog.xaml 的交互逻辑
    /// </summary>
    public partial class FactoryCheckDialog : Window, IComponentConnector
    {
        static string m_WebApiUrl = "http://172.17.135.210:8899";
        public FactoryCheckDialog()
        {
            InitializeComponent();

            this.DataContext = this;
            EndProductNo = this.endproductno.Text;
        }

        public class authorization
        {
            public string Code { get; set; }
            public string Msg { get; set; }
            public string Result { get; set; }
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

        public string FactoryIDs { get; set; }
       public string fanos { get; set; }
       static public string EndProductNo { get; set; }


        private UserModel model;
        public List<CraftStateItem> craftStates;
        public int UserId { get; set; }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (this.model == null)
                return;
            if (string.IsNullOrEmpty(this.factoryID.Text))
            {
                MessageBox.Show("请输入工厂编号", "提示");
            }
            else if (string.IsNullOrEmpty(this.fano.Text))
            {
                MessageBox.Show("请输入设备编号", "提示");
            }
            else if (string.IsNullOrEmpty(this.endproductno.Text))
            {
                MessageBox.Show("请输入成品编号", "提示");
            }
            else
            {
                FactoryIDs = this.factoryID.Text;
                fanos = this.fano.Text;
                EndProductNo = this.endproductno.Text;
                string result = Test_BIS_INS_TransfINSBaseData(FactoryIDs, fanos, EndProductNo);
                Console.WriteLine(result);
                JavaScriptSerializer js = new JavaScriptSerializer();
                authorization auth = js.Deserialize<authorization>(result);
                //authorization auth = (authorization)JsonConvert.DeserializeObject(result, typeof(authorization));
                string a = auth.Result;
                if (auth.Code != "0")
                {
                    UserSaveResponse userSaveResponse = LocalApi.factoryInfo(
                    new UserSaveRequest()
                    {
                       factoryID = this.factoryID.Text,
                       fano = this.fano.Text,
                       end_product_no = this.endproductno.Text
                    }
                    );
                    UserCheckDialog userCheck = new UserCheckDialog();
                    userCheck.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("不存在", "提示");
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

        public static string Test_BIS_INS_TransfINSBaseData(string FactoryIDs,string fanos,string EndPorNo)
        {
            string requestUri = string.Concat(m_WebApiUrl, "/WIP/WipGetDevice");

            List<Domain.Model.INSBaseCellData> list = new List<Domain.Model.INSBaseCellData>();
            Domain.Model.INSBaseCellData data = new Domain.Model.INSBaseCellData()
            {
                factoryID = FactoryIDs,
                fano = fanos,
                end_product_no = EndPorNo
            };
            Console.WriteLine(Utility.Http.HttpClient.Post(requestUri, data));
            string result = Utility.Http.HttpClient.Post(requestUri, data);
            return result;
        }

    }
}
