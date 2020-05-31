using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATL_MC.MainCtrl.Enum
{
   public enum EnumBarcodeScanner_ResultType
    {
        /// <summary>
        /// 扫码通过
        /// </summary>
        OK,
        /// <summary>
        /// 扫码返回结果有误
        /// </summary>
        Err,
        /// <summary>
        /// 扫码失败
        /// </summary>
        Fail

    }
}
