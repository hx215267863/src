﻿// Decompiled with JetBrains decompiler
// Type: IFactory.Domain.Entities.ProcessInfo
// Assembly: IFactory.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A1CF10FE-6DE1-441C-9879-9FC1623B8297
// Assembly location: E:\设备工作\ali-sys\项目\ATL\i-factory\ATL-Client\BIN 6.27\客户端704\IFactory.Domain.dll

namespace IFactory.Domain.Entities
{
  public class ProcessInfo
  {
    public int ProcessDID { get; set; }

    public string ProcessNO { get; set; }

    public string ProcessName { get; set; }

    public int CraftDID { get; set; }

    public virtual CraftInfo Craft { get; set; }
  }
}