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
    class Basics : MathDefinition {
        public Dictionary<Shape, ShapeParams> ShapeCollection(double CenterX, double CenterY, double alpha, double deg, double CanvasWidth, double CanvasHeight) {
            Dictionary<Shape, ShapeParams> shapeCollection = new Dictionary<Shape, ShapeParams>();

            // Coord line x
            Line coordLineXShape = new Line() {
                X1 = 0,
                Y1 = CenterY,

                X2 = CanvasWidth,
                Y2 = CenterY,
                Stroke = Settings.coordLineBrush,
                StrokeThickness = 1,
            };
            ShapeParams coordLineXDef = new ShapeParams() {
                IndexZ = -1
            };
            shapeCollection.Add(coordLineXShape, coordLineXDef);

            // Coord line y
            Line coordLineYShape = new Line() {
                X1 = CenterX,
                Y1 = 0,

                X2 = CenterX,
                Y2 = CanvasHeight,
                Stroke = Settings.coordLineBrush,
                StrokeThickness = 1,
            };
            ShapeParams coordLineYDef = new ShapeParams() {
                IndexZ = -1
            };
            shapeCollection.Add(coordLineYShape, coordLineYDef);

            PointCollection xTriangle = new PointCollection() {
                new Point(0, .5),
                new Point(1, 0),
                new Point(0, -0.5)
            };
            Polygon xTriangleShape = new Polygon();
            xTriangleShape.Points = xTriangle;
            xTriangleShape.Fill = Settings.coordLineBrush;
            xTriangleShape.Stretch = Stretch.Fill;
            xTriangleShape.Width = 10;
            xTriangleShape.Height = 10;
            ShapeParams xTriangleDef = new ShapeParams() {
                ChangeLeft = true,
                ChangeTop = true,
                Left = CanvasWidth - xTriangleShape.Width - 2,
                Top = CenterY - xTriangleShape.Height / 2
            };
            shapeCollection.Add(xTriangleShape, xTriangleDef);


            PointCollection yTriangle = new PointCollection() {
                new Point(-0.5, 1),
                new Point(0, -1),
                new Point(.5, 1)
            };
            Polygon yTriangleShape = new Polygon();
            yTriangleShape.Points = yTriangle;
            yTriangleShape.Fill = Settings.coordLineBrush;
            yTriangleShape.Stretch = Stretch.Fill;
            yTriangleShape.Width = 10;
            yTriangleShape.Height = 10;
            ShapeParams yTriangleDef = new ShapeParams() {
                ChangeLeft = true,
                ChangeTop = true,
                Left = CenterX - yTriangleShape.Width / 2,
                Top = 0
            };
            shapeCollection.Add(yTriangleShape, yTriangleDef);

            return shapeCollection;
        }
    }
}
