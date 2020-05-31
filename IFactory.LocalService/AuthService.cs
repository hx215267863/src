using IFactory.Data;
using IFactory.Domain.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace IFactory.LocalService
{
    public class AuthService : BaseService<AuthInfo>, IAuthService, IBaseService<AuthInfo>
    {
        public AuthService(IDatabaseFactory databaseFactory)
          : base(databaseFactory)
        {
        }

        public AuthInfo GetAuth(string accessToken)
        {
            return this.Table.Where(m => m.AccessToken == accessToken).FirstOrDefault();
        }
    }
}
