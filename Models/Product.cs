namespace MlVitrine.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string? product_name { get; set; }
        public string? product_model { get; set; }
        public string? product_description { get; set; }
        public string? product_sku { get; set; }
        public string? product_mainimage { get; set; }
        public decimal product_price { get; set; }
        public int? product_stock { get; set; }
        public int product_ean { get; set; }
        public bool product_active { get; set; }
        public int ProductSpecId { get; set; }
        public virtual ProductSpec? ProductSpec { get; set; }
        public int ProductConditionId { get; set; }
        public virtual ProductCondition? ProductCondition { get; set; }
        public int BrandId { get; set; }
        public virtual Brand? Brand { get; set; }

        public List<ProductImage>? ProductImages { get; set; }
        public List<ProductPriceHistory>? ProductPrices { get; set; }
        public List<ProductCategory>? ProductCategories { get; set; }
        public List<ProductStockHistory>? ProductStocks { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
