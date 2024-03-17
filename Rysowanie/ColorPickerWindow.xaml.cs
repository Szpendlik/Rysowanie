using Rysowanko;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Rysowanie
{
    /// <summary>
    /// Logika interakcji dla klasy ColorPickerWindow.xaml
    /// </summary>
    public partial class ColorPickerWindow : Window
    {
        public Color SelectedColor { get; set; }

        public ColorPickerWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            UpdatePreviewColor();
            DialogResult = true;
            Close();
        }

        private void UpdatePreviewColor()
        {
            byte red = ParseTextBoxValue(R);
            byte green = ParseTextBoxValue(G);
            byte blue = ParseTextBoxValue(B);

            SelectedColor = Color.FromRgb(red, green, blue);
        }

        private byte ParseTextBoxValue(TextBox textBox)
        {
            if(byte.TryParse(textBox.Text, NumberStyles.None, CultureInfo.InvariantCulture, out byte value))
            {
                return Math.Min((byte)255, Math.Max((byte)0, value));
            }
            return 0;
        }
        public void RGBtoHSV()
        {
            double saturation;
            double hue;
            int red = ParseTextBoxValue(R);
            int green = ParseTextBoxValue(G);
            int blue = ParseTextBoxValue(B);

            double min = Math.Min(Math.Min(red,green),blue);
            double max = Math.Max(Math.Max(red,green),blue);

            double delta = max - min;

            double value = max / 255.0;

            if (max == 0)
            {
                saturation = 0;
            }
            else
            {
                saturation = delta / max;
            }

            if(delta==0)
            {
                hue = 0;
            }
            else if(max==red)
            {
                hue = (green-blue) / delta + ((green<blue)?6:0);
            }
            else if (max == green)
            {
                hue = (blue - red) / delta + 2;
            }
            else
            {
                hue = (red - green) / delta + 4;
            }
            
            hue *= 60;
            
            if (hue < 0)
            {
                hue += 360;
            }

            Hue.Content = "Hue: " + hue;
            Saturation.Content = "Saturation: " + saturation;
            Value.Content = "Value: " + value;
        }

        private void R_TextChanged(object sender, TextChangedEventArgs e)
        {
            RGBtoHSV();
        }

        private void G_TextChanged(object sender, TextChangedEventArgs e)
        {
            RGBtoHSV();
        }

        private void B_TextChanged(object sender, TextChangedEventArgs e)
        {
            RGBtoHSV();
        }
    }
}
