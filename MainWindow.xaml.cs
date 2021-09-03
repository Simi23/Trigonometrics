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
        public static double CenterX = 200;
        public static double CenterY = 200;
        public static double ZoomFactor = 1;
        

        private void GenerateCanvasDrawing(double alpha) {
            mainCanvas.Children.Clear();

            List<MathDefinition> shapesToDraw = new List<MathDefinition>() {
                new MathCollection.Basics(),
                new MathCollection.MathSin(),
                new MathCollection.MathCos(),
                new MathCollection.MathTan(),
                new MathCollection.MathCot()
            };

            foreach (MathDefinition shapeCollection in shapesToDraw) {
                Dictionary<Shape, ShapeParams> shapes = shapeCollection.ShapeCollection(CenterX, CenterY, alpha, ConvertToDegrees(alpha), mainCanvas.ActualWidth, mainCanvas.ActualHeight);
                foreach (Shape shape in shapes.Keys) {
                    ShapeParams shapeParam = shapes[shape];
                    mainCanvas.Children.Add(shape);
                    if (shapeParam.MouseEnter != null) {
                        shape.MouseEnter += shapeParam.MouseEnter;
                    }
                    if (shapeParam.MouseLeave != null) {
                        shape.MouseLeave += shapeParam.MouseLeave;
                    }
                    if (shapeParam.ChangeLeft) {
                        Canvas.SetLeft(shape, shapeParam.Left);
                    }
                    if (shapeParam.ChangeTop) {
                        Canvas.SetTop(shape, shapeParam.Top);
                    }
                    Canvas.SetZIndex(shape, shapeParam.IndexZ);
                }
            }
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

                GenerateCanvasDrawing(rad);

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
    }
}
