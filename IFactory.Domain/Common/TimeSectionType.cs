using System.ComponentModel;

namespace IFactory.Domain.Common
{
    /// <summary>
    /// 时间截面类型
    /// </summary>
    public enum TimeSectionType
    {
        [Description("天")]
        Day = 1,
        [Description("周")]
        Week = 2,
        [Description("月")]
        Month = 3,        
        [Description("季")]
        Quarter = 4,       
        [Description("年")]
        Year = 5,
    }
}
