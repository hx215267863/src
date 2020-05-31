// Decompiled with JetBrains decompiler
// Type: IFactory.Domain.Models.AlarmTemporaryItem
// Assembly: IFactory.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A1CF10FE-6DE1-441C-9879-9FC1623B8297
// Assembly location: E:\设备工作\ali-sys\项目\ATL\i-factory\ATL-Client\BIN 6.27\客户端704\IFactory.Domain.dll

using System;

namespace IFactory.Domain.Models
{
  public class ProductNGItem
  {
        public string DeviceNo { get; set; }

        //工时
        public int ProductTime { get; set; }

        public int NGTime { get; set; }

        public int MotorNG { get; set; }

        public int QiGangNG { get; set; }

        public int GanYingNG { get; set; }

        public string ProductionNo { get; set; }

        public DateTime ProductionStart { get; set; }

        public DateTime ProductionEnd { get; set; }

        //生产时间
        public DateTime ProductionTimer { get; set; }

        public int WaitTime { get; set; }



   }
}
