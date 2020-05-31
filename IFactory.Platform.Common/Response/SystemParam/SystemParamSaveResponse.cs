namespace IFactory.Platform.Common.Response.SystemParam
{
    public class SystemParamSaveResponse : BaseResponse
    {
        public string ITEM_CD { get; set; }

        public IFactory.Domain.Common.SizeMeas? SizeMeas { get; set; }
    }
}
