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

namespace Trigonometrics.MathCollection
{
    class MathTan : MathDefinition
    {
        // 0 162 232
        static Brush brush = ColourPalette.BrushRGB(0, 162, 232);
        public Dictionary<Shape, ShapeParams> ShapeCollection(double CenterX, double CenterY, double alpha, double deg, double CanvasWidth, double CanvasHeight)
        {
            Dictionary<Shape, ShapeParams> shapeCollection = new Dictionary<Shape, ShapeParams>();

            bool tanRight = DetermineTanRight(deg);
            bool showTanHelp = DetermineShowTanHelper(deg);
            
            double tanX1, tanX2, tanY1, tanY2;

            double tan = tanRight ? Math.Tan(alpha) : Math.Tan(alpha) * -1;

            if (tanRight) {
                tanX1 = CenterX + 100;
                tanY1 = CenterY;
                tanX2 = CenterX + 100;
                tanY2 = CenterY - tan * 100;
            }
            else {
                tanX1 = CenterX - 100;
                tanY1 = CenterY;
                tanX2 = CenterX - 100;
                tanY2 = CenterY - tan * 100;
            }

            if (Math.Abs(deg) % 180 - 90 == 0) {
                tanY2 = Math.Min(CanvasHeight, Math.Max(0, tanY2));
            }

            Line tanLineShape = new Line()
            {
                X1 = tanX1,
                Y1 = tanY1,

                X2 = tanX2,
                Y2 = tanY2,
                Stroke = brush,
                StrokeThickness = 3
            };
            ShapeParams tanLineDef = new ShapeParams() {
                IndexZ = 1
            };
            shapeCollection.Add(tanLineShape, tanLineDef);

            if (showTanHelp) {
                // Helper line
                Line tanHelperShape = new Line()
                {
                    X1 = CenterX,
                    Y1 = CenterY,

                    X2 = tanX2,
                    Y2 = tanY2,
                    Stroke = Brushes.Gray,
                    StrokeDashArray = new DoubleCollection() { 6, 3 },
                    StrokeThickness = 1
                };
                ShapeParams tanHelperDef = new ShapeParams()
                {
                    IndexZ = -1
                };
                shapeCollection.Add(tanHelperShape, tanHelperDef);
            }

            return shapeCollection;
        }

        public bool DetermineTanRight(double angle)
        {
            double tempAngle = angle % 360;
            if (tempAngle < 0) {
                tempAngle += 360;
            }

            if (tempAngle <= 90) {
                return true;
            }
            else if (tempAngle < 180) {
                return false;
            }
            else if (tempAngle <= 270) {
                return false;
            }
            else {
                return true;
            }
        }

        public bool DetermineShowTanHelper(double angle)
        {
            return Math.Abs(angle) % 90 != 0;
        }
    }
}
