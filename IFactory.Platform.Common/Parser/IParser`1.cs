namespace IFactory.Platform.Common.Parser
{
    public interface IParser<T>
    {
        T Parse(string body);
    }
}
