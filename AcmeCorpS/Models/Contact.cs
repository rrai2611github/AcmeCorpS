namespace AcmeCorpS.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string Email { get; set; }
        // Add other properties as needed

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
