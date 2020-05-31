using IFactory.Platform.Common;
using IFactory.Platform.Common.Request;
using System.Threading.Tasks;

namespace IFactory.Platform.Client
{
    public interface IWebApiClient
    {
        T Execute<T>(IRequest<T> request) where T : BaseResponse;

        T Execute<T>(IRequest<T> request, string session) where T : BaseResponse;

        Task<T> ExecuteAsync<T>(IRequest<T> request) where T : BaseResponse;

        Task<T> ExecuteAsync<T>(IRequest<T> request, string session) where T : BaseResponse;
    }
}
