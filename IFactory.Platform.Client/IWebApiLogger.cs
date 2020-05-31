namespace IFactory.Platform.Client
{
  public interface IWebApiLogger
  {
    void Error(string message);

    void Warn(string message);

    void Info(string message);
  }
}
