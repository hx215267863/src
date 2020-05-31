using IFactory.Data;
using IFactory.Domain.Entities;

namespace IFactory.LocalService
{
  public class SettingService : BaseService, ISettingService
  {
    public SettingService(IDatabaseFactory databaseFactory)
      : base(databaseFactory)
    {
    }

    public KanbanSettingInfo GetKanbanSetting(int kanbanSettingId)
    {
      return this.DataContext.KanbanSettingInfos.Find(kanbanSettingId);
    }

    public void SaveKanbanSetting(KanbanSettingInfo kanbanSettingInfo)
    {
      this.DataContext.SaveChanges();
    }
  }
}
