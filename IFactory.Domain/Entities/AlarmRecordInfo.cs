// Decompiled with JetBrains decompiler
// Type: IFactory.Domain.Entities.AlarmRecordInfo
// Assembly: IFactory.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A1CF10FE-6DE1-441C-9879-9FC1623B8297
// Assembly location: E:\设备工作\ali-sys\项目\ATL\i-factory\ATL-Client\BIN 6.27\客户端704\IFactory.Domain.dll

using System;

namespace IFactory.Domain.Entities
{
  public class AlarmRecordInfo
  {
    public int AlarmRecordDID { get; set; }

    public string RuleDID { get; set; }

    public int FacilityDID { get; set; }

    public DateTime AlarmTime { get; set; }

    public int AlarmCount { get; set; }

    public int DisposeState { get; set; }

    public DateTime? DisposeTime { get; set; }

    public string Handler { get; set; }

    public int? Duration { get; set; }

    public string Address { get; set; }

    public string Remark { get; set; }

    public virtual FacilityInfo Facility { get; set; }
  }
}
