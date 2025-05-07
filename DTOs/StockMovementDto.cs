namespace API_Manajemen_Barang.DTOs
{
    public class StockMovementDto
    {
        public int StockMovementId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public DateTime MovementDate { get; set; }
        public string MovementType { get; set; } // "in" or "out"
        public string Description { get; set; }
        public StockMovementDto(int stockMovementId, int productId, string productName, int quantity, DateTime movementDate, string movementType, string description)
        {
            StockMovementId = stockMovementId;
            ProductId = productId;
            ProductName = productName;
            Quantity = quantity;
            MovementDate = movementDate;
            MovementType = movementType;
            Description = description;
        }
    }
}
