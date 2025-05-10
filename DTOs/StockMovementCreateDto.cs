namespace API_Manajemen_Barang.DTOs
{
    public class StockMovementCreateDto
    {
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public string MovementType { get; set; } // "in" or "out"
        public string Note { get; set; }
    }
}
