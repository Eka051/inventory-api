namespace Inventory_api.src.Core.Entities
{
    public class PurchaseOrder
    {
        public int PurchaseOrderId { get; set; }
        public string PoNumber { get; set; } = null!;
        public int SupplierId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } = null!;
        public virtual ICollection<PurchaseOrderItem> Items { get; set; } = null!;

    }
}
