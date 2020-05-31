using System;

namespace IFactory.Domain.Entities
{
    public class AlarmTemporaryInfo
    {
        public int AlarmTemporaryDID { get; set; }

        public string RuleDID { get; set; }

        public int FacilityDID { get; set; }

        public DateTime AlarmTime { get; set; }

        public int DisposeState { get; set; }

        public DateTime? DisposeTime { get; set; }

        public string Handler { get; set; }

        public int? Duration { get; set; }

        public string Address { get; set; }

        public string Remark { get; set; }

        public virtual FacilityInfo Facility { get; set; }
    }
}
