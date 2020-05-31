using System.ComponentModel;

namespace IFactory.Domain.Common
{
    /// <summary>
    /// 测量尺寸
    /// </summary>
    public enum AlarmCount
    {
        [Description("0X0D0001")]
        Sidestrip_Height,
        [Description("0X0D0002")]
        Sidestrip_Width,
        [Description("0X0D0003")]
        Topstrip_Height,
        [Description("0X0D0004")]
        MainBody_Width,
        [Description("0X0D0005")]
        MainBody_Height,
        [Description("0X0D0006")]
        Distance_Between_Tabs,
        [Description("0X0D0007")]
        Distance_Between_Tab1_and_left_edge,
        [Description("0X0D0008")]
        Distance_Between_Tab2_and_left_edge,
        [Description("0X0D0101")]
        BagArea_width,
        [Description("0X0D0102")]
        TabALToSlotDistanceRight,
        [Description("0X0D0103")]
        TabNIToSlotDistanceLeft,

    }
}
