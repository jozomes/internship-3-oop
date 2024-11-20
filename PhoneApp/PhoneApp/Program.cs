using PhoneApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DrugoPredavanje
{
    class Program
    {
        static void Main(string[] args)
        {
            Kontakt mama;
            //mama.firstName = "ivana";
            Kontakt padre = new Kontakt("Mate", "063420123");
            Console.WriteLine(padre);
            Pravokutnik pk = new Pravokutnik(3,7,(0,0),(2,0),(0,2), (2,2));
            Console.WriteLine(pk.Opseg());
            Console.ReadKey();
        }
    }
}