using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATL_MC.DAL.Model
{
    public class ProductDto
    {
        public int RowID { get; set; }
        /// <summary>
        /// ID
        /// </summary>
        public string ID { get; set; }
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

        /// <summary>
        /// 产品颜色 
        /// </summary>
        public string ITEM_COLOR { get; set; }
        /// <summary>
        /// 产品规格长度
        /// </summary>
        public int ITEM_HEIGHT { get; set; }
        /// <summary>
        /// 产品规格宽度
        /// </summary>
        public int ITEM_WIDTH { get; set; }
        /// <summary>
        /// 每跺单位数
        /// </summary>
        public int QTY_FOR_CRIB { get; set; }
        /// <summary>
        /// 每盘列数,未知用途,先保留
        /// </summary>
        public int QTY_FOR_Columns { get; set; } //新增
        /// <summary>
        /// 每盘单位数
        /// </summary>
        public int QTY_FOR_TRAY { get; set; }
        /// <summary>
        /// 拉带光源1亮度,不同产品,需要不同亮度
        /// </summary>
        public int MoveInLight_1 { get; set; }
        /// <summary>
        /// 拉带光源2亮度,不同产品,需要不同亮度
        /// </summary>
        public int MoveInLight_2 { get; set; }

        public string Remark { get; set; }
        public DateTime UPT_DT { get; set; }
        public DateTime CRT_DT { get; set; }

        

    }
}
