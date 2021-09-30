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

        public const double ShiftedCenterX = 20;
        private static Ellipse rightKnob;
        private static Ellipse bottomKnob;
        private static Line helperLine1;
        private static Line helperLine2;

        const int defaultWidth = 680;
        const int defaultHeight = 540;
        const int maxWidth = 1123;
        const int maxHeight = 920;

        public static bool ShowSin = true;
        public static bool ShowCos = true;
        public static bool ShowTan = true;
        public static bool ShowCot = true;
        public static bool EnableSnapping = true;

        public static bool SinConnector = false;
        public static bool CosConnector = false;
        public static bool TanConnector = false;
        public static bool CotConnector = false;

        public static bool ShowSinCosFunc = false;
        public static bool ShowTanCotFunc = false;

        private static bool IsDragging = false;
        private static bool ChangedByDrag = false;
        private static double Alpha = 0;

        public MainWindow() {
            InitializeComponent();

            mainCanvas.MouseMove += MainCanvas_MouseMove;

            AssignToggleButtons();
        }

        private void AssignToggleButtons() {
            sinColor.Fill = Settings.sinBrush;
            sinColor.Cursor = Cursors.Hand;
            sinColor.MouseLeftButtonDown += (sender, e) => {
                ToggleSin(!ShowSin);
            };

            cosColor.Fill = Settings.cosBrush;
            cosColor.Cursor = Cursors.Hand;
            cosColor.MouseLeftButtonDown += (sender, e) => {
                ToggleCos(!ShowCos);
            };

            tanColor.Fill = Settings.tanBrush;
            tanColor.Cursor = Cursors.Hand;
            tanColor.MouseLeftButtonDown += (sender, e) => {
                ToggleTan(!ShowTan);
            };

            cotColor.Fill = Settings.cotBrush;
            cotColor.Cursor = Cursors.Hand;
            cotColor.MouseLeftButtonDown += (sender, e) => {
                ToggleCot(!ShowCot);
            };

            snappingToggle.Fill = Settings.snappingToggleBrush;
            snappingToggle.Cursor = Cursors.Hand;
            snappingToggle.MouseLeftButtonDown += (sender, e) => {
                ToggleSnapping(!EnableSnapping);
            };

            funcSinCos.Cursor = Cursors.Hand;
            funcSinCos.MouseLeftButtonDown += (sender, e) => {
                if (ShowSinCosFunc) {
                    ShowSinCosFunc = false;
                    Rectangle bg = funcSinCos.Children[0] as Rectangle;
                    bg.Fill = Settings.deactivatedToggleBrush;
                    HideAdditionalElements();
                    EnableConnectors(false, false);
                    GenerateCanvasDrawing(Alpha);

                    ToggleSin(true);
                    ToggleCos(true);
                    ToggleTan(true);
                    ToggleCot(true);
                } else {
                    if (ShowTanCotFunc) {
                        ShowTanCotFunc = false;
                        Rectangle bgTanCot = funcTanCot.Children[0] as Rectangle;
                        bgTanCot.Fill = Settings.deactivatedToggleBrush;
                    } else {
                        ShowAdditionalElements();
                    }

                    ShowSinCosFunc = true;
                    Rectangle bg = funcSinCos.Children[0] as Rectangle;
                    bg.Fill = Settings.activatedToggleBrush;

                    SetupSinCosProjection();

                    secRigthLabel.Content = "Szinusz függvény";
                    secBottomLabel.Content = "Koszinusz függvény";
                }
            };

            funcTanCot.Cursor = Cursors.Hand;
            funcTanCot.MouseLeftButtonDown += (sender, e) => {
                if (ShowTanCotFunc) {
                    ShowTanCotFunc = false;
                    Rectangle bg = funcTanCot.Children[0] as Rectangle;
                    bg.Fill = Settings.deactivatedToggleBrush;
                    HideAdditionalElements();
                    EnableConnectors(false, false);
                    GenerateCanvasDrawing(Alpha);

                    ToggleSin(true);
                    ToggleCos(true);
                    ToggleTan(true);
                    ToggleCot(true);
                } else {
                    if (ShowSinCosFunc) {
                        ShowSinCosFunc = false;
                        Rectangle bgSinCos = funcSinCos.Children[0] as Rectangle;
                        bgSinCos.Fill = Settings.deactivatedToggleBrush;
                    } else {
                        ShowAdditionalElements();
                    }

                    ShowTanCotFunc = true;
                    Rectangle bg = funcTanCot.Children[0] as Rectangle;
                    bg.Fill = Settings.activatedToggleBrush;

                    SetupTanCotProjection();

                    secRigthLabel.Content = "Tangens függvény";
                    secBottomLabel.Content = "Kotangens függvény";
                }
            };
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
                dragKnob = new Ellipse() {
                    Width = 8,
                    Height = 8,
                    Stroke = ColourPalette.BrushRGB(20, 20, 20),
                    StrokeThickness = 1,
                    Fill = ColourPalette.BrushRGB(220, 220, 220),
                    Uid = "dragKnob",
                    Cursor = Cursors.Hand
                };
                mainCanvas.Children.Add(dragKnob);
            }
            Canvas.SetLeft(dragKnob, CenterX + Math.Cos(alpha) * 100 - 4);
            Canvas.SetTop(dragKnob, CenterY - Math.Sin(alpha) * 100 - 4);
            Panel.SetZIndex(dragKnob, 100);

            List<MathDefinition> shapesToDraw = new List<MathDefinition>() {
                new MathCollection.Basics(),
                new MathCollection.PrimaryAdditions(),
                new MathCollection.MathSin(),
                new MathCollection.MathCos(),
                new MathCollection.MathTan(),
                new MathCollection.MathCot(),
                new MathCollection.CenterCircle(),
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

            deleteList = new List<UIElement>();
            foreach (UIElement element in containerGrid.Children) {
                if (element.Uid.StartsWith("connector")) {
                    deleteList.Add(element);
                }
            }
            foreach (UIElement element in deleteList) {
                containerGrid.Children.Remove(element);
            }

            if (SinConnector) {
                double startX = mainBorder.Margin.Left + Canvas.GetLeft(dragKnob) + 4;
                double startY = mainBorder.Margin.Top + Canvas.GetTop(dragKnob) + 4;
                double endX = secRightBorder.Margin.Left + ShiftedCenterX + ConvertToDegrees(alpha);
                double endY = secRightBorder.Margin.Top + 200 - Math.Sin(alpha) * 100;

                if (rightKnob == null) {
                    rightKnob = new Ellipse() {
                        Width = 8,
                        Height = 8,
                        Stroke = ColourPalette.BrushRGB(20, 20, 20),
                        StrokeThickness = 1,
                        Fill = ColourPalette.BrushRGB(220, 220, 220),
                        Uid = "connectorSinKnob"
                    };
                    secRightCanvas.Children.Add(rightKnob);
                }
                Canvas.SetLeft(rightKnob, endX - secRightBorder.Margin.Left - 4);
                Canvas.SetTop(rightKnob, endY - secRightBorder.Margin.Top - 4);

                Line sinConnector = new Line() {
                    Uid = "connectorSin",
                    X1 = startX + 6,
                    Y1 = startY + 2,
                    X2 = endX - 2,
                    Y2 = endY + 2,
                    Stroke = Settings.sinBrush,
                    StrokeThickness = 2,
                    StrokeDashArray = new DoubleCollection() { 4, 2 },
                };
                containerGrid.Children.Add(sinConnector);
                Panel.SetZIndex(sinConnector, 10);
            }

            bool canShowTan = Math.Abs(Math.Tan(alpha) * 100) <= 200;
            if (!canShowTan) {
                secRightCanvas.Children.Remove(rightKnob);
                rightKnob = null;
            }
            if (TanConnector && Math.Abs(ConvertToDegrees(alpha)) % 90 != 0 && canShowTan) {
                double startX = mainBorder.Margin.Left + CenterX + 100;
                double startY = mainBorder.Margin.Top + CenterY - Math.Max(-200, Math.Min(Math.Tan(alpha) * 100, 200));
                double endX = secRightBorder.Margin.Left + ShiftedCenterX + ConvertToDegrees(alpha);

                double lineY = Math.Min(startY + 2, mainBorder.Margin.Top + 398);

                if (rightKnob == null) {
                    rightKnob = new Ellipse() {
                        Width = 8,
                        Height = 8,
                        Stroke = ColourPalette.BrushRGB(20, 20, 20),
                        StrokeThickness = 1,
                        Fill = ColourPalette.BrushRGB(220, 220, 220),
                        Uid = "connectorTanKnob"
                    };
                    secRightCanvas.Children.Add(rightKnob);
                }
                Canvas.SetLeft(rightKnob, endX - secRightBorder.Margin.Left - 4);
                Canvas.SetTop(rightKnob, startY - secRightBorder.Margin.Top - 4);

                Line tanConnector = new Line() {
                    Uid = "connectorTan",
                    X1 = startX,
                    Y1 = lineY,
                    X2 = endX,
                    Y2 = lineY,
                    Stroke = Settings.tanBrush,
                    StrokeThickness = 2,
                    StrokeDashArray = new DoubleCollection() { 4, 2 },
                };
                containerGrid.Children.Add(tanConnector);
                Panel.SetZIndex(tanConnector, 10);
            }

            if (CosConnector) {
                // Bottom knob
                double knobX = secBottomBorder.Margin.Left + ShiftedCenterX + ConvertToDegrees(alpha);
                double knobY = secBottomBorder.Margin.Top + CenterY - Math.Cos(alpha) * 100;
                if (bottomKnob == null) {
                    bottomKnob = new Ellipse() {
                        Width = 8,
                        Height = 8,
                        Stroke = ColourPalette.BrushRGB(20, 20, 20),
                        StrokeThickness = 1,
                        Fill = ColourPalette.BrushRGB(220, 220, 220),
                        Uid = "connectorCosKnob"
                    };
                    secBottomCanvas.Children.Add(bottomKnob);
                }
                Canvas.SetLeft(bottomKnob, knobX - 4 - secBottomBorder.Margin.Left);
                Canvas.SetTop(bottomKnob, knobY - 4 - secBottomBorder.Margin.Top);

                // Line: knob -> curveStart
                double line1X = mainBorder.Margin.Left + Canvas.GetLeft(dragKnob) + 6;
                double line1Y = mainBorder.Margin.Top + Canvas.GetTop(dragKnob) + 10;
                double curveStartX = line1X;
                double curveStartY = secBottomBorder.Margin.Top;

                Line cosConnector1 = new Line() {
                    Uid = "connectorCosLine1",
                    X1 = line1X,
                    Y1 = line1Y,
                    X2 = curveStartX,
                    Y2 = curveStartY,
                    Stroke = Settings.cosBrush,
                    StrokeThickness = 2,
                    StrokeDashArray = new DoubleCollection() { 4, 2 },
                };
                containerGrid.Children.Add(cosConnector1);
                Panel.SetZIndex(cosConnector1, 10);

                // Curve: bezier
                double curveEndX = mainBorder.Margin.Left + mainBorder.ActualWidth;
                double curveEndY = knobY;
                List<Point> points = new List<Point>() {
                    new Point(curveStartX, curveStartY),
                    new Point(curveStartX, curveEndY),
                    new Point(curveEndX, curveEndY)
                };
                Path bezierPath = CreateBezierPath(points);
                bezierPath.Stroke = Settings.cosBrush;
                bezierPath.StrokeThickness = 2;
                bezierPath.Uid = "connectorCosCurve";
                bezierPath.StrokeDashArray = new DoubleCollection() { 4, 2 };
                containerGrid.Children.Add(bezierPath);


                // Line: curveEnd -> bottomKnob
                Line cosConnector2 = new Line() {
                    Uid = "connectorCosLine2",
                    X1 = curveEndX,
                    Y1 = curveEndY,
                    X2 = knobX,
                    Y2 = knobY,
                    Stroke = Settings.cosBrush,
                    StrokeThickness = 2,
                    StrokeDashArray = new DoubleCollection() { 4, 2 },
                };
                containerGrid.Children.Add(cosConnector2);
                Panel.SetZIndex(cosConnector2, 10);
            }

            bool canShowCot = Math.Abs(1 / Math.Tan(alpha) * 100) <= 200;
            if (!canShowCot) {
                secBottomCanvas.Children.Remove(bottomKnob);
                bottomKnob = null;
            }
            if (CotConnector && Math.Abs(ConvertToDegrees(alpha)) % 90 != 0 && canShowCot) {
                // Bottom knob
                double knobX = secBottomBorder.Margin.Left + ShiftedCenterX + ConvertToDegrees(alpha);
                double knobY = secBottomBorder.Margin.Top + CenterY - Math.Max(-200, Math.Min(1 / Math.Tan(alpha) * 100, 200));
                if (bottomKnob == null) {
                    bottomKnob = new Ellipse() {
                        Width = 8,
                        Height = 8,
                        Stroke = ColourPalette.BrushRGB(20, 20, 20),
                        StrokeThickness = 1,
                        Fill = ColourPalette.BrushRGB(220, 220, 220),
                        Uid = "connectorCotKnob"
                    };
                    secBottomCanvas.Children.Add(bottomKnob);
                }
                Canvas.SetLeft(bottomKnob, knobX - 4 - secBottomBorder.Margin.Left);
                Canvas.SetTop(bottomKnob, knobY - 4 - secBottomBorder.Margin.Top);

                // Line: knob -> curveStart
                double line1X = mainBorder.Margin.Left + CenterX + Math.Min(Math.Max(-200, Math.Min(1 / Math.Tan(alpha) * 100, 200)) + 2, 198);
                double line1Y = mainBorder.Margin.Top + CenterY - 100;
                double curveStartX = line1X;
                double curveStartY = secBottomBorder.Margin.Top;

                Line cotConnector1 = new Line() {
                    Uid = "connectorCotLine1",
                    X1 = line1X,
                    Y1 = line1Y,
                    X2 = curveStartX,
                    Y2 = curveStartY,
                    Stroke = Settings.cotBrush,
                    StrokeThickness = 2,
                    StrokeDashArray = new DoubleCollection() { 4, 2 },
                };
                containerGrid.Children.Add(cotConnector1);
                Panel.SetZIndex(cotConnector1, 10);

                // Curve: bezier
                double curveEndX = mainBorder.Margin.Left + mainBorder.ActualWidth;
                double curveEndY = knobY;
                List<Point> points = new List<Point>() {
                    new Point(curveStartX, curveStartY),
                    new Point(curveStartX, curveEndY),
                    new Point(curveEndX, curveEndY)
                };
                Path bezierPath = CreateBezierPath(points);
                bezierPath.Stroke = Settings.cotBrush;
                bezierPath.StrokeThickness = 2;
                bezierPath.Uid = "connectorCotCurve";
                bezierPath.StrokeDashArray = new DoubleCollection() { 4, 2 };
                containerGrid.Children.Add(bezierPath);


                // Line: curveEnd -> bottomKnob
                Line cotConnector2 = new Line() {
                    Uid = "connectorCotLine2",
                    X1 = curveEndX,
                    Y1 = curveEndY,
                    X2 = knobX,
                    Y2 = knobY,
                    Stroke = Settings.cotBrush,
                    StrokeThickness = 2,
                    StrokeDashArray = new DoubleCollection() { 4, 2 },
                };
                containerGrid.Children.Add(cotConnector2);
                Panel.SetZIndex(cotConnector2, 10);
            }
        }

        private void MainCanvas_MouseMove(object sender, MouseEventArgs e) {
            Point mp = Mouse.GetPosition(mainCanvas);
            double a = CenterY - mp.Y;
            double b = mp.X - CenterX;
            double alpha = Math.Atan(Math.Abs(a / b));

            alpha = double.IsNaN(alpha) ? 0 : alpha;

            if (a >= 0 && b < 0) {
                alpha = ConvertToRadians(180) - alpha;
            } else if (a < 0 && b < 0) {
                alpha = ConvertToRadians(180) + alpha;
            } else if (a < 0 && b >= 0) {
                alpha = ConvertToRadians(360) - alpha;
            }

            if (Mouse.DirectlyOver is System.Windows.Shapes.Ellipse) {
                Ellipse ell = (Ellipse)Mouse.DirectlyOver;
                if (ell.Uid == "dragKnob") {
                    if (IsDragging == false && e.LeftButton == MouseButtonState.Pressed) {
                        IsDragging = true;
                    }
                }
            }
            if (IsDragging == true && e.LeftButton == MouseButtonState.Released) {
                IsDragging = false;
                Mouse.OverrideCursor = null;
            }

            double threshold = 4;
            double distanceFrom15 = ConvertToDegrees(alpha) % 15;
            if (EnableSnapping && (distanceFrom15 < threshold || distanceFrom15 > 15 - threshold)) {
                alpha = ConvertToRadians(Math.Round(ConvertToDegrees(alpha) / 15) * 15);
            }

            if (IsDragging) {
                UpdateText(alpha);
                GenerateCanvasDrawing(alpha);
                Alpha = alpha;
                angleInput.Text = Convert.ToString(Math.Round(ConvertToDegrees(alpha) * 100) / 100);
                ChangedByDrag = true;
                Mouse.OverrideCursor = Cursors.Hand;
            }
        }

        private void UpdateText(double rad) {
            int roundDecimals = 4;
            double angle = ConvertToDegrees(rad);

            string tan = angle % 180 == 90 ? "-" : Math.Round(Math.Tan(rad), roundDecimals).ToString();
            string cot = angle % 180 == 0 ? "-" : Math.Round(1 / Math.Tan(rad), roundDecimals).ToString();

            lb_v_sin.Content = Math.Round(Math.Sin(rad), roundDecimals);
            lb_v_cos.Content = Math.Round(Math.Cos(rad), roundDecimals);
            lb_v_tg.Content = tan;
            lb_v_ctg.Content = cot;
        }

        private void angleInput_TextChanged(object sender, TextChangedEventArgs e) {
            if (ChangedByDrag) {
                ChangedByDrag = false;
                return;
            }
            if (double.TryParse(angleInput.Text, out double angle)) {
                angleInput.BorderBrush = System.Windows.Media.Brushes.LightBlue;
                angleInput.SelectionBrush = System.Windows.Media.Brushes.LightBlue;
                angleInput.Background = Brushes.White;

                double rad = ConvertToRadians(angle);

                UpdateText(rad);
                GenerateCanvasDrawing(rad);
                Alpha = rad;

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
        public static double ConvertToRadians(double angle) {
            return (Math.PI / 180) * angle;
        }
        public static double ConvertToDegrees(double radian) {
            return radian / (Math.PI / 180);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            angleInput.Text = "0";
        }

        private void ShowAdditionalElements() {
            Width = maxWidth;
            Height = maxHeight;
            secRightBorder.Visibility = Visibility.Visible;
            secBottomBorder.Visibility = Visibility.Visible;
            secRigthLabel.Visibility = Visibility.Visible;
            secBottomLabel.Visibility = Visibility.Visible;

            Line l1 = new Line() {
                X1 = mainBorder.Margin.Left,
                Y1 = secBottomBorder.Margin.Top,

                X2 = secBottomBorder.Margin.Left,
                Y2 = secBottomBorder.Margin.Top,
                Stroke = Settings.coordLineBrush,
                StrokeThickness = 1,
                StrokeDashArray = new DoubleCollection() { 6, 3 },
            };
            Line l2 = new Line() {
                X1 = mainBorder.Margin.Left + mainBorder.ActualWidth,
                Y1 = mainBorder.Margin.Top + mainBorder.ActualHeight,

                X2 = mainBorder.Margin.Left + mainBorder.ActualWidth,
                Y2 = secBottomBorder.Margin.Top + secBottomBorder.ActualHeight,
                Stroke = Settings.coordLineBrush,
                StrokeThickness = 1,
                StrokeDashArray = new DoubleCollection() { 6, 3 },
            };
            containerGrid.Children.Add(l1);
            containerGrid.Children.Add(l2);

            helperLine1 = l1;
            helperLine2 = l2;
        }

        private void HideAdditionalElements() {
            Width = defaultWidth;
            Height = defaultHeight;
            secRightBorder.Visibility = Visibility.Hidden;
            secBottomBorder.Visibility = Visibility.Hidden;
            secRigthLabel.Visibility = Visibility.Hidden;
            secBottomLabel.Visibility = Visibility.Hidden;

            containerGrid.Children.Remove(helperLine1);
            containerGrid.Children.Remove(helperLine2);
        }

        private void SetupSinCosProjection() {
            secRightCanvas.Children.Clear();
            secBottomCanvas.Children.Clear();
            rightKnob = null;
            bottomKnob = null;

            SetupSinCanvas();
            SetupCosCanvas();

            EnableConnectors(true, false);
            GenerateCanvasDrawing(Alpha);

            ToggleSin(true);
            ToggleCos(true);
            ToggleTan(false);
            ToggleCot(false);
        }

        private void SetupSinCanvas() {
            List<MathDefinition> shapesToDraw = new List<MathDefinition>() {
                new MathCollection.Basics(),
                new MathCollection.SinWave(),
                new MathCollection.RadGrid(),
            };

            SetupSecondaryCanvas(shapesToDraw, secRightCanvas);
        }

        private void SetupCosCanvas() {
            List<MathDefinition> shapesToDraw = new List<MathDefinition>() {
                new MathCollection.Basics(),
                new MathCollection.CosWave(),
                new MathCollection.RadGrid(),
            };

            SetupSecondaryCanvas(shapesToDraw, secBottomCanvas);
        }

        private void SetupTanCotProjection() {
            secRightCanvas.Children.Clear();
            secBottomCanvas.Children.Clear();
            rightKnob = null;
            bottomKnob = null;

            SetupTanCanvas();
            SetupCotCanvas();

            EnableConnectors(false, true);
            GenerateCanvasDrawing(Alpha);

            ToggleSin(false);
            ToggleCos(false);
            ToggleTan(true);
            ToggleCot(true);
        }

        private void SetupTanCanvas() {
            List<MathDefinition> shapesToDraw = new List<MathDefinition>() {
                new MathCollection.Basics(),
                new MathCollection.TanWave(),
                new MathCollection.RadGrid(),
            };

            SetupSecondaryCanvas(shapesToDraw, secRightCanvas);
        }

        private void SetupCotCanvas() {
            List<MathDefinition> shapesToDraw = new List<MathDefinition>() {
                new MathCollection.Basics(),
                new MathCollection.CotWave(),
                new MathCollection.RadGrid(),
            };

            SetupSecondaryCanvas(shapesToDraw, secBottomCanvas);
        }

        private void SetupSecondaryCanvas(List<MathDefinition> shapesToDraw, Canvas canvas) {
            foreach (MathDefinition shapeCollection in shapesToDraw) {
                Dictionary<Shape, ShapeParams> shapes = shapeCollection.ShapeCollection(ShiftedCenterX, CenterY, Alpha, ConvertToDegrees(Alpha), canvas.ActualWidth, canvas.ActualHeight);
                foreach (Shape shape in shapes.Keys) {
                    ShapeParams shapeParam = shapes[shape];
                    canvas.Children.Add(shape);
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

            Dictionary<TextBlock, ShapeParams> textCollection = new MathCollection.TextCollection().GetTextCollection(ShiftedCenterX, CenterY, canvas.ActualWidth, canvas.ActualHeight);
            foreach (TextBlock textBlock in textCollection.Keys) {
                ShapeParams shapeParam = textCollection[textBlock];
                canvas.Children.Add(textBlock);
                if (shapeParam.ChangeLeft) {
                    Canvas.SetLeft(textBlock, shapeParam.Left);
                }
                if (shapeParam.ChangeTop) {
                    Canvas.SetTop(textBlock, shapeParam.Top);
                }
            }

            Dictionary<TextBlock, ShapeParams> textCollection2 = new MathCollection.RadGridText().GetTextCollection(ShiftedCenterX, CenterY, canvas.ActualWidth, canvas.ActualHeight);
            foreach (TextBlock textBlock in textCollection2.Keys) {
                ShapeParams shapeParam = textCollection2[textBlock];
                canvas.Children.Add(textBlock);
                if (shapeParam.ChangeLeft) {
                    Canvas.SetLeft(textBlock, shapeParam.Left);
                }
                if (shapeParam.ChangeTop) {
                    Canvas.SetTop(textBlock, shapeParam.Top);
                }
            }
        }

        private void EnableConnectors(bool sinCos, bool tanCot) {
            SinConnector = sinCos;
            CosConnector = sinCos;
            TanConnector = tanCot;
            CotConnector = tanCot;
        }

        private void ToggleSin(bool toggle) {
            ShowSin = toggle;

            if (toggle) {
                sinColor.Fill = Settings.sinBrush;
            } else {
                sinColor.Fill = Settings.baseColorBrush;
            }

            GenerateCanvasDrawing(Alpha);
        }

        private void ToggleCos(bool toggle) {
            ShowCos = toggle;

            if (toggle) {
                cosColor.Fill = Settings.cosBrush;
            } else {
                cosColor.Fill = Settings.baseColorBrush;
            }

            GenerateCanvasDrawing(Alpha);
        }

        private void ToggleTan(bool toggle) {
            ShowTan = toggle;

            if (toggle) {
                tanColor.Fill = Settings.tanBrush;
            } else {
                tanColor.Fill = Settings.baseColorBrush;
            }

            GenerateCanvasDrawing(Alpha);
        }

        private void ToggleCot(bool toggle) {
            ShowCot = toggle;

            if (toggle) {
                cotColor.Fill = Settings.cotBrush;
            } else {
                cotColor.Fill = Settings.baseColorBrush;
            }

            GenerateCanvasDrawing(Alpha);
        }

        private void ToggleSnapping(bool toggle) {
            EnableSnapping = toggle;

            if (toggle) {
                snappingToggle.Fill = Settings.snappingToggleBrush;
            } else {
                snappingToggle.Fill = Settings.baseColorBrush;
            }
        }

        private Path CreateBezierPath(List<Point> points) {
            PolyBezierSegment pqbz = new PolyBezierSegment() {
                Points = new PointCollection(points),
            };

            PathSegmentCollection psc = new PathSegmentCollection();
            psc.Add(pqbz);

            PathFigure pf = new PathFigure();
            pf.StartPoint = points[0];
            pf.Segments = psc;

            PathFigureCollection pfc = new PathFigureCollection();
            pfc.Add(pf);

            PathGeometry pg = new PathGeometry();
            pg.Figures = pfc;

            Path path = new Path() {
                Data = pg
            };

            return path;
        }
    }
}
