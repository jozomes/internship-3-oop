using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneApp
{
    public class Kontakt
    {
        public string firstName;
        public string number;
        public DateTime dateCreated;
        public Guid id;
        //public string status;

        public Kontakt(string name, string num) 
        {
            firstName = name;
            number = num;
            id = Guid.NewGuid();
            dateCreated = DateTime.Now;
        }
        public Kontakt(string num)
        {
            firstName = "nepoznato";
            number = num;
        }
        /*public void BlockContact() 
        {
            this.status = "blokiran";
        }*/
    }
}
