using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Trigonometrics.MathCollection {
    class CenterCircle : MathDefinition {
        public Dictionary<Shape, ShapeParams> ShapeCollection(double CenterX, double CenterY, double alpha, double deg, double CanvasWidth, double CanvasHeight) {
            Dictionary<Shape, ShapeParams> shapeCollection = new Dictionary<Shape, ShapeParams>();

            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";

            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;


            Path path = new Path();
            double endX = Math.Round(CenterX + Math.Cos(alpha) * 25, 2);
            double endY = Math.Round(CenterY - Math.Sin(alpha) * 25, 2);
            path.Data = Geometry.Parse($"M{CenterX},{CenterY} L{CenterX + 25},{CenterY} A25,25 0 {(deg >= 180 ? "1" : "0")} 0 {endX},{endY} z");
            path.Fill = Settings.angleCircleBrush;

            ShapeParams p = new ShapeParams() { };
            shapeCollection.Add(path, p);

            return shapeCollection;
        }
    }
}
