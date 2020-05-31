using System.ComponentModel;

namespace IFactory.Domain.Common
{
    /// <summary>
    /// 测量尺寸
    /// </summary>
    public enum SizeMeas2
    {
        [Description("主体顶部宽度")]
        MainBody_Width_Top,
        [Description("主体底部宽度")]
        MainBody_Width_Bottom,
        [Description("主体高度")]
        MainBody_Height,
        [Description("顶封高度")]
        Topseal_Height,
        [Description("Tab1与左气袋的距离")]
        Distance_Between_Tab1_and_left_edge,
        [Description("Tab2与左气袋的距离")]
        Distance_Between_Tab2_and_left_edge,
        [Description("Tab之间的距离")]
        Distance_Between_Tabs,
        [Description("左折边顶部高度")]
        Side_LeftFoldingHight_Top,
        [Description("左折边底部高度")]
        Side_LeftFoldingHight_Bottom,
        [Description("右折边顶部高度")]
        Side_RightFoldingHight_Top,
        [Description("左折边底部高度")]
        Side_RightFoldingHight_Bottom,

    }
}
