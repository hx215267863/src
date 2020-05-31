using IFactory.Platform.Common.Response.User;

namespace IFactory.Platform.Common.Request.User
{
    public class PersonalInfoUpdateRequest : BaseRequest<PersonalInfoUpdateResponse>
    {
        public override string ApiName
        {
            get
            {
                return "personal.info.update";
            }
        }

        public int UserId { get; set; }

        public string Name { get; set; }

        public IFactory.Domain.Common.Gender? Gender { get; set; }
    }
}
