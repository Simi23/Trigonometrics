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
    class TextCollection
    {
        public Dictionary<TextBlock, ShapeParams> GetTextCollection(double CenterX, double CenterY, double CanvasWidth, double CanvasHeight)
        {
            Dictionary<TextBlock, ShapeParams> textCollection = new Dictionary<TextBlock, ShapeParams>();

            TextBlock xCoord = new TextBlock()
            {
                Text = "x",
                FontSize = 14,
                Foreground = Settings.coordLineBrush,
                FontStyle = FontStyles.Italic,
                FontWeight = FontWeights.Bold,
            };
            ShapeParams xCoordDef = new ShapeParams() {
                ChangeLeft = true,
                ChangeTop = true,
                Top = CenterY - 26,
                Left = CanvasWidth - 20
            };
            textCollection.Add(xCoord, xCoordDef);

            TextBlock yCoord = new TextBlock()
            {
                Text = "y",
                FontSize = 14,
                Foreground = Settings.coordLineBrush,
                FontStyle = FontStyles.Italic,
                FontWeight = FontWeights.Bold,
            };
            ShapeParams yCoordDef = new ShapeParams()
            {
                ChangeLeft = true,
                ChangeTop = true,
                Top = 4,
                Left = CenterX + 10
            };
            textCollection.Add(yCoord, yCoordDef);

            TextBlock zeroPoint = new TextBlock()
            {
                Text = "0",
                FontSize = 14,
                Foreground = Settings.coordLineBrush,
                FontStyle = FontStyles.Italic,
                FontWeight = FontWeights.Bold,
            };
            ShapeParams zeroPointDef = new ShapeParams()
            {
                ChangeLeft = true,
                ChangeTop = true,
                Top = CenterY + 2,
                Left = CenterX - 12
            };
            textCollection.Add(zeroPoint, zeroPointDef);

            return textCollection;
        }
    }
}
