using IFactory.Platform.Common.Response.Setting;

namespace IFactory.Platform.Common.Request.Setting
{
    public class KanbanSettingGetRequest : BaseRequest<KanbanSettingGetResponse>
    {
        public override string ApiName
        {
            get
            {
                return "kanban.setting.get";
            }
        }
    }
}
