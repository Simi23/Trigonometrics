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

namespace Trigonometrics
{
    class ShapeParams
    {
        public bool ChangeLeft { get; set; }
        public double Left { get; set; }
        public bool ChangeTop { get; set; }
        public double Top { get; set; }
        public int IndexZ { get; set; }
        public MouseEventHandler MouseEnter { get; set; }
        public MouseEventHandler MouseLeave { get; set; }
    }
}
