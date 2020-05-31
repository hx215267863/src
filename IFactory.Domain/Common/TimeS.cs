using System.ComponentModel;

namespace IFactory.Domain.Common
{
    /// <summary>
    /// 时间2
    /// </summary>
    public enum TimeS
    {
        [Description("分")]
        Mins,
        [Description("时")]
        Hours,
        [Description("天")]
        Days,
    }
}
