namespace MlVitrine.Models
{
    public class ProductImage
    {
        public int ProductImageId { get; set; }
        public string? image_url { get; set; }
        public int ProductId { get; set; }
        public virtual Product? Product {get; set;}
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}