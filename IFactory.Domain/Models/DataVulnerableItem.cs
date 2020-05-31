using System;

namespace IFactory.Domain.Models
{
    public class DataVulnerableItem
    {

        //编号
        public int Iden { get; set; }

        //时间
        public DateTime time { get; set; }

        //名称
        public String Name { get; set; }

        //使用次数
        public int Used { get; set; }

        //预期寿命
        public int Expect { get; set; }

        //更换次数
        public int Exchange { get; set; }

        //操作员
        public String User { get; set; }

        //图号1
        public String PicNum1 { get; set; }

        //图号2
        public String PicNum2 { get; set; }

        public string code { get; set; }

        //
        public int DeviceDid { get; set; }

        public DateTime TimeStart { get; set; }

        public DateTime TimeEnd { get; set; }

        public string Keyword { get; set; }
    }
}
