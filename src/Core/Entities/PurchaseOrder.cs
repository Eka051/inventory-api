namespace Inventory_api.src.Core.Entities
{
    public class PurchaseOrder
    {
        public int PurchaseOrderId { get; set; }
        public required string PoNumber { get; set; }
        public int SupplierId { get; set; }
        public DateTime OrderDate { get; set; }
        public required string Status { get; set; }
        public virtual required ICollection<PurchaseOrderItem> Items { get; set; }

    }
}
