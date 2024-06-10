namespace CafeBislerium.Data
{
    public class Order
    {
        public Guid OrderID { get; set; } = Guid.NewGuid();

        public List<Items> Items { get; set; }

        public long CustID { get; set; }

        public DateTime OrderDate {  get; set; }

        public float TotalPrice {  get; set; }
    }
}
