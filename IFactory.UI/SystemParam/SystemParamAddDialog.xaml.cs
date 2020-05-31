using ATL_MC.DAL.Model;
using ATL_MC.DAL.Service;
using IFactory.Common;
using IFactory.Domain.Common;
using IFactory.Domain.Models;
using IFactory.Platform.Common.Request.SystemParam;
using IFactory.Platform.Common.Response.SystemParam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;

namespace IFactory.UI.SystemParam
{
    /// <summary>
    /// UserAddDialog.xaml 的交互逻辑
    /// </summary>
    public partial class SystemParamAddDialog : Window, IComponentConnector
    {
        public SystemParamAddDialog(ProductDetailDto detail, bool _insert)
        {
            InitializeComponent();

            this.DataContext = this;
            aaa = detail;
            insert = _insert;
        }

        private ProductDetailDto model;

        public ProductDetailDto aaa = null;

        public bool insert = true;
        public string ITEM_CD { get; set; }
        public int IDX { get; set; }
        public ProductService _productService = new ProductService();

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.model.ITEM_CD))
            {
                MessageBox.Show("请输入产品编码", "提示");
            }
            if (string.IsNullOrEmpty(this.model.ITEM_NM))
            {
                MessageBox.Show("请输入产品编码", "提示");
            }
            else if (string.IsNullOrEmpty(this.model.SLOT_TY.ToString()))
            {
                MessageBox.Show("请输入槽位类型", "提示");
            }
            else if (string.IsNullOrEmpty(this.model.SLOT_SITE.ToString()))
            {
                MessageBox.Show("请输入槽位位置", "提示");
            }
            else if (string.IsNullOrEmpty(this.model.SLOT_xAxis.ToString()))
            {
                MessageBox.Show("请输入槽位x坐标", "提示");
            }
            else if (string.IsNullOrEmpty(this.model.SLOT_yAxis.ToString()))
            {
                MessageBox.Show("请输入槽位y坐标", "提示");
            }
            else if (string.IsNullOrEmpty(this.model.SLOT_zAxis.ToString()))
            {
                MessageBox.Show("请输入槽位z坐标", "提示");
            }
            else if (string.IsNullOrEmpty(this.model.SLOT_rxAxis.ToString()))
            {
                MessageBox.Show("请输入槽位rx坐标", "提示");
            }
            else if (string.IsNullOrEmpty(this.model.SLOT_ryAxis.ToString()))
            {
                MessageBox.Show("请输入槽位ry坐标", "提示");
            }
            else if (string.IsNullOrEmpty(this.model.SLOT_rzAxis.ToString()))
            {
                MessageBox.Show("请输入槽位rz坐标", "提示");
            }
            else if (string.IsNullOrEmpty(this.model.SLOT_Fig))
            {
                MessageBox.Show("请输入槽位fig值", "提示");
            }
            else if (string.IsNullOrEmpty(this.model.Brightness_1.ToString()))
            {
                MessageBox.Show("请输入光源1", "提示");
            }
            else if (string.IsNullOrEmpty(this.model.Brightness_2.ToString()))
            {
                MessageBox.Show("请输入光源2", "提示");
            }
            else if (string.IsNullOrEmpty(this.model.Brightness_3.ToString()))
            {
                MessageBox.Show("请输入光源3", "提示");
            }
            else if (string.IsNullOrEmpty(this.model.Brightness_4.ToString()))
            {
                MessageBox.Show("请输入光源4", "提示");
            }
            else
            {
                if (insert)
                {
                    model.ID = Guid.NewGuid().ToString();
                    _productService.InsertProductdetailInfo(model);
                }
                else
                {
                    _productService.UpdateProductDetail(model);
                }
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
            if (aaa != null)
            {
                this.model = aaa;
            }
            else
            {
                this.model = new ProductDetailDto();
            }

            this.DataContext = this.model;




            var list = _productService.GetAllProductNum();
            var list1 = list.Select<ProductDto, ComboBoxItem>(p => new ComboBoxItem { Key = p.ITEM_CD, Value = p.ID }).ToList();
            this.ddlITEM_CD.ItemsSource = list1;

            //ddlITEM_CD.SelectedItem
            // ddlITEM_CD.Items = List<>;
            this.ddlSLOT_SITE.ItemsSource = SLOT_SITE.一.ToArrayList();
          //  this.ddlSLOT_TY.ItemsSource = Enum_SLOT_TY.TrayA.ToArrayList();

        }

        private void btnGetPos_Click(object sender, RoutedEventArgs e)
        {
            double x, y, z, u;
            //TODO:获取机械手坐标
            //if( 0 == MainWindow.m_MainCtrl.mEpsonScaraRobot.GetCurPos(out x,out y,out z,out u))
            //{
            //    this.model.SLOT_X_DOT = x.ToString("0.000");
            //    this.model.SLOT_Y_DOT = y.ToString("0.000");
            //    this.model.SLOT_Z_DOT = (-135.0).ToString("0.000");
            //    this.model.SLOT_U_DOT = u.ToString("0.000");

            //    txtSLOT_X_DOT.Text = x.ToString("0.000");
            //    txtSLOT_Y_DOT.Text = y.ToString("0.000");
            //    txtSLOT_Z_DOT.Text = (-135.0).ToString("0.000");
            //    txtSLOT_U_DOT.Text = u.ToString("0.000");
            //}
        }

        private class ComboBoxItem
        {
            public string Key { get; set; }
            public string Value { get; set; }
        }
    }
}
