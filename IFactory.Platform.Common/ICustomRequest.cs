namespace IFactory.Platform.Common
{
    public interface ICustomRequest<TReponse> where TReponse : BaseResponse
    {
        TReponse PareseResponse(string json);
    }
}
