using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneApp.Klase
{
    public static class ContactsList
    {
        public static List<Kontakt> AllContacts { get; } = new();

        public static void AddContact(Kontakt newContact) 
        {
            AllContacts.Add(newContact);
        }
    }
}
