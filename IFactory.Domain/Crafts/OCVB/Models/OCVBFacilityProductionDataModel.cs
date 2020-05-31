// Decompiled with JetBrains decompiler
// Type: IFactory.Domain.Crafts.OCVB.Models.OCVBFacilityProductionDataModel
// Assembly: IFactory.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A1CF10FE-6DE1-441C-9879-9FC1623B8297
// Assembly location: E:\设备工作\ali-sys\项目\ATL\i-factory\ATL-Client\BIN 6.27\客户端704\IFactory.Domain.dll

using IFactory.Domain.Crafts.Base.Models;
using System;

namespace IFactory.Domain.Crafts.OCVB.Models
{
  public class OCVBFacilityProductionDataModel : FacilityProductionDataModel
  {
    public DateTime? StartDate { get; set; }

    public string ProductNo { get; set; }

    public string TabBarCode { get; set; }

    public string Operator { get; set; }

    public DateTime? TestTime { get; set; }

    public float? Voltage { get; set; }

    public float? coreResistance { get; set; }

    public float? Temprature_E { get; set; }

    public float? Temprature_base { get; set; }

    public int OCVChannel { get; set; }

    public int IVChannel { get; set; }

    public float? Result { get; set; }

    public float? VIValue { get; set; }

    public int UserId { get; set; }
  }
}
