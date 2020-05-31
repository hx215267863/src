using IFactory.Domain.Models;

namespace IFactory.Platform.Common.Response.Setting
{
    public class KanbanSettingGetResponse : BaseResponse
    {
        public KanbanSettingModel KanbanSetting { get; set; }
    }
}
