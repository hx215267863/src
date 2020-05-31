using System;

namespace IFactory.Domain.Entities
{
    public class ProductParamInfo
    {
        public string ITEM_CD { get; set; }

        public string ITEM_NM { get; set; }

        public string ITEM_DESC { get; set; }

        public IFactory.Domain.Common.Gender? Gender { get; set; }

        public IFactory.Domain.Common.SizeMeas? Size { get; set; }




        public string MODEL_CD { get; set; }

        public string MODEL_NM { get; set; }

        public string ITEM_COLOR { get; set; }

        public string ITEM_LONG { get; set; }
        public string ITEM_WIDE { get; set; }
        public string LIGHT_BRIGHT { get; set; }
        public string QTY_FOR_CRIB { get; set; }
        public string QTY_FOR_TARY { get; set; }

        public string MO { get; set; }
        public string CRT_ID { get; set; }

        public DateTime CRT_DT { get; set; }
        public string UPT_ID { get; set; }

        public DateTime UPT_DT { get; set; }

        public string craftwork { get; set; }
        public string process { get; set; }
        public string quarters { get; set; }
        public string segments { get; set; }
        public string staffid { get; set; }

        public string factoryID { get; set; }
        public string fano { get; set; }
        public string end_product_no { get; set; }
    }
}
