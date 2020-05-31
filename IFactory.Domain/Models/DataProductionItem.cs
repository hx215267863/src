using System;

namespace IFactory.Domain.Models
{
    public class DataProductionItem
    {

        public int Iden { get; set; }

        //生产时间
        public DateTime ProductTime { get; set; }

        //产量
        public int CellTotal { get; set; }

        //良品数
        public int OKCount { get; set; }

        //NG数
        public int NGCount { get; set; }

        //产能
        public int EnableProduction { get; set; }

        //开机时间
        public int RunningTime { get; set; }

        //停机时间
        public int StopTime { get; set; }

        //待机时间
        public int WaitTime { get; set; }

        //良品率
        public string OKRate { get; set; }

        //成品编码
        public string code { get; set; }

        //
        public int DeviceDid { get; set; }

        public DateTime TimeStart { get; set; }

        public DateTime TimeEnd { get; set; }

        public string Keyword { get; set; }
    }
}
