using IFactory.Platform.Common.Response.Alarm;
using System.Collections.Generic;

namespace IFactory.Platform.Common.Request.Alarm
{
    public class AlarmRuleSaveRequest : BaseUploadRequest<AlarmRuleSaveResponse>
    {
        public override string ApiName
        {
            get
            {
                return "alarm.rule.save";
            }
        }

        public string AlarmRuleDID { get; set; }

        public string AlarmContent { get; set; }

        public string AlarmReason { get; set; }

        public string SolutionText { get; set; }

        public int CraftDID { get; set; }

        public int UnitDID { get; set; }

        public int AlarmTypeDID { get; set; }

        public Dictionary<string, string> Fields { get; set; }
    }
}
