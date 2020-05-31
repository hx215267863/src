using System.ComponentModel;

namespace IFactory.Domain.Common
{
    /// <summary>
    /// 测量尺寸
    /// </summary>
    public enum SizeMeas
    {
        [Description("侧封高度")]
        Sidestrip_Height,
        [Description("侧封宽度")]
        Sidestrip_Width,
        [Description("顶封高度")]
        Topstrip_Height,
        [Description("主体宽度")]
        MainBody_Width,
        [Description("主体高度")]
        MainBody_Height,
        [Description("两Tab间的距离")]
        Distance_Between_Tabs,
        [Description("Tab1到气袋距离")]
        Distance_Between_Tab1_and_left_edge,
        [Description("Tab2到气袋距离")]
        Distance_Between_Tab2_and_left_edge,
        [Description("气袋宽度")]
        BagArea_width,
        [Description("铝Tab到槽的距离")]
        TabALToSlotDistanceRight,
        [Description("镍Tab到槽的距离")]
        TabNIToSlotDistanceLeft,
        [Description("左1Sealant高度")]
        SealantHeightOfLeft1,
        [Description("左2Sealant高度")]
        SealantHeightOfLeft2,
        [Description("右1Sealant高度")]
        SealantHeightOfRight1,
        [Description("右2Sealant高度")]
        SealantHeightOfRight2,
        [Description("左Sealant到槽的距离")]
        SealantToSlotDistanceLeft,
        [Description("右Sealant到槽的距离")]
        SealantToSlotDistanceRight,
        [Description("Topseal_Height_2nd")]
        Topseal_Height_2nd,
        [Description("SidePoint1")]
        SidePoint1,
        [Description("SidePoint2")]
        SidePoint2,
        [Description("SidePoint3")]
        SidePoint3,
        [Description("TopPoint1")]
        TopPoint1,
        [Description("TopPoint2")]
        TopPoint2,
        [Description("TopPoint3")]
        TopPoint3,
        [Description("TobPoint1")]
        TobPoint1,
        [Description("TobPoint2")]
        TobPoint2,


    }
}
