// Decompiled with JetBrains decompiler
// Type: IFactory.Domain.Models.AlarmRecordModel
// Assembly: IFactory.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A1CF10FE-6DE1-441C-9879-9FC1623B8297
// Assembly location: E:\设备工作\ali-sys\项目\ATL\i-factory\ATL-Client\BIN 6.27\客户端704\IFactory.Domain.dll

using System;
using System.Collections.Generic;

namespace IFactory.Domain.Models
{
  public class AlarmRecordModel
  {
    private IList<AlarmFieldValue> fieldValues;

    public int DID { get; set; }

    public string RuleDID { get; set; }

    public int CraftDID { get; set; }

    public int ProcessDID { get; set; }

    public int FacilityDID { get; set; }

    public int UnitDID { get; set; }

    public DateTime AlarmTime { get; set; }

    public int? AlarmCount { get; set; }

    public int? DisposeState { get; set; }

    public DateTime? DisposeTime { get; set; }

    public string Handler { get; set; }

    public int? Duration { get; set; }

    public string Address { get; set; }

    public string Remark { get; set; }

    public string AlarmContent { get; set; }

    public string AlarmReason { get; set; }

    public string SolutionText { get; set; }

    public string SolutionImagePath { get; set; }

    public string AlarmLocationImagePath { get; set; }

    public string CraftName { get; set; }

    public string UnitName { get; set; }

    public string ProcessName { get; set; }

    public string FacilityName { get; set; }

    public string AlarmTypeName { get; set; }

    public IList<AlarmFieldValue> FieldValues
    {
      get
      {
        return this.fieldValues ?? (this.fieldValues = (IList<AlarmFieldValue>) new List<AlarmFieldValue>());
      }
      set
      {
        this.fieldValues = value;
      }
    }
  }
}
