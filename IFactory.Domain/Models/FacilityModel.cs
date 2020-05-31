// Decompiled with JetBrains decompiler
// Type: IFactory.Domain.Models.FacilityModel
// Assembly: IFactory.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A1CF10FE-6DE1-441C-9879-9FC1623B8297
// Assembly location: E:\设备工作\ali-sys\项目\ATL\i-factory\ATL-Client\BIN 6.27\客户端704\IFactory.Domain.dll

namespace IFactory.Domain.Models
{
  public class FacilityModel
  {
    public int FacilityDID { get; set; }

    public string MMName { get; set; }

    public int ProcessDID { get; set; }

    public string MMIP { get; set; }

    public string MMPort { get; set; }

    public bool MMIsUse { get; set; }

    public string MMClearAddr { get; set; }

    public string MMRestAddr { get; set; }

    public int MMSeq { get; set; }

    public string MAAddress { get; set; }

    public bool IsUse { get; set; }

    public string Remark { get; set; }
  }
}
