namespace API_Manajemen_Barang.DTOs
{
    public class StockMovementResponseDto
    {
        public int StockMovementId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public string MovementType { get; set; } // "in" or "out"
        public string Note { get; set; }
        public DateTime CreatedAt { get; set; }
        public ItemDto Item { get; set; } // Include item details
    }
}
