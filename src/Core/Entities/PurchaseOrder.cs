namespace API_Manajemen_Barang.src.Core.Entities
{
    public class PurchaseOrder
    {
        public int PurchaseOrderId { get; set; }
        public string PoNumber { get; set; }
        public int SupplierId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public virtual ICollection<PurchaseOrderItem> Items { get; set; }

    }
}
