﻿namespace API_Manajemen_Barang.src.Core.Entities
{
    public class Item
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<StockMovement> StockMovements { get; set; }
    }
}
