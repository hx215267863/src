using IFactory.Platform.Common.Response.SystemParam;
using System;
namespace IFactory.Platform.Common.Request.SystemParam
{
    public class SystemParamSaveRequest : BaseRequest<SystemParamSaveResponse>
    {
        public override string ApiName
        {
            get
            {
                return "productParam.save";
            }
        }
        public bool Insert { set; get; }
        public int IDX { get; set; }

        public string ITEM_CD { get; set; }

        public string SLOT_TY { get; set; }

        public string SLOT_SITE { get; set; }

        public IFactory.Domain.Common.Gender? Gender { get; set; }

        public IFactory.Domain.Common.SizeMeas? Size { get; set; }

        public string SLOT_X_DOT { get; set; }

        public string SLOT_Y_DOT { get; set; }

        public string SLOT_Z_DOT { get; set; }

        public string SLOT_U_DOT { get; set; }

        public string LIGHT_1 { get; set; }
        public string LIGHT_2 { get; set; }
        public string LIGHT_3 { get; set; }
        public string LIGHT_4 { get; set; }


        public string MO { get; set; }
        public string CRT_ID { get; set; }

        public DateTime CRT_DT { get; set; }
        public string UPT_ID { get; set; }

        public DateTime UPT_DT { get; set; }
    }
}
