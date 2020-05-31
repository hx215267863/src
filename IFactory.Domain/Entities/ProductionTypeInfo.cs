// Decompiled with JetBrains decompiler
// Type: IFactory.Domain.Entities.ProductionTypeInfo
// Assembly: IFactory.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A1CF10FE-6DE1-441C-9879-9FC1623B8297
// Assembly location: E:\设备工作\ali-sys\项目\ATL\i-factory\ATL-Client\BIN 6.27\客户端704\IFactory.Domain.dll

using System;

namespace IFactory.Domain.Entities
{
  public class ProductionTypeInfo
  {
    public int DID { get; set; }

    public string ProductNo { get; set; }

    public Decimal? MinWeight { get; set; }

    public Decimal? MaxWeight { get; set; }

    public Decimal? MinScope { get; set; }

    public Decimal? MaxScope { get; set; }

    public int? BarCodeLen { get; set; }

    public int? PrefixLen { get; set; }

    public string PrefixData { get; set; }

    public string DefaultBarCode { get; set; }

    public int FacilityDID { get; set; }

    public DateTime Time { get; set; }
  }
}
