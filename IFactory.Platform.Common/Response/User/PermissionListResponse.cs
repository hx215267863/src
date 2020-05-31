using IFactory.Domain.Models;
using System.Collections.Generic;

namespace IFactory.Platform.Common.Response.User
{
    public class PermissionListResponse : BaseResponse
    {
        public IList<PermissionModel> Permissions { get; set; }
    }
}
