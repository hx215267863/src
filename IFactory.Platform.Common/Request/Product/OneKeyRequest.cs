using IFactory.Platform.Common.Response.Product;
using System;

namespace IFactory.Platform.Common.Request.Product
{
    public class OneKeyRequest : BaseRequest<OneKeyResponse>
    {
        public override string ApiName
        {
            get
            {
                return "One.Key.list";
            }
        }

        public int OneKey_flag { get; set; }
    }
}
