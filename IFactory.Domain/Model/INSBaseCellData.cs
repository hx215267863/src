using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFactory.Domain.Model
{
    /// <summary>
    /// PIEF DBF 文件基础资料
    /// </summary>
    public class INSBaseCellData
    {
        public string CRAFTWORK { get; set; }
        /// <summary>
        /// PROCESS
        /// </summary>
        public string PROCESS { get; set; }
        /// <summary>
        /// QUARTERS
        /// </summary>
        public string QUARTERS { get; set; }
        /// <summary>
        /// SEGMENT
        /// </summary>
        public string SEGMENT { get; set; }
        /// <summary>
        /// STAFFID
        /// </summary>
        public string STAFF_ID { get; set; }
        /// <summary>
        /// BARCODE
        /// </summary>
        public string CellName { get; set; }

        public string factoryID { get; set; }
        public string fano { get; set; }
        public string end_product_no { get; set; }
    }
}
