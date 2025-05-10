namespace API_Manajemen_Barang.DTOs
{
    public class StockMovementDto
    {
        public int StockMovementId { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public DateTime MovementDate { get; set; }
        public string MovementType { get; set; } // "in" or "out"
    }
}
