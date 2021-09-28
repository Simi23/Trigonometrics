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
    class CosWave : MathDefinition
    {
        public Dictionary<Shape, ShapeParams> ShapeCollection(double CenterX, double CenterY, double alpha, double deg, double CanvasWidth, double CanvasHeight)
        {
            Dictionary<Shape, ShapeParams> shapeCollection = new Dictionary<Shape, ShapeParams>();

            List<Point> points = new List<Point>();

            for (int i = 0; i <= 361; i++)
            {
                double rad = MainWindow.ConvertToRadians(i);
                double cos = Math.Cos(rad);
                points.Add(new Point(CenterX + i, CenterY - cos * 100));
            }

            PolyQuadraticBezierSegment pqbz = new PolyQuadraticBezierSegment()
            {
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

            Path path = new Path()
            {
                Data = pg,
                StrokeThickness = 3,
                Stroke = Settings.circleBrush
            };
            shapeCollection.Add(path, new ShapeParams());

            return shapeCollection;
        }
    }
}
