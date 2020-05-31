using IFactory.Platform.Common.Response.User;

namespace IFactory.Platform.Common.Request.User
{
    public class PermissionOrderRequest : BaseRequest<PermissionOrderResponse>
    {
        public override string ApiName
        {
            get
            {
                return "permission.order";
            }
        }

        public int PermissionId { get; set; }

        public PermissionOrderRequest.DirectionType Direction { get; set; }

        public enum DirectionType
        {
            Up,
            Down,
        }
    }
}
