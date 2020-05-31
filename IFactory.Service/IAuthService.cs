using IFactory.Domain.Entities;

namespace IFactory.Service
{
    public interface IAuthService : IBaseService<AuthInfo>
    {
        AuthInfo GetAuth(string accessToken);
    }
}
