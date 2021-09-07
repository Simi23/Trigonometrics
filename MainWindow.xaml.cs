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
        
        public static double CenterX = 200;
        public static double CenterY = 200;
        public static double ZoomFactor = 1;
        
        public MainWindow() {
            InitializeComponent();

            mainCanvas.MouseMove += DragKnob_MouseMove;
        }        

        private void GenerateCanvasDrawing(double alpha) {
            bool knobExists = false;
            Ellipse dragKnob = new Ellipse();
            List<UIElement> deleteList = new List<UIElement>();
            foreach (UIElement element in mainCanvas.Children) {
                if (element.Uid != "dragKnob") {
                    deleteList.Add(element);
                } else {
                    knobExists = true;
                    dragKnob = (Ellipse)element;
                }
            }
            foreach (UIElement element in deleteList) {
                mainCanvas.Children.Remove(element);
            }
            if (!knobExists) {
                dragKnob = new Ellipse()
                {
                    Width = 8,
                    Height = 8,
                    Stroke = ColourPalette.BrushRGB(20, 20, 20),
                    StrokeThickness = 1,
                    Fill = ColourPalette.BrushRGB(220, 220, 220),
                    Uid = "dragKnob",
                    Cursor = Cursors.Hand
                };
                mainCanvas.Children.Add(dragKnob);
                //dragKnob.MouseMove += DragKnob_MouseMove;
            }
            Canvas.SetLeft(dragKnob, CenterX + Math.Cos(alpha) * 100 - 4);
            Canvas.SetTop(dragKnob, CenterY - Math.Sin(alpha) * 100 - 4);
            Canvas.SetZIndex(dragKnob, 10);

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

        private void DragKnob_MouseMove(object sender, MouseEventArgs e)
        {
            Point mp = Mouse.GetPosition(mainCanvas);
            double a = CenterY - mp.Y;
            double b = mp.X - CenterX;
            double alpha = Math.Atan(a / b);

            GenerateCanvasDrawing(alpha);
            Console.WriteLine($"Angle: {ConvertToDegrees(alpha)}");
        }

        private void angleInput_TextChanged(object sender, TextChangedEventArgs e) {
            if (double.TryParse(angleInput.Text, out double angle)) {
                angleInput.BorderBrush = System.Windows.Media.Brushes.LightBlue;
                angleInput.SelectionBrush = System.Windows.Media.Brushes.LightBlue;
                angleInput.Background = Brushes.White;

                double rad = ConvertToRadians(angle);

                int roundDecimals = 4;

                string tan = angle % 180 == 90 ? "-" : Math.Round(Math.Tan(rad), roundDecimals).ToString();
                string cot = angle % 180 == 0 ? "-" : Math.Round(1 / Math.Tan(rad), roundDecimals).ToString();

                lb_v_sin.Content = Math.Round(Math.Sin(rad), roundDecimals);
                lb_v_cos.Content = Math.Round(Math.Cos(rad), roundDecimals);
                lb_v_tg.Content = tan;
                lb_v_ctg.Content = cot;

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
