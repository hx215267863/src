namespace IFactory.Platform.Common.Request
{
  public interface IRequest<out T> where T : IResponse
  {
    string ApiName { get; }

    void Validate();

    string GetParamJson();
  }
}
