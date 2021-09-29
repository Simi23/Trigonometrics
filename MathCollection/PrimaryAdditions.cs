using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace Trigonometrics.MathCollection {
    class PrimaryAdditions : MathDefinition {
        public Dictionary<Shape, ShapeParams> ShapeCollection(double CenterX, double CenterY, double alpha, double deg, double CanvasWidth, double CanvasHeight) {
            Dictionary<Shape, ShapeParams> shapeCollection = new Dictionary<Shape, ShapeParams>();

            // Main Circle
            Ellipse circleShape = new Ellipse() {
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
            Line baseLineShape = new Line() {
                X1 = CenterX,
                Y1 = CenterY,

                X2 = CenterX + 100,
                Y2 = CenterY,
                Stroke = Settings.baseColorBrush,
                StrokeThickness = 2
            };
            ShapeParams baseLineDef = new ShapeParams() {
                IndexZ = 1
            };
            shapeCollection.Add(baseLineShape, baseLineDef);

            // Secondary Line
            Line secLineShape = new Line() {
                X1 = CenterX,
                Y1 = CenterY,

                X2 = CenterX + Math.Cos(alpha) * 100,
                Y2 = CenterY - Math.Sin(alpha) * 100,
                Stroke = Settings.baseColorBrush,
                StrokeThickness = 2
            };
            ShapeParams secLineDef = new ShapeParams() {
                IndexZ = 1
            };
            shapeCollection.Add(secLineShape, secLineDef);

            return shapeCollection;
        }
    }
}
