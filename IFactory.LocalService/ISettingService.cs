using IFactory.Domain.Entities;

namespace IFactory.LocalService
{
    public interface ISettingService
    {
        KanbanSettingInfo GetKanbanSetting(int kanbanSettingId);

        void SaveKanbanSetting(KanbanSettingInfo kanbanSettingInfo);
    }
}
