// Decompiled with JetBrains decompiler
// Type: IFactory.Domain.Models.UserModel
// Assembly: IFactory.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A1CF10FE-6DE1-441C-9879-9FC1623B8297
// Assembly location: E:\设备工作\ali-sys\项目\ATL\i-factory\ATL-Client\BIN 6.27\客户端704\IFactory.Domain.dll

using System;

namespace IFactory.Domain.Models
{
  public class SystemParamModel
    {

    public int  IDX { get; set; }
    public string ITEM_CD { get; set; }

    public string SLOT_TY { get; set; }

    public string SLOT_SITE { get; set; }

    public IFactory.Domain.Common.Gender? Gender { get; set; }

    public IFactory.Domain.Common.SizeMeas? Size { get; set; }

    public string SLOT_X_DOT { get; set; }

    public string SLOT_Y_DOT { get; set; }

    public string SLOT_Z_DOT { get; set; }

    public string SLOT_U_DOT { get; set; }

    public string LIGHT_1 { get; set; }
    public string LIGHT_2 { get; set; }
    public string LIGHT_3 { get; set; }
    public string LIGHT_4 { get; set; }

    public string MO { get; set; }
    public string CRT_ID { get; set; }

    public DateTime CRT_DT { get; set; }
    public string UPT_ID { get; set; }

    public DateTime UPT_DT { get; set; }

    }
}
