// Decompiled with JetBrains decompiler
// Type: IFactory.Domain.Models.AlarmRuleModel
// Assembly: IFactory.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A1CF10FE-6DE1-441C-9879-9FC1623B8297
// Assembly location: E:\设备工作\ali-sys\项目\ATL\i-factory\ATL-Client\BIN 6.27\客户端704\IFactory.Domain.dll

namespace IFactory.Domain.Models
{
  public class AlarmRuleModel
  {
    public string RuleDID { get; set; }

    public string AlarmContent { get; set; }

    public int AlarmTypeDID { get; set; }

    public string AlarmReason { get; set; }

    public int SolutionDID { get; set; }

    public int SolutionImageDID { get; set; }

    public int AlarmLocationImageDID { get; set; }

    public int CraftDID { get; set; }

    public int UnitDID { get; set; }

    public string SolutionText { get; set; }

    public string SolutionImagePath { get; set; }

    public string AlarmLocationImagePath { get; set; }

    public string AlarmTypeName { get; set; }
  }
}
