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
    class MathCot : MathDefinition
    {
        // 255 127 39
        static Brush brush = ColourPalette.BrushRGB(255, 127, 39);
        public Dictionary<Shape, ShapeParams> ShapeCollection(double CenterX, double CenterY, double alpha, double deg, double CanvasWidth, double CanvasHeight)
        {
            Dictionary<Shape, ShapeParams> shapeCollection = new Dictionary<Shape, ShapeParams>();

            bool cotUp = DetermineCotUp(deg);
            bool showCotHelp = DetermineShowCotHelper(deg);

            Brush tanBrush = Math.Tan(alpha) >= 0 ? Brushes.OrangeRed : Brushes.CornflowerBlue;
            double cotX1, cotX2, cotY1, cotY2;

            double cot = cotUp ? 1 / Math.Tan(alpha) : -1 / Math.Tan(alpha);

            if (cotUp) {
                cotX1 = CenterX;
                cotY1 = CenterY - 100;
                cotX2 = CenterX + cot * 100;
                cotY2 = CenterY - 100;
            }
            else {
                cotX1 = CenterX;
                cotY1 = CenterY + 100;
                cotX2 = CenterX + cot * 100;
                cotY2 = CenterY + 100;
            }

            if (Math.Abs(deg) % 180 == 0) {
                cotX2 = Math.Min(CanvasHeight, Math.Max(0, cotX2));
            }

            Line cotLineShape = new Line()
            {
                X1 = cotX1,
                Y1 = cotY1,

                X2 = cotX2,
                Y2 = cotY2,
                Stroke = tanBrush,
                StrokeThickness = 3
            };
            ShapeParams cotLineDef = new ShapeParams()
            {
                IndexZ = 1
            };
            shapeCollection.Add(cotLineShape, cotLineDef);

            if (showCotHelp) {
                // Helper line
                Line tanHelperShape = new Line()
                {
                    X1 = CenterX,
                    Y1 = CenterY,

                    X2 = cotX2,
                    Y2 = cotY2,
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

        public bool DetermineCotUp(double angle)
        {
            double tempAngle = angle % 360;
            if (tempAngle < 0) {
                tempAngle += 360;
            }

            if (tempAngle <= 90) {
                return true;
            }
            else if (tempAngle < 180) {
                return true;
            }
            else if (tempAngle <= 270) {
                return false;
            }
            else {
                return false;
            }
        }

        public bool DetermineShowCotHelper(double angle)
        {
            return Math.Abs(angle) % 90 != 0;
        }
    }
}
