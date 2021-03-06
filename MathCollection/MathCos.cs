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
    class MathCos : MathDefinition {
        static Brush brush = Settings.cosBrush;

        public Dictionary<Shape, ShapeParams> ShapeCollection(double CenterX, double CenterY, double alpha, double deg, double CanvasWidth, double CanvasHeight) {
            Dictionary<Shape, ShapeParams> shapeCollection = new Dictionary<Shape, ShapeParams>();

            if (!MainWindow.ShowCos) {
                return shapeCollection;
            }

            Line cosShape = new Line() {
                X1 = CenterX,
                Y1 = CenterY,

                X2 = CenterX + Math.Cos(alpha) * 100,
                Y2 = CenterY,
                Stroke = brush,
                StrokeThickness = 3
            };
            ShapeParams cosDef = new ShapeParams() {
                IndexZ = 1,
            };
            shapeCollection.Add(cosShape, cosDef);

            Line cosHelperShape = new Line() {
                X1 = CenterX + Math.Cos(alpha) * 100,
                Y1 = CenterY,

                X2 = CenterX + Math.Cos(alpha) * 100,
                Y2 = CenterY - Math.Sin(alpha) * 100,
                Stroke = brush,
                StrokeDashArray = new DoubleCollection() { 6, 3 },
                StrokeThickness = 1
            };
            ShapeParams cosHelperDef = new ShapeParams() {
                IndexZ = 1,
            };
            shapeCollection.Add(cosHelperShape, cosHelperDef);

            return shapeCollection;
        }
    }
}
