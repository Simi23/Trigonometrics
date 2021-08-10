using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Trigonometrics
{
    class MathDefinition
    {
        public string Name { get; set; }
        public bool Enabled { get; set; }
        public bool CanBeShown { get; set; }
        public Func<double, double> Result { get; set; }
        public Func<double, double, double> MyProperty { get; set; }
        public Brush Brush { get; set; }
    }
}
