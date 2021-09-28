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
    class RadGrid : MathDefinition
    {
        public Dictionary<Shape, ShapeParams> ShapeCollection(double CenterX, double CenterY, double alpha, double deg, double CanvasWidth, double CanvasHeight)
        {
            Dictionary<Shape, ShapeParams> shapeCollection = new Dictionary<Shape, ShapeParams>();

            List<int> points = new List<int>()
            {
                90, 180, 270, 360
            };

            foreach (int p in points)
            {
                Line l = new Line()
                {
                    X1 = CenterX + p,
                    Y1 = 0,

                    X2 = CenterX + p,
                    Y2 = CanvasHeight,
                    Stroke = Settings.coordLineBrush,
                    StrokeThickness = 1,
                    StrokeDashArray = new DoubleCollection() { 6, 3 },
                };
                ShapeParams par = new ShapeParams()
                {
                    IndexZ = -1
                };
                shapeCollection.Add(l, par);
            }

            return shapeCollection;
        }
    }
}
