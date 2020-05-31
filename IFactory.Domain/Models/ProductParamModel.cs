// Decompiled with JetBrains decompiler
// Type: IFactory.Domain.Models.UserModel
// Assembly: IFactory.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A1CF10FE-6DE1-441C-9879-9FC1623B8297
// Assembly location: E:\设备工作\ali-sys\项目\ATL\i-factory\ATL-Client\BIN 6.27\客户端704\IFactory.Domain.dll

using System;

namespace IFactory.Domain.Models
{
  public class ProductParamModel
    {

    public string ITEM_CD { get; set; }

    public string ITEM_NM { get; set; }

    public string ITEM_DESC { get; set; }

    public IFactory.Domain.Common.Gender? Gender { get; set; }

    public IFactory.Domain.Common.SizeMeas? Size { get; set; }

    public string MODEL_CD { get; set; }

    public string MODEL_NM { get; set; }

    public string ITEM_COLOR { get; set; }

    public string ITEM_LONG { get; set; }
    public string ITEM_WIDE { get; set; }
    public string LIGHT_BRIGHT { get; set; }
    public string QTY_FOR_CRIB { get; set; }
    public string QTY_FOR_TARY { get; set; }

    public string MO { get; set; }
    public string CRT_ID { get; set; }

    public DateTime CRT_DT { get; set; }
    public string UPT_ID { get; set; }

    public DateTime UPT_DT { get; set; }

    }
}
