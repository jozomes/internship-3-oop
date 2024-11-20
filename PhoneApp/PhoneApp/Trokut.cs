using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneApp
{
    internal class Trokut
    {
        public double lenA;
        public double lenB;
        public double lenC;
        public (double, double) t1;
        public (double, double) t2;
        public (double, double) t3;
        public Trokut(double strA, double strB, double strC, (double, double) dot1, (double, double) dot2, (double, double) dot3)
        {
            lenA = strA; lenB = strB; lenC = strC;
            t1 = dot1;
            t2 = dot2;
            t3 = dot3;
        }
    }
}
