// Decompiled with JetBrains decompiler
// Type: IFactory.Domain.Models.UserModel
// Assembly: IFactory.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A1CF10FE-6DE1-441C-9879-9FC1623B8297
// Assembly location: E:\设备工作\ali-sys\项目\ATL\i-factory\ATL-Client\BIN 6.27\客户端704\IFactory.Domain.dll

using System;

namespace IFactory.Domain.Models
{
  public class UserModel
  {
    public int UserId { get; set; }

    public string UserName { get; set; }

    public string Password { get; set; }

    public string Name { get; set; }

    public IFactory.Domain.Common.Gender? Gender { get; set; }

    public IFactory.Domain.Common.SizeMeas? Size { get; set; }

    public int RoleId { get; set; }

    public DateTime CreateTime { get; set; }

    public string RoleName { get; set; }

    public string GenderDesc { get; set; }

    public string CraftDIDs { get; set; }

    public string craftwork { get; set; }
    public string process { get; set; }
    public string quarters { get; set; }
    public string segments { get; set; }
    public string staffid { get; set; }

    public string barcode { get; set; }
    public string factoryID { get; set; }
    public string fano { get; set; }

    public string endproductno { get; set; }
    }
}
