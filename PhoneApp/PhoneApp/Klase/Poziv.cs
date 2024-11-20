using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneApp.Klase
{
    public class Poziv
    {
        public Kontakt calledPerson;
        public DateTime timeOfCall;
        public DateTime timeOfCallEnd;


        public Poziv() 
        {
            timeOfCall = DateTime.Now;
        }
    }
}
