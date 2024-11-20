using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneApp
{
    internal class Pravokutnik
    {
        public double lenA;
        public double lenB;
        public (double,double) t1;
        public (double,double) t2;
        public (double,double) t3;
        public (double,double) t4;
        public Pravokutnik(double strA, double strB, (double,double) dot1, (double, double) dot2, (double, double) dot3, (double, double) t4)
        {
            this.lenA = strA; this.lenB = strB;
            this.t1 = dot1;
            this.t2 = dot2;
            this.t3 = dot3;
            this.t4 = t4;
        }
        public string Opseg()
        {
            double strA = Math.Abs(t3.Item1 - t2.Item1);
            double strB = Math.Abs(t2.Item1 - t1.Item1);

            
            return "Opseg ovog pravokutnika je: " + (strA*2+strB*2);
        }
        /*public string Povrsina() 
        {
            double result = 0;
        }*/
    }
    
}
