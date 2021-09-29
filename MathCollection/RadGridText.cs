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
        // ½ π
        public Dictionary<TextBlock, ShapeParams> GetTextCollection(double CenterX, double CenterY, double CanvasWidth, double CanvasHeight) {
            Dictionary<TextBlock, ShapeParams> textCollection = new Dictionary<TextBlock, ShapeParams>();

            Dictionary<int, string> columnTexts = new Dictionary<int, string>() {
                {90, "½π" },
                {180, "1π" },
                {270, "1½π" },
                {370, "2π" },
            };

            foreach (int p in columnTexts.Keys) {
                TextBlock text = new TextBlock() {
                    Text = columnTexts[p],
                    FontSize = 14,
                    Foreground = Settings.coordLineBrush,
                    FontStyle = FontStyles.Italic,
                };
                ShapeParams textDef = new ShapeParams() {
                    ChangeLeft = true,
                    ChangeTop = true,
                    Top = 10,
                    Left = CenterX + p - 36
                };
                textCollection.Add(text, textDef);
            }

            return textCollection;
        }
    }
}
