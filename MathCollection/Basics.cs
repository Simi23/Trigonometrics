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
    class Basics : MathDefinition
    {
        static Brush baseBrush = Settings.baseColorBrush;
        public Dictionary<Shape, ShapeParams> ShapeCollection(double CenterX, double CenterY, double alpha, double deg, double CanvasWidth, double CanvasHeight)
        {
            Dictionary<Shape, ShapeParams> shapeCollection = new Dictionary<Shape, ShapeParams>();

            // Main Circle
            Ellipse circleShape = new Ellipse()
            {
                Width = 200,
                Height = 200,
                Stroke = Settings.circleBrush,
                StrokeThickness = 3
            };
            ShapeParams circleDef = new ShapeParams() {
                IndexZ = 0,
                Left = CenterX - 100,
                Top = CenterY - 100,
                ChangeLeft = true,
                ChangeTop = true
            };
            shapeCollection.Add(circleShape, circleDef);

            // Base Line
            Line baseLineShape = new Line()
            {
                X1 = CenterX,
                Y1 = CenterY,

                X2 = CenterX + 100,
                Y2 = CenterY,
                Stroke = baseBrush,
                StrokeThickness = 2
            };
            ShapeParams baseLineDef = new ShapeParams()
            {
                IndexZ = 1
            };
            shapeCollection.Add(baseLineShape, baseLineDef);

            // Secondary Line
            Line secLineShape = new Line()
            {
                X1 = CenterX,
                Y1 = CenterY,

                X2 = CenterX + Math.Cos(alpha) * 100,
                Y2 = CenterY - Math.Sin(alpha) * 100,
                Stroke = baseBrush,
                StrokeThickness = 2
            };
            ShapeParams secLineDef = new ShapeParams()
            {
                IndexZ = 1
            };
            shapeCollection.Add(secLineShape, secLineDef);

            // Coord line x
            Line coordLineXShape = new Line()
            {
                X1 = 0,
                Y1 = CenterY,

                X2 = CanvasWidth,
                Y2 = CenterY,
                Stroke = Settings.coordLineBrush,
                StrokeDashArray = new DoubleCollection() { 6, 3 },
                StrokeThickness = 1
            };
            ShapeParams coordLineXDef = new ShapeParams()
            {
                IndexZ = -1
            };
            shapeCollection.Add(coordLineXShape, coordLineXDef);

            // Coord line y
            Line coordLineYShape = new Line()
            {
                X1 = CenterX,
                Y1 = 0,

                X2 = CenterX,
                Y2 = CanvasHeight,
                Stroke = Settings.coordLineBrush,
                StrokeDashArray = new DoubleCollection() { 6, 3 },
                StrokeThickness = 1
            };
            ShapeParams coordLineYDef = new ShapeParams()
            {
                IndexZ = -1
            };
            shapeCollection.Add(coordLineYShape, coordLineYDef);

            return shapeCollection;
        }
    }
}
