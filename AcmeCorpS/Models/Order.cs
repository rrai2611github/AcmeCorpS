namespace AcmeCorpS.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        // Add other properties as needed

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
