﻿// Decompiled with JetBrains decompiler
// Type: IFactory.Domain.Crafts.OCVB.Mappings.OCVBFacilityRunArgMap
// Assembly: IFactory.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A1CF10FE-6DE1-441C-9879-9FC1623B8297
// Assembly location: E:\设备工作\ali-sys\项目\ATL\i-factory\ATL-Client\BIN 6.27\客户端704\IFactory.Domain.dll

using IFactory.Domain.Crafts.Base.Mappings;
using IFactory.Domain.Crafts.OCVB.Entities;

namespace IFactory.Domain.Crafts.OCVB.Mappings
{
  public class OCVBFacilityRunArgMap : FacilityRunArgMap<OCVBFacilityRunArgInfo>
  {
    public override string TableName
    {
      get
      {
        return "ocvb_facility_run_arg";
      }
    }
  }
}