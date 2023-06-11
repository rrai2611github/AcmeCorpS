using System.Collections.Generic;

namespace AcmeCorpS.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        // Add other properties as needed

        public List<Contact> Contacts { get; set; }
        public List<Order> Orders { get; set; }
    }
}
