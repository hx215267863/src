// Decompiled with JetBrains decompiler
// Type: IFactory.Domain.Models.AlarmRecordItem
// Assembly: IFactory.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A1CF10FE-6DE1-441C-9879-9FC1623B8297
// Assembly location: E:\设备工作\ali-sys\项目\ATL\i-factory\ATL-Client\BIN 6.27\客户端704\IFactory.Domain.dll

using System;

namespace IFactory.Domain.Models
{
  public class AlarmRecordItem
  {
    public int DID { get; set; }

    public string AlarmContent { get; set; }

    public string CraftName { get; set; }

    public string UnitName { get; set; }

    public string ProcessName { get; set; }

    public string FacilityName { get; set; }

    public DateTime AlarmTime { get; set; }

    public DateTime? DisposeTime { get; set; }

    public int? Duration { get; set; }

    public string RuleDID { get; set; }

    public string AlarmTypeName { get; set; }

    public int AlarmDid { get; set; }
    }
}
