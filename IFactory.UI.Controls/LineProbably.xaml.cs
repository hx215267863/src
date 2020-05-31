using IFactory.Domain.Models;
using System.Windows.Controls;
using System.Windows.Markup;

namespace IFactory.UI.Controls
{
    /// <summary>
    /// LineProbably.xaml 的交互逻辑
    /// </summary>
    public partial class LineProbably : Canvas, IComponentConnector
    {
        public LineProbably()
        {
            InitializeComponent();
        }

        public void BindData(ProductionLineProbablyModel productionLineProbablyModel)
        {
            this.txtName.Text = productionLineProbablyModel.Name;
            this.txtNowYield.Text = productionLineProbablyModel.NowYield;
            this.txtProductionType.Text = productionLineProbablyModel.ProductionType;
            this.txtTargetYield.Text = productionLineProbablyModel.TargetYield;
            this.txtUserName.Text = productionLineProbablyModel.UserName;
            this.txtPPM.Text = productionLineProbablyModel.PPM;
            this.txtOKCount.Text = productionLineProbablyModel.OKCount;
            this.txtOKRate.Text = productionLineProbablyModel.OKRate;
        }
    }
}
