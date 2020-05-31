using IFactory.Domain.Entities;

namespace IFactory.Service
{
    public interface ISettingService
    {
        KanbanSettingInfo GetKanbanSetting(int kanbanSettingId);

        void SaveKanbanSetting(KanbanSettingInfo kanbanSettingInfo);
    }
}
