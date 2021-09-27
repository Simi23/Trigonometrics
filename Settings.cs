using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Trigonometrics
{
    class Settings
    {
        public static Brush baseColorBrush = ColourPalette.BrushRGB(138, 138, 138);
        public static Brush circleBrush = ColourPalette.BrushRGB(43, 43, 43);
        public static Brush coordLineBrush = ColourPalette.BrushRGB(128, 128, 128);

        public static Brush cosBrush = ColourPalette.BrushRGB(34, 177, 76);
        public static Brush cotBrush = ColourPalette.BrushRGB(255, 69, 0);
        public static Brush sinBrush = ColourPalette.BrushRGB(237, 28, 36);
        public static Brush tanBrush = ColourPalette.BrushRGB(0, 162, 232);

        public static Brush activatedToggleBrush = ColourPalette.BrushRGB(150, 183, 255);
        public static Brush deactivatedToggleBrush = ColourPalette.BrushRGB(244, 244, 245);
    }
}
