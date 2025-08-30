using Inventory_api.src.Core.Entities;

namespace Inventory_api.src.Core.Entities
{
    public class PurchaseOrderItem
    {
        public int PurchaseOrderItemId { get; set; }
        public int PurchaseOrderId { get; set; }
        public virtual required PurchaseOrder PurchaseOrder { get; set; }
        public int ItemId { get; set; }
        public virtual required Item Item { get; set; }
        public int Quantity { get; set; }
        public int PurchasePrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
