using System.ComponentModel;

namespace IFactory.Domain.Common
{
    /// <summary>
    /// 性别
    /// </summary>
    public enum Gender
    {
        [Description("男")]
        Male,
        [Description("女")]
        Female,
    }
}
