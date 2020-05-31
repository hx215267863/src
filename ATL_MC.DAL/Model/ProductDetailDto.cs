using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATL_MC.DAL.Model
{
    public class ProductDetailDto
    {
        public int RowID { get; set; }
        /// <summary>
        /// ID
        /// </summary>
        public string ID { get; set; }
        public string productid { get; set; }

        #region 主表数据

        /// <summary>
        /// 产品编码
        /// </summary>
        public string ITEM_CD { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ITEM_NM { get; set; }

        /// <summary>
        /// 产品型号
        /// </summary>
        public string MODEL_CD { get; set; }


        #endregion

        /// <summary>
        /// 槽位类型,5个类型tray盘A、B、C、D、E
        /// </summary>
        public int SLOT_TY { get; set; }
        /// <summary>
        /// 槽位位置
        /// </summary>
        public int SLOT_SITE { get; set; }
        public double SLOT_xAxis { get; set; }
        public double SLOT_yAxis { get; set; }
        public double SLOT_zAxis { get; set; }
        public double SLOT_rxAxis { get; set; }
        public double SLOT_ryAxis { get; set; }
        public double SLOT_rzAxis { get; set; }
        public string SLOT_Fig { get; set; }
        public string SLOT_Remark { get; set; }

        /// <summary>
        /// 光源亮度1
        /// </summary>
        public int Brightness_1 { get; set; }
        /// <summary>
        /// 光源亮度2
        /// </summary>
        public int Brightness_2 { get; set; }
        /// <summary>
        /// 光源亮度3
        /// </summary>
        public int Brightness_3 { get; set; }
        /// <summary>
        /// 光源亮度4
        /// </summary>
        public int Brightness_4 { get; set; }
        public DateTime UPT_DT { get; set; }
        public DateTime CRT_DT { get; set; }
    }
}
