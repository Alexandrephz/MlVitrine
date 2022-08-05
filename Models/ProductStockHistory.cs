namespace MlVitrine.Models
{
    public class ProductStockHistory
    {
        public int ProductStockHistoryId { get; set; }
        public int product_stock { get; set; }

        public int productid_stock { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}