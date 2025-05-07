namespace API_Manajemen_Barang.Models
{
    public class StockMovement
    {
        public int StockMovementId { get; set; }
        public int ItemId { get; set; }
        public string Type { get; set; } // "in" or "out"
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
        
        public Item Item { get; set; }
    }
}
