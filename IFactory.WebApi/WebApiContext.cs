namespace IFactory.Platform
{
    public class WebApiContext : IWebApiContext
    {
        public int AppId { get; set; }

        public int UserId { get; set; }

        public int AuthId { get; set; }
    }
}
