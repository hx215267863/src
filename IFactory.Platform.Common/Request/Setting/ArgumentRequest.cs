using IFactory.Platform.Common.Response.Setting;
using System;

namespace IFactory.Platform.Common.Request.Setting
{
    public class ArgumentRequest : BaseRequest<ArgumentResponse>
    {
        public override string ApiName
        {
            get
            {
                return "Set.Argument.list";
            }
        }

        public int? ProcessDID { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }
    }
}
