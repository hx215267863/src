using System.ComponentModel;

namespace IFactory.Domain.Common
{
    /// <summary>
    /// 槽位
    /// </summary>
    public enum SLOT_SITE
    {
        [Description("1号位")]
        一,
        [Description("2号位")]
        二,
        [Description("3号位")]
        三,
        [Description("4号位")]
        四,
        [Description("5号位")]
        五,
        [Description("6号位")]
        六,
        [Description("7号位")]
        七,
        [Description("8号位")]
        八,
        [Description("9号位")]
        九,
        [Description("10号位")]
        十,
    }

    public enum BATTERY_COLOR
    {
        [Description("黑色")]
        黑色,
        [Description("银色")]
        银色,
    }

    public enum Enum_SLOT_TY
    {
        [Description("TrayA")]
        TrayA,
        [Description("TrayB")]
        TrayB,
        [Description("TrayC")]
        TrayC,
        [Description("TrayD")]
        TrayD,
        [Description("TrayE")]
        TrayE,
    }
}
