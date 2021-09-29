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
    class RadGridText {
        public Dictionary<TextBlock, ShapeParams> GetTextCollection(double CenterX, double CenterY, double CanvasWidth, double CanvasHeight) {
            Dictionary<TextBlock, ShapeParams> textCollection = new Dictionary<TextBlock, ShapeParams>();

            Dictionary<int, string> columnTexts = new Dictionary<int, string>() {
                {90, "90°" },
                {180, "180°" },
                {270, "270°" },
                {360, "360°" },
            };

            foreach (int p in columnTexts.Keys) {
                TextBlock text = new TextBlock() {
                    Text = columnTexts[p],
                    FontSize = 14,
                    Foreground = Settings.coordLineBrush,
                    FontStyle = FontStyles.Italic,
                    Background = ColourPalette.BrushRGB(255, 255, 255),
                    TextAlignment = TextAlignment.Center,
                    Width = 36
                };
                ShapeParams textDef = new ShapeParams() {
                    ChangeLeft = true,
                    ChangeTop = true,
                    Top = 8,
                    Left = CenterX + p - 18,
                };
                textCollection.Add(text, textDef);
            }

            Dictionary<int, string> rowTexts = new Dictionary<int, string>() {
                {100, "1" },
                {-100, "-1" },
            };

            foreach (int p in rowTexts.Keys) {
                TextBlock text = new TextBlock() {
                    Text = rowTexts[p],
                    FontSize = 12,
                    Foreground = Settings.coordLineBrush,
                    FontStyle = FontStyles.Italic,
                    Background = ColourPalette.BrushRGB(255, 255, 255),
                    TextAlignment = TextAlignment.Center,
                    Width = 19,
                };
                ShapeParams textDef = new ShapeParams() {
                    ChangeLeft = true,
                    ChangeTop = true,
                    Top = CenterY - p - 8,
                    Left = 0,
                };
                textCollection.Add(text, textDef);
            }

            return textCollection;
        }
    }
}
