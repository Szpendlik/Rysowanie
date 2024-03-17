using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using Rysowanie;


namespace Rysowanko
{
    public partial class MainWindow : Window
    {
        Point currentPoint = new Point();
        bool isLineStart = false;
        int drawStyle = 0;
        int editStyle = 0;
        Color selectedColor = new Color();
        List<Line> allLines = new List<Line>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void obszarRoboczy_MouseDown(object sender, MouseButtonEventArgs e)
        {
            currentPoint = e.GetPosition(this);
        }
        private void obszarRoboczy_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && drawStyle == 1)
            {
                Line line = new Line();

                line.Stroke = SystemColors.WindowFrameBrush;

                Point newPoint = e.GetPosition(this);

                line.X1 = currentPoint.X;
                line.Y1 = currentPoint.Y;
                line.X2 = newPoint.X;
                line.Y2 = newPoint.Y;

                currentPoint = newPoint;

                obszarRoboczy.Children.Add(line);
            }

            if (e.LeftButton == MouseButtonState.Pressed && drawStyle == 3)
            {
                if (isLineStart)
                {
                    Line line = new Line();

                    line.Stroke = SystemColors.WindowFrameBrush;

                    line.X1 = currentPoint.X;
                    line.Y1 = currentPoint.Y;
                    line.X2 = e.GetPosition(this).X;
                    line.Y2 = e.GetPosition(this).Y;

                    obszarRoboczy.Children.Add(line);
                    allLines.Add(line);

                    isLineStart = false;
                }
                else
                {
                    isLineStart = true;
                }
            }
            if (drawStyle == 10 && e.LeftButton == MouseButtonState.Pressed)
            {
                var clikedElement = e.Source as FrameworkElement;

                if (clikedElement != null)
                {
                    if (obszarRoboczy.Children.Contains(clikedElement))
                    {
                        obszarRoboczy.Children.Remove(clikedElement);
                    }
                }
            }
        }

        private void obszarRoboczy_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (drawStyle == 2)
            {
                Ellipse ellpipse = drawPoint(6, e.GetPosition(this).X, e.GetPosition(this).Y);
            }
            else if (drawStyle == 4)
            {
                Polygon poly = new Polygon();

                double mouseX = e.GetPosition(this).X;
                double mouseY = e.GetPosition(this).Y;

                double polySize = 20;

                Point point1 = new Point(mouseX - polySize, mouseY + 2 * polySize);
                Point point2 = new Point(mouseX + polySize, mouseY + 2 * polySize);
                Point point3 = new Point(mouseX + 2 * polySize, mouseY);
                Point point4 = new Point(mouseX + polySize, mouseY - 2 * polySize);
                Point point5 = new Point(mouseX - polySize, mouseY - 2 * polySize);
                Point point6 = new Point(mouseX - 2 * polySize, mouseY);

                PointCollection polyPoints = new PointCollection();

                polyPoints.Add(point1);
                polyPoints.Add(point2);
                polyPoints.Add(point3);
                polyPoints.Add(point4);
                polyPoints.Add(point5);
                polyPoints.Add(point6);

                poly.Points = polyPoints;


                Brush brushColor = new SolidColorBrush(selectedColor);

                poly.Stroke = brushColor;

                obszarRoboczy.Children.Add(poly);
            }
            if (drawStyle == 5)
            {

                Polygon poly = new Polygon();

                double mouseX = e.GetPosition(this).X;
                double mouseY = e.GetPosition(this).Y;

                double polySize = 20;

                Point point1 = new Point(mouseX + polySize / 2, mouseY + polySize / 2);
                Point point2 = new Point(mouseX + polySize / 2, mouseY - polySize / 2);
                Point point3 = new Point(mouseX - polySize / 2, mouseY - polySize / 2);
                Point point4 = new Point(mouseX - polySize / 2, mouseY + polySize / 2);


                PointCollection polyPoints = new PointCollection();

                polyPoints.Add(point1);
                polyPoints.Add(point2);
                polyPoints.Add(point3);
                polyPoints.Add(point4);

                poly.Points = polyPoints;


                Brush brushColor = new SolidColorBrush(selectedColor);

                poly.Stroke = brushColor;

                obszarRoboczy.Children.Add(poly);
            }
            else if (drawStyle == 6)
            {


                Polygon poly = new Polygon();

                double mouseX = e.GetPosition(this).X;
                double mouseY = e.GetPosition(this).Y;

                double polySize = 20;

                Point point1 = new Point(mouseX + polySize / 2, mouseY + polySize / 2);
                Point point2 = new Point(mouseX + polySize / 2, mouseY - polySize / 2);
                Point point3 = new Point(mouseX - polySize / 2, mouseY - polySize / 2);
                Point point4 = new Point(mouseX - polySize / 2, mouseY + polySize / 2);
                Point point5 = new Point(mouseX - polySize, mouseY + polySize / 2);
                Point point6 = new Point(mouseX, mouseY + polySize * 2);
                Point point7 = new Point(mouseX + polySize, mouseY + polySize / 2);


                PointCollection polyPoints = new PointCollection();

                polyPoints.Add(point1);
                polyPoints.Add(point2);
                polyPoints.Add(point3);
                polyPoints.Add(point4);
                polyPoints.Add(point5);
                polyPoints.Add(point6);
                polyPoints.Add(point7);

                poly.Points = polyPoints;


                Brush brushColor = new SolidColorBrush(selectedColor);
                Brush brushColor1 = new SolidColorBrush(selectedColor);

                poly.Stroke = brushColor;
                poly.Fill = brushColor1;

                obszarRoboczy.Children.Add(poly);
            }
            else if (drawStyle == 7)
            {


                Polygon poly = new Polygon();

                double mouseX = e.GetPosition(this).X;
                double mouseY = e.GetPosition(this).Y;

                double polySize = 20;

                Point point1 = new Point(mouseX + polySize / 2, mouseY + polySize);
                Point point2 = new Point(mouseX + polySize / 2, mouseY + polySize / 2);
                Point point3 = new Point(mouseX + polySize, mouseY + polySize / 2);
                Point point4 = new Point(mouseX + polySize, mouseY - polySize / 2);
                Point point5 = new Point(mouseX + polySize / 2, mouseY - polySize / 2);
                Point point6 = new Point(mouseX + polySize / 2, mouseY - polySize);
                Point point7 = new Point(mouseX - polySize / 2, mouseY - polySize);
                Point point8 = new Point(mouseX - polySize / 2, mouseY - polySize / 2);
                Point point9 = new Point(mouseX - polySize, mouseY - polySize / 2);
                Point point10 = new Point(mouseX - polySize, mouseY + polySize / 2);
                Point point11 = new Point(mouseX - polySize / 2, mouseY + polySize / 2);
                Point point12 = new Point(mouseX - polySize / 2, mouseY + polySize);

                PointCollection polyPoints = new PointCollection();

                polyPoints.Add(point1);
                polyPoints.Add(point2);
                polyPoints.Add(point3);
                polyPoints.Add(point4);
                polyPoints.Add(point5);
                polyPoints.Add(point6);
                polyPoints.Add(point7);
                polyPoints.Add(point8);
                polyPoints.Add(point9);
                polyPoints.Add(point10);
                polyPoints.Add(point11);
                polyPoints.Add(point12);

                poly.Points = polyPoints;


                Brush brushColor = new SolidColorBrush(selectedColor);
                Brush brushColor1 = new SolidColorBrush(selectedColor);

                poly.Stroke = brushColor;
                poly.Fill = brushColor1;

                obszarRoboczy.Children.Add(poly);
            }

            if(drawStyle == 10)
            {
                var clikedElement = e.Source as FrameworkElement;

                if(clikedElement != null) 
                {
                    if(obszarRoboczy.Children.Contains(clikedElement))
                    {
                        obszarRoboczy.Children.Remove(clikedElement);
                    }
                }
            }

        }

        private Ellipse drawPoint(double size, double x, double y)
        {
            Ellipse ellipse = new Ellipse();
            ellipse.Width = size;
            ellipse.Height = size;

            Canvas.SetTop(ellipse, y - ellipse.Height / 2);
            Canvas.SetLeft(ellipse, x - ellipse.Width / 2);

            Brush brushColor = new SolidColorBrush(selectedColor);

            ellipse.Fill = brushColor;

            obszarRoboczy.Children.Add(ellipse);

            return ellipse;
        }

        private void przycisk_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Program do tworzenia grafiki, 2023, Bartosz Skura");
        }

        private void DrawBtn_Click(object sender, RoutedEventArgs e)
        {
            //Rysowanie dowolone
            drawStyle = 1;
        }

        private void DrawPoints_Click(object sender, RoutedEventArgs e)
        {
            //Rysowanie punktów
            drawStyle = 2;
        }

        private void DrawSegmentBtn_Click(object sender, RoutedEventArgs e)
        {
            //Rysowanie Lini
            drawStyle = 3;
        }

        private void Edit_Segment_Click(object sender, RoutedEventArgs e)
        {
            //Edycja lini
            editStyle = 1;
        }

        private void DrawPolygon_Click(object sender, RoutedEventArgs e)
        {
            //Rysuje Wielokąt
            drawStyle = 4;
        }

        private void DrawSquare_Click(object sender, RoutedEventArgs e)
        {
            //Rysuje Kwadrat
            drawStyle = 5;
        }

        private void DrawArrow_Click(object sender, RoutedEventArgs e)
        {
            //Rysuje Strzałke
            drawStyle = 6;
        }

        private void DrawPlus_Click(object sender, RoutedEventArgs e)
        {
            //Rysuje Plus
            drawStyle = 7;
        }

        private void ColorPicker_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ColorPickerWindow colorPickerWindow = new ColorPickerWindow();
            if (colorPickerWindow.ShowDialog() == true)
            {
                selectedColor = colorPickerWindow.SelectedColor;

                ColorPicker.Fill = new SolidColorBrush(selectedColor);
            }
        }

        private void AddPictureButton_Click(object sender, RoutedEventArgs e)
        {
            PicturePickerWindow picturePickerWindow = new PicturePickerWindow();
            picturePickerWindow.Show();
        }
        public void SaveToFile(Uri path, Canvas surface, int x)
        {
            if (path == null) return;

            Transform transform = surface.LayoutTransform;
            surface.LayoutTransform = null;

            Size size = new Size(surface.ActualWidth, surface.ActualHeight);

            surface.Measure(size);
            surface.Arrange(new Rect(size));

            RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap(
                (int)size.Width,
                (int)size.Height,
                96d,
                96d,
                PixelFormats.Pbgra32);

            renderTargetBitmap.Render(surface);

            if(x == 1)
            {
                using (FileStream outStream = new FileStream(path.LocalPath, FileMode.Create))
                {
                    PngBitmapEncoder encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(renderTargetBitmap));
                    encoder.Save(outStream);
                }
            }
            else
            {
                using(FileStream outStream = new FileStream(path.LocalPath, FileMode.Create))
                {
                    JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(renderTargetBitmap));
                    encoder.Save(outStream);
                }
            }

            surface.LayoutTransform = transform;

        }

        private void SavePaintAsPNG_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Image File (*.png)|*.png";
            saveFileDialog.FilterIndex = 1;
            if(saveFileDialog.ShowDialog() == true)
            {
                Uri uri= new Uri(saveFileDialog.FileName);
                SaveToFile(uri, obszarRoboczy, 1);
            }
        }

        private void SavePaintAsJPG_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Image File (*.jpg)|*.jpg";
            saveFileDialog.FilterIndex = 1;
            if (saveFileDialog.ShowDialog() == true)
            {
                Uri uri = new Uri(saveFileDialog.FileName);
                SaveToFile(uri, obszarRoboczy, 2);
            }
        }

        private void Eraser_Click(object sender, RoutedEventArgs e)
        {
            drawStyle = 10;
        }
    }
}