namespace IFactory.UI.Core
{
    public class LocalConfig
    {
        public LocalConfig.ClientTypeEnum ClientType { get; set; }

        public enum ClientTypeEnum
        {
            Normal,
            Live,
        }
    }
}
