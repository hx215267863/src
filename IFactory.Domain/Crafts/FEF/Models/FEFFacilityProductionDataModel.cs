﻿// Decompiled with JetBrains decompiler
// Type: IFactory.Domain.Crafts.FEF.Models.FEFFacilityProductionDataModel
// Assembly: IFactory.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A1CF10FE-6DE1-441C-9879-9FC1623B8297
// Assembly location: E:\设备工作\ali-sys\项目\ATL\i-factory\ATL-Client\BIN 6.27\客户端704\IFactory.Domain.dll

using IFactory.Domain.Crafts.Base.Models;
using System;

namespace IFactory.Domain.Crafts.FEF.Models
{
  public class FEFFacilityProductionDataModel : FacilityProductionDataModel
  {
    public DateTime? StartDate { get; set; }

    public string ProductNo { get; set; }

    public string Operator { get; set; }

    public float? Temprature_Top { get; set; }

    public float? Temprature_Bottom { get; set; }

    public float? UserId { get; set; }
  }
}