namespace MlVitrine.Models
{
    public class ProductPriceHistory
    {
        public int ProductPriceHistoryId { get; set; }
        public decimal product_price { get; set; }

        public int productid_price { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        
    }
}