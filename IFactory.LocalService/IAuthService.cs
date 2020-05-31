using IFactory.Domain.Entities;

namespace IFactory.LocalService
{
    public interface IAuthService : IBaseService<AuthInfo>
    {
        AuthInfo GetAuth(string accessToken);
    }
}
