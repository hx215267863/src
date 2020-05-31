// Decompiled with JetBrains decompiler
// Type: IFactory.Domain.Models.KanbanSettingModel
// Assembly: IFactory.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A1CF10FE-6DE1-441C-9879-9FC1623B8297
// Assembly location: E:\设备工作\ali-sys\项目\ATL\i-factory\ATL-Client\BIN 6.27\客户端704\IFactory.Domain.dll

using IFactory.Domain.Common;

namespace IFactory.Domain.Models
{
  public class KanbanSettingModel
  {
    public int KanbanSettingId { get; set; }

    public TimeSectionType ProductionReportTimeSection { get; set; }

    public TimeSectionType ExcellentRateReportTimeSection { get; set; }

    public TimeSectionType AlarmReportTimeSection { get; set; }

    public string DateFormat { get; set; }

    public string TimeFormat { get; set; }

    public int RefreshInterval { get; set; }
  }
}
