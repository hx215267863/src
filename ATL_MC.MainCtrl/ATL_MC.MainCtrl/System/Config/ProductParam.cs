using ATL_MC.EpsonScaraRobotController;
using ATL_MC.MainCtrl.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATL_MC.MainCtrl.System
{

    /// <summary>
    /// 当前运行产品的换型参数,数据库中内容
    /// </summary>
    public class ProductParam : BaseStatus
    {
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

        public List<ProductSLotInfo> ProductSlotInfos { get; set; } = new List<ProductSLotInfo>();

        /// <summary>
        /// 查询产品槽位信息
        /// </summary>
        /// <param name="trayType">空tray类别</param>
        /// <param name="slot">槽位</param>
        /// <returns></returns>
        public ProductSLotInfo ProductInfo(EnumTrayType enumTrayType, int slot)
        {
            var product = this.ProductSlotInfos.Where(p => p.SLOT_TY == (int)enumTrayType && p.SLOT_SITE == slot).FirstOrDefault();

            //TODO：测试用
            product = product == null ? new ProductSLotInfo() : product;
            return product;
        }

    }



    public class ProductSLotInfo : BaseStatus
    {
        /// <summary>
        /// ID
        /// </summary>
        public string ID { get; set; }
        ///// <summary>
        ///// 产品编码
        ///// </summary>
        //public string ITEM_CD { get; set; }
        /// <summary>
        /// 槽位类型,5个类型tray盘A、B、C、D、E
        /// </summary>
        public int SLOT_TY { get; set; }
        /// <summary>
        /// 槽位位置
        /// </summary>
        public int SLOT_SITE { get; set; }

        #region 机械手示教坐标

        public SixAxisPose RobotPose_Put { get; set; }

        
        #endregion

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

    }
}
