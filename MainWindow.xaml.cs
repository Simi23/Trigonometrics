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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Trigonometrics {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }
        private static double CenterX = 200;
        private static double CenterY = 200;
        private static double ZoomFactor = 1;

        private void GenerateCanvasDrawing(double alpha, bool showTanHelp, bool tanRight/*, bool showCot, bool cotUp*/) {
            mainCanvas.Children.Clear();

            // Main Circle
            Ellipse circle = new Ellipse() {
                Width = 200,
                Height = 200,
                Stroke = new SolidColorBrush(Color.FromRgb(43, 43, 43)),
                StrokeThickness = 3
            };
            mainCanvas.Children.Add(circle);
            Canvas.SetZIndex(circle, 0);
            Canvas.SetLeft(circle, CenterX - 100);
            Canvas.SetTop(circle, CenterY - 100);

            circle.MouseEnter += Circle_MouseEnter;
            circle.MouseLeave += Circle_MouseLeave;

            // Base Line
            Line baseLine = new Line() {
                X1 = CenterX,
                Y1 = CenterY,

                X2 = CenterX + 100,
                Y2 = CenterY,
                Stroke = Brushes.Blue,
                StrokeThickness = 3
            };
            mainCanvas.Children.Add(baseLine);

            // Secondary Line
            Line secLine = new Line() {
                X1 = CenterX,
                Y1 = CenterY,

                X2 = CenterX + Math.Cos(alpha) * 100,
                Y2 = CenterY - Math.Sin(alpha) * 100,
                Stroke = Brushes.Blue,
                StrokeThickness = 3
            };
            mainCanvas.Children.Add(secLine);

            // Coord line x
            Line coordLineX = new Line() {
                X1 = 0,
                Y1 = CenterY,

                X2 = mainCanvas.ActualWidth,
                Y2 = CenterY,
                Stroke = Brushes.Gray,
                StrokeDashArray = new DoubleCollection() { 6, 1 },
                StrokeThickness = 2
            };
            mainCanvas.Children.Add(coordLineX);
            Canvas.SetZIndex(coordLineX, -1);

            // Coord line y
            Line coordLineY = new Line() {
                X1 = CenterX,
                Y1 = 0,

                X2 = CenterX,
                Y2 = mainCanvas.ActualHeight,
                Stroke = Brushes.Gray,
                StrokeDashArray = new DoubleCollection() { 6, 1 },
                StrokeThickness = 2
            };
            mainCanvas.Children.Add(coordLineY);
            Canvas.SetZIndex(coordLineY, -1);

            // sinLine
            Line sinLine = new Line() {
                X1 = CenterX + Math.Cos(alpha) * 100,
                Y1 = CenterY,

                X2 = CenterX + Math.Cos(alpha) * 100,
                Y2 = CenterY - Math.Sin(alpha) * 100,
                Stroke = Brushes.Red,
                StrokeThickness = 3
            };
            mainCanvas.Children.Add(sinLine);
            Canvas.SetZIndex(sinLine, 1);

            // cosLine
            Line cosLine = new Line() {
                X1 = CenterX,
                Y1 = CenterY - Math.Sin(alpha) * 100,

                X2 = CenterX + Math.Cos(alpha) * 100,
                Y2 = CenterY - Math.Sin(alpha) * 100,
                Stroke = Brushes.SlateBlue,
                StrokeThickness = 3
            };
            mainCanvas.Children.Add(cosLine);
            Canvas.SetZIndex(cosLine, 1);

            // tan line
            // Main line
            Brush tanBrush = Math.Tan(alpha) >= 0 ? Brushes.OrangeRed : Brushes.CornflowerBlue;
            double tanX1, tanX2, tanY1, tanY2;

            double tan = tanRight ? Math.Tan(alpha) : Math.Tan(alpha) * -1;

            if (tanRight) {
                tanX1 = CenterX + 100;
                tanY1 = CenterY;
                tanX2 = CenterX + 100;
                tanY2 = CenterY - tan * 100;
            } else {
                tanX1 = CenterX - 100;
                tanY1 = CenterY;
                tanX2 = CenterX - 100;
                tanY2 = CenterY - tan * 100;
            }

            if (Math.Abs(ConvertToDegrees(alpha)) % 180 - 90 == 0)
            {
                tanY2 = Math.Min(mainCanvas.ActualHeight, Math.Max(0, tanY2));
            }

            Line tanLine = new Line() {
                X1 = tanX1,
                Y1 = tanY1,

                X2 = tanX2,
                Y2 = tanY2,
                Stroke = tanBrush,
                StrokeThickness = 3
            };
            mainCanvas.Children.Add(tanLine);
            Canvas.SetZIndex(tanLine, 1);

            if (showTanHelp) {
                // Helper line
                Line tanHelper = new Line() {
                    X1 = CenterX,
                    Y1 = CenterY,

                    X2 = tanX2,
                    Y2 = tanY2,
                    Stroke = Brushes.Gray,
                    StrokeDashArray = new DoubleCollection() { 6, 1 },
                    StrokeThickness = 2
                };
                mainCanvas.Children.Add(tanHelper);
                Canvas.SetZIndex(tanHelper, -1);
            }
        }

        private void Circle_MouseLeave(object sender, MouseEventArgs e) {
            Ellipse c = (Ellipse)sender;
            c.Stroke = new SolidColorBrush(Color.FromRgb(43, 43, 43));

            DoubleAnimation da = new DoubleAnimation(4, 3, new Duration(TimeSpan.FromSeconds(.125)));
            Storyboard.SetTarget(da, c);
            Storyboard.SetTargetProperty(da, new PropertyPath("StrokeThickness"));
            Storyboard stb = new Storyboard();
            stb.Children.Add(da);
            stb.Begin();
        }

        private void Circle_MouseEnter(object sender, MouseEventArgs e) {
            Ellipse c = (Ellipse)sender;
            c.Stroke = Brushes.Black;

            DoubleAnimation da = new DoubleAnimation(3, 4, new Duration(TimeSpan.FromSeconds(.125)));
            Storyboard.SetTarget(da, c);
            Storyboard.SetTargetProperty(da, new PropertyPath("StrokeThickness"));
            Storyboard stb = new Storyboard();
            stb.Children.Add(da);
            stb.Begin();

        }

        private void angleInput_TextChanged(object sender, TextChangedEventArgs e) {
            if (double.TryParse(angleInput.Text, out double angle)) {
                angleInput.BorderBrush = System.Windows.Media.Brushes.LightBlue;
                angleInput.SelectionBrush = System.Windows.Media.Brushes.LightBlue;
                angleInput.Background = Brushes.White;

                double rad = ConvertToRadians(angle);

                int roundDecimals = 4;

                lb_v_sin.Content = Math.Round(Math.Sin(rad), roundDecimals);
                lb_v_cos.Content = Math.Round(Math.Cos(rad), roundDecimals);
                lb_v_tg.Content = Math.Round(Math.Tan(rad), roundDecimals);
                lb_v_ctg.Content = Math.Round(1 / Math.Tan(rad), roundDecimals);

                GenerateCanvasDrawing(rad, DetermineShowTanHelper(angle), DetermineTanRight(angle));

            } else if (angleInput.Text.Length == 0) {
                angleInput.BorderBrush = System.Windows.Media.Brushes.LightBlue;
                angleInput.SelectionBrush = System.Windows.Media.Brushes.LightBlue;
                angleInput.Background = Brushes.White;

                lb_v_sin.Content = "NaN";
                lb_v_cos.Content = "NaN";
                lb_v_tg.Content = "NaN";
                lb_v_ctg.Content = "NaN";
            } else {
                angleInput.BorderBrush = System.Windows.Media.Brushes.Red;
                angleInput.SelectionBrush = System.Windows.Media.Brushes.Red;
                angleInput.Background = Brushes.LightCoral;

                lb_v_sin.Content = "NaN";
                lb_v_cos.Content = "NaN";
                lb_v_tg.Content = "NaN";
                lb_v_ctg.Content = "NaN";
            }
        }
        public double ConvertToRadians(double angle) {
            return (Math.PI / 180) * angle;
        }

        public double ConvertToDegrees(double radian)
        {
            return radian / (Math.PI / 180);
        }

        public bool DetermineTanRight(double angle) {
            double tempAngle = angle % 360;
            if(tempAngle < 0) {
                tempAngle += 360;
            }

           if (tempAngle <= 90)
            {
                return true;
            }
            else if (tempAngle < 180)
            {
                return false;
            }
            else if (tempAngle <= 270)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool DetermineShowTanHelper(double angle) {
            return Math.Abs(angle) % 90 != 0;
        }

        public bool DetermineCotUp(double angle)
        {
            return false;
        }

        public bool DetermineShowCot(double angle)
        {
            return false;
        }
    }
}
