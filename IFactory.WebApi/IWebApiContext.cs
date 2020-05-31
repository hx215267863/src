namespace IFactory.Platform
{
    public interface IWebApiContext
    {
        int AppId { get; set; }

        int UserId { get; set; }

        int AuthId { get; set; }
    }
}
