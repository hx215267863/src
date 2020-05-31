namespace IFactory.Platform.Common
{
    public interface IResponse
    {
        string ErrCode { get; set; }

        string ErrMsg { get; set; }
    }
}
