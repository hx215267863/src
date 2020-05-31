using ATL_MC.DAL.Model;
using ATL_MC.DAL.Service;
using IFactory.Common;
using IFactory.Domain.Common;
using IFactory.Domain.Models;
using IFactory.Platform.Common.Request.Product;
using IFactory.Platform.Common.Request.SystemParam;
using IFactory.Platform.Common.Response.Product;
using IFactory.Platform.Common.Response.SystemParam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;

namespace IFactory.UI.SystemParamManager
{
    /// <summary>
    /// UserAddDialog.xaml 的交互逻辑
    /// </summary>
    public partial class ProductParamAddDialog : Window, IComponentConnector
    {
        public ProductParamAddDialog(ProductDto  productdto, bool insert )
        {
            InitializeComponent();

            this.DataContext = this;
            inset = insert;
            aaa = productdto;
        }

        private ProductDto model;
        public string ITEM_CD { get; set; }

        public ProductDto aaa = null;

        public bool inset = true; 

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        public ProductService _productService=new ProductService ();


        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (this.model == null)
                return;
            if (string.IsNullOrEmpty(this.model.ITEM_CD))
            {
                MessageBox.Show("请输入产品编码", "提示");
            }
            else if (string.IsNullOrEmpty(this.model.ITEM_NM))
            {
                MessageBox.Show("请输入产品名称", "提示");
            }
            else if (string.IsNullOrEmpty(this.model.MODEL_CD))
            {
                MessageBox.Show("请输入产品型号", "提示");
            }
            else if (string.IsNullOrEmpty(this.model.ITEM_COLOR))
            {
                MessageBox.Show("请输入产品颜色", "提示");
            }
            else if (string.IsNullOrEmpty(this.model.ITEM_HEIGHT.ToString()))
            {
                MessageBox.Show("请输入产品规格长", "提示");
            }
            else if (string.IsNullOrEmpty(this.model.ITEM_WIDTH.ToString()))
            {
                MessageBox.Show("请输入产品规格宽", "提示");
            }
            else if (string.IsNullOrEmpty(this.model.QTY_FOR_CRIB.ToString()))
            {
                MessageBox.Show("请输入产品每垛的单位数", "提示");
            }
            else if (string.IsNullOrEmpty(this.model.QTY_FOR_TRAY.ToString()))
            {
                MessageBox.Show("请输入产品每Tary盘单位数", "提示");
            }
            else if (string.IsNullOrEmpty(this.model.MoveInLight_1.ToString()))
            {
                MessageBox.Show("请输入拉带亮度1", "提示");
            }
            else if (string.IsNullOrEmpty(this.model.MoveInLight_2.ToString()))
            {
                MessageBox.Show("请输入拉带亮度2", "提示");
            }
            else
            {

                if (inset)
                {
                    model.ID = Guid.NewGuid().ToString();
                    _productService.InsertProductInfo(model);
                }
                else
                {
                    _productService.UpdateProductInfo(model);
                }
              
              

                //ProductParamSaveResponse productParamSaveResponse = LocalApi.Execute(
                //    new ProductParamSaveRequest()
                //    {
                //        Insert = inset,
                //        ITEM_CD = this.model.ITEM_CD,
                //        ITEM_NM = this.model.ITEM_NM,
                //        ITEM_DESC = this.model.ITEM_DESC,
                //        MODEL_CD = this.model.MODEL_CD,
                //        MODEL_NM = this.model.MODEL_NM,
                //        ITEM_COLOR = this.model.ITEM_COLOR,
                //        ITEM_LONG = this.model.ITEM_LONG,
                //        ITEM_WIDE = this.model.ITEM_WIDE,
                //        LIGHT_BRIGHT = this.model.LIGHT_BRIGHT,
                //        QTY_FOR_CRIB = this.model.QTY_FOR_CRIB,
                //        QTY_FOR_TARY = this.model.QTY_FOR_TARY,
                //        MO = this.model.MO
                //    }
                //);
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
                this.model = new ProductDto();
            }
            this.DataContext = this.model;

            this.ddlBATTERY_COLOR.ItemsSource = BATTERY_COLOR.银色.ToArrayList();
        }
    }
}
