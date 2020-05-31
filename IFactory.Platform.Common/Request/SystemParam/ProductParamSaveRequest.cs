using IFactory.Platform.Common.Response.SystemParam;
using System;
namespace IFactory.Platform.Common.Request.SystemParam
{
    public class ProductParamSaveRequest : BaseRequest<ProductParamSaveResponse>
    {
        public override string ApiName
        {
            get
            {
                return "productParam.save";
            }
        }
        public bool Insert { get; set; }
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
    }
}
