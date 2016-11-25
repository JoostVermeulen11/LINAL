using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Linal_wk1
{
    public class RandomColor
    {
        private static Random random = new Random();

        public static Brush GetRandomBrush()
        {
            return new SolidColorBrush(Color.FromRgb((byte)random.Next(0, 255), (byte)random.Next(0, 255), (byte)random.Next(0, 255)));
        }
    }
}
