namespace IFactory.Platform.Common.Request
{
    public interface IUploadRequest<T> : IRequest<T>, IUploadRequest where T : BaseResponse
    {
    }
}
