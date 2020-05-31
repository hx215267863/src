using LiveCharts.Wpf;
using System.Collections.Generic;
using System.Windows.Media;

namespace IFactory.UI.Helpers
{
    public static class ChartHelper
    {
        public static ColorsCollection ToColorsCollection(this Color[] colors)
        {
            ColorsCollection colorsCollection = new ColorsCollection();
            Color[] colorArray = colors;
            colorsCollection.AddRange(colorArray);
            return colorsCollection;
        }
    }
}
