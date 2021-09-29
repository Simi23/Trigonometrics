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

namespace Trigonometrics.MathCollection {
    class CenterCircle : MathDefinition {
        public Dictionary<Shape, ShapeParams> ShapeCollection(double CenterX, double CenterY, double alpha, double deg, double CanvasWidth, double CanvasHeight) {
            Dictionary<Shape, ShapeParams> shapeCollection = new Dictionary<Shape, ShapeParams>();

            Path path = new Path();
            path.Data = Geometry.Parse($"M{CenterX},{CenterY} L{CenterX + 25},{CenterY} A25,25 0 {(deg >= 180 ? "1" : "0")} 0 {CenterX + Math.Cos(alpha) * 25},{CenterY - Math.Sin(alpha) * 25} z");
            path.Fill = Settings.angleCircleBrush;

            ShapeParams p = new ShapeParams() { };
            shapeCollection.Add(path, p);

            return shapeCollection;
        }
    }
}
