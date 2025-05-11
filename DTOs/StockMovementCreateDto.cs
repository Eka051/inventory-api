using System.ComponentModel.DataAnnotations;

namespace API_Manajemen_Barang.DTOs
{
    public class StockMovementCreateDto
    {
        [Required]
        public int ItemId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public string MovementType { get; set; } // "in" or "out"
        public string Note { get; set; }
    }
}
