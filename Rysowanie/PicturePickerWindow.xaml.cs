using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
    /// Logika interakcji dla klasy PicturePickerWindow.xaml
    /// </summary>
    public partial class PicturePickerWindow : Window
    {
        Uri orginalImageUri;
        BitmapImage bitmapImage;
        public PicturePickerWindow()
        {
            InitializeComponent();
        }

        private void UploadPicture_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();

            openDialog.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*jpg; *.jpeg; *.gif; *.bmp";

            if(openDialog.ShowDialog() == true){
                
                Uri uri = new Uri(openDialog.FileName);

                orginalImageUri = uri;
                
                bitmapImage = new BitmapImage(uri);

                OrginalImage.Source = new BitmapImage(uri);

                bitmapImage = new BitmapImage(uri);

            }
        }

        private void ApplyLinearFilter(WriteableBitmap image, double[,] filter)
        {
            int width = image.PixelWidth;
            int height = image.PixelHeight;

            int stride = width * 4; // Każdy piksel zajmuje 4 bajty (ARGB)

            byte[] pixels = new byte[height * stride];
            image.CopyPixels(pixels, stride, 0);

            byte[] resultPixels = new byte[height * stride];

            int filterSize = filter.GetLength(0);
            int filterOffset = filterSize / 2;

            for (int y = filterOffset; y < height - filterOffset; y++)
            {
                for (int x = filterOffset; x < width - filterOffset; x++)
                {
                    double red = 0, green = 0, blue = 0;

                    for (int i = 0; i < filterSize; i++)
                    {
                        for (int j = 0; j < filterSize; j++)
                        {
                            int pixelIndex = ((y + i - filterOffset) * stride) + ((x + j - filterOffset) * 4);

                            red += pixels[pixelIndex + 2] * filter[i, j];
                            green += pixels[pixelIndex + 1] * filter[i, j];
                            blue += pixels[pixelIndex] * filter[i, j];
                        }
                    }

                    int resultIndex = (y * stride) + (x * 4);
                    resultPixels[resultIndex + 2] = Clamp((int)red);
                    resultPixels[resultIndex + 1] = Clamp((int)green);
                    resultPixels[resultIndex] = Clamp((int)blue);
                    resultPixels[resultIndex + 3] = 255; // Alpha

                }
            }

            image.WritePixels(new Int32Rect(0, 0, width, height), resultPixels, stride, 0);
            FilterImage.Source = image;
        }

        private byte Clamp(int value)
        {
            return (byte)(value < 0 ? 0 : (value > 255 ? 255 : value));
        }

        private void FilterPicture_Click(object sender, RoutedEventArgs e)
        {
            float f1 = (float)Convert.ToDouble(_1st.Text);
            bool valid1 = float.TryParse(_1st.Text, out f1);


            float f2 = (float)Convert.ToDouble(_2nd.Text);
            bool valid2 = float.TryParse(_2nd.Text, out f2);


            float f3 = (float)Convert.ToDouble(_3rd.Text);
            bool valid3 = float.TryParse(_3rd.Text, out f3);


            float f4 = (float)Convert.ToDouble(_4th.Text);
            bool valid4 = float.TryParse(_4th.Text, out f4);


            float f5 = (float)Convert.ToDouble(_5th.Text);
            bool valid5 = float.TryParse(_5th.Text, out f5);


            float f6 = (float)Convert.ToDouble(_6th.Text);
            bool valid6 = float.TryParse(_6th.Text, out f6);


            float f7 = (float)Convert.ToDouble(_7th.Text);
            bool valid7 = float.TryParse(_7th.Text, out f7);


            float f8 = (float)Convert.ToDouble(_8th.Text);
            bool valid8 = float.TryParse(_8th.Text, out f8);


            float f9 = (float)Convert.ToDouble(_9th.Text);
            bool valid9 = float.TryParse(_9th.Text, out f9);


            double[,] matrixElements =
            {
               {f1,f2,f3},
               {f4,f5,f6},
               {f7,f8,f9}
            };

            WriteableBitmap writeableBitmap = new WriteableBitmap(bitmapImage);

            ApplyLinearFilter(writeableBitmap, matrixElements);
        }
    }
}
