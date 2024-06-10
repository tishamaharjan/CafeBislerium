namespace CafeBislerium.Data
{
    public class Items
    {
        public required string ItemName { get; set; }

        public float Price { get; set; }

        public ItemCategory Category { get; set; }

        public int Quantity { get; set; }
    }
}
