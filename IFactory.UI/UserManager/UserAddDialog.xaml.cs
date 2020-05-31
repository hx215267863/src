using IFactory.Common;
using IFactory.Domain.Common;
using IFactory.Domain.Models;
using IFactory.Platform.Common.Request.Product;
using IFactory.Platform.Common.Request.User;
using IFactory.Platform.Common.Response.Product;
using IFactory.Platform.Common.Response.User;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;

namespace IFactory.UI.UserManager
{
    /// <summary>
    /// UserAddDialog.xaml 的交互逻辑
    /// </summary>
    public partial class UserAddDialog : Window, IComponentConnector
    {
        public UserAddDialog()
        {
            InitializeComponent();

            this.DataContext = this;
        }

        public class CraftStateItem
        {
            public int CraftDID { get; set; }
            public bool Selected { get; set; }
            public string CraftName { get; set; }
        }

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
            if (string.IsNullOrEmpty(this.model.UserName))
            {
                MessageBox.Show("请输入用户名", "提示");
            }
            else if (string.IsNullOrEmpty(this.model.Name))
            {
                MessageBox.Show("请输入姓名", "提示");
            }
            else if (string.IsNullOrEmpty(this.model.Password))
            {
                MessageBox.Show("请输入密码", "提示");
            }
            else if (this.model.RoleId == 0)
            {
                MessageBox.Show("请选择角色", "提示");
            }
            else
            {
                UserSaveResponse userSaveResponse = LocalApi.Execute(
                    new UserSaveRequest() {
                    Gender = this.model.Gender,
                    Name = this.model.Name,
                    Password = this.model.Password,
                    RoleId = this.model.RoleId,
                    UserId = this.model.UserId,
                    UserName = this.model.UserName,
                    CraftDIDs = string.Join<int>(",", this.craftStates.Where<UserAddDialog.CraftStateItem>(m => m.Selected).Select<UserAddDialog.CraftStateItem, int>(m => m.CraftDID)) }
                );
                this.DialogResult = new bool?(true);
                this.Close();
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
            this.ddlGender.ItemsSource = Gender.Female.ToArrayList();
            this.ddlRole.ItemsSource = roleListResponse.Roles;
            if (this.UserId > 0)
            {
                this.model = (LocalApi.Execute(new UserGetRequest()
                {
                    UserId = this.UserId
                })).User;
                this.dialogTitle.Content = "修改用户";
            }
            else
            {
                this.dialogTitle.Content = "添加用户";
                this.model = new UserModel();
            }
            CraftListResponse craftListResponse = LocalApi.GetCraftsList(new CraftListRequest());
            int[] numArray;
            if (!string.IsNullOrEmpty(this.model.CraftDIDs))
                numArray = ((IEnumerable<string>)this.model.CraftDIDs.Split(',')).Select<string, int>(m => int.Parse(m)).ToArray<int>();
            else
                numArray = new int[0];
            int[] craftDIDs = numArray;
            this.craftStates = craftListResponse.Crafts.Select<CraftModel, UserAddDialog.CraftStateItem>(m =>
            {
                CraftStateItem craftStateItem = new UserAddDialog.CraftStateItem();
                craftStateItem.CraftDID = m.CraftDID;
                craftStateItem.CraftName = m.CraftName;
                int num = ((IEnumerable<int>)craftDIDs).Contains<int>(m.CraftDID) ? 1 : 0;
                craftStateItem.Selected = num != 0;
                return craftStateItem;
            }).ToList();
            this.lstCrafts.ItemsSource = this.craftStates;
            this.DataContext = this.model;
        }
    }
}
