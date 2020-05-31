using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace IFactory.UI.Controls
{
    public class PlaceholderTextBox : TextBox
    {
        public static readonly DependencyProperty PlaceholderProperty = DependencyProperty.Register("Placeholder", typeof(string), typeof(PlaceholderTextBox), (PropertyMetadata)new FrameworkPropertyMetadata("请在此输入", FrameworkPropertyMetadataOptions.AffectsRender));
        public static readonly DependencyProperty PlaceholderStyleProperty = DependencyProperty.Register("PlaceholderStyle", typeof(Style), typeof(PlaceholderTextBox), (PropertyMetadata)new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));
        private readonly TextBlock _placeholderTextBlock = new TextBlock();
        private readonly VisualBrush _placeholderVisualBrush = new VisualBrush();

        public string Placeholder
        {
            get
            {
                return (string)this.GetValue(PlaceholderTextBox.PlaceholderProperty);
            }
            set
            {
                this.SetValue(PlaceholderTextBox.PlaceholderProperty, value);
            }
        }

        public Style PlaceholderStyle
        {
            get
            {
                return (Style)this.GetValue(PlaceholderTextBox.PlaceholderProperty);
            }
            set
            {
                this.SetValue(PlaceholderTextBox.PlaceholderProperty, value);
            }
        }

        public PlaceholderTextBox()
        {
            Binding binding1 = new Binding() { Source = this, Path = new PropertyPath("Placeholder", new object[0]) };
            this._placeholderTextBlock.SetBinding(TextBlock.TextProperty, (BindingBase)binding1);
            this._placeholderVisualBrush.AlignmentX = AlignmentX.Left;
            this._placeholderVisualBrush.Stretch = Stretch.None;
            this._placeholderVisualBrush.Visual = (Visual)this._placeholderTextBlock;
            Binding binding2 = new Binding() { Source = this, Path = new PropertyPath("PlaceholderStyle", new object[0]) };
            this._placeholderTextBlock.SetBinding(FrameworkElement.StyleProperty, (BindingBase)binding2);
            this.Background = (Brush)this._placeholderVisualBrush;
            this.TextChanged += new TextChangedEventHandler(this.PlaceholderTextBox_TextChanged);
        }

        private void PlaceholderTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Background = string.IsNullOrEmpty(this.Text) ? (Brush)this._placeholderVisualBrush : (Brush)null;
        }
    }
}
