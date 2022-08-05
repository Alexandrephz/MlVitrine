using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MlVitrine.Migrations
{
    public partial class PopulateTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData("Brand", "brand_name", "Sony");
            migrationBuilder.InsertData("Brand", "brand_name", "Apple");
            migrationBuilder.InsertData("Brand", "brand_name", "Alexa");
            migrationBuilder.InsertData("Brand", "brand_name", "Google");
            migrationBuilder.InsertData("Brand", "brand_name", "Sony");
            migrationBuilder.InsertData("Brand", "brand_name", "Xiaomi");
            migrationBuilder.InsertData("Brand", "brand_name", "Logitech");
            migrationBuilder.InsertData("Brand", "brand_name", "LG");
            migrationBuilder.InsertData("ProductCategory", "category_name", "Celular/Smartphone");
            migrationBuilder.InsertData("ProductCategory", "category_name", "TV/Audio");
            migrationBuilder.InsertData("ProductCategory", "category_name", "Games/PC");
            migrationBuilder.InsertData("ProductSpec", "product_attribute", "Preto 220V");
            migrationBuilder.InsertData("ProductSpec", "product_attribute", "Preto");
            migrationBuilder.InsertData("ProductSpec", "product_attribute", "Preto bivolt");
            migrationBuilder.InsertData("ProductSpec", "product_attribute", "vermelho");
            migrationBuilder.InsertData("ProductSpec", "product_attribute", "branco bivolt");
            migrationBuilder.InsertData("ProductCondition", "product_condition", "Novo");

            migrationBuilder.InsertData("Product", columns: new[] { "product_name", "product_model", "product_description", "product_sku", "product_ean",
            "BrandId", "ProductSpecId", "ProductConditionId", "product_active","product_price","product_stock","CreatedDate", "UpdatedDate" }, values: new object[] { "Smartphone", "Xiaomi Mi 12",
             "O mais recente lançamento da Xiaomi blablabla","SMXIA12PR",213131544,6, 2, 1, true,299.90, 50, DateTime.UtcNow, DateTime.UtcNow });

            migrationBuilder.InsertData("ProductImage", columns: new[] { "ProductId", "image_url", "CreatedDate", "UpdatedDate" },
            values: new object[] { 1, "https://img.joomcdn.net/7d6d4257100f23e4a9fb60161f78a30dddf25c7f_original.jpeg", DateTime.UtcNow, DateTime.UtcNow });

            migrationBuilder.InsertData("Product", columns: new[] { "product_name", "product_model", "product_description", "product_sku", "product_ean",
            "BrandId", "ProductSpecId", "ProductConditionId", "product_active","product_price","product_stock","CreatedDate", "UpdatedDate" }, values: new object[] { "Smartphone", "Xiaomi Mi 11 Pro",
             "O mais recente lançamento da Xiaomi blablabla","SMXIA11PROPR",123131231,6, 2, 1, true, 299.90, 50, DateTime.UtcNow, DateTime.UtcNow });

            migrationBuilder.InsertData("ProductImage", columns: new[] { "ProductId", "image_url", "CreatedDate", "UpdatedDate" },
            values: new object[] { 1, "https://img.joomcdn.net/7d6d4257100f23e4a9fb60161f78a30dddf25c7f_original.jpeg", DateTime.UtcNow, DateTime.UtcNow });

            migrationBuilder.InsertData("ProductImage", columns: new[] { "ProductId", "image_url", "CreatedDate", "UpdatedDate" },
            values: new object[] { 1, "https://img.joomcdn.net/7d6d4257100f23e4a9fb60161f78a30dddf25c7f_original.jpeg", DateTime.UtcNow, DateTime.UtcNow });

            migrationBuilder.InsertData("ProductImage", columns: new[] { "ProductId", "image_url", "CreatedDate", "UpdatedDate" },
            values: new object[] { 1, "https://img.joomcdn.net/7d6d4257100f23e4a9fb60161f78a30dddf25c7f_original.jpeg", DateTime.UtcNow, DateTime.UtcNow });

            migrationBuilder.InsertData("Product", columns: new[] { "product_name", "product_model", "product_description", "product_sku", "product_ean",
            "BrandId", "ProductSpecId", "ProductConditionId", "product_active","product_price","product_stock","CreatedDate", "UpdatedDate" }, values: new object[] { "Smartphone", "Xiaomi Mi 10 Pro",
             "O mais recente lançamento da Xiaomi blablabla","SMXIA10PROPR",123131231,6, 2, 1, false,299.90, 50, DateTime.UtcNow, DateTime.UtcNow });

            migrationBuilder.InsertData("ProductImage", columns: new[] { "ProductId", "image_url", "CreatedDate", "UpdatedDate" },
            values: new object[] { 2, "https://img.joomcdn.net/7d6d4257100f23e4a9fb60161f78a30dddf25c7f_original.jpeg", DateTime.UtcNow, DateTime.UtcNow });


            migrationBuilder.InsertData("Product", columns: new[] { "product_name", "product_model", "product_description", "product_sku", "product_ean",
            "BrandId", "ProductSpecId", "ProductConditionId", "product_active","product_price","product_stock","CreatedDate", "UpdatedDate" }, values: new object[] { "Teclado", "Gamer mecanico RGB",
             "O teclado mais avançado para games","TLGMRGBP",123313113,7, 2, 1, true,299.90, 50, DateTime.UtcNow, DateTime.UtcNow });

            migrationBuilder.InsertData("ProductImage", columns: new[] { "ProductId", "image_url", "CreatedDate", "UpdatedDate" },
            values: new object[] { 4, "https://m.media-amazon.com/images/I/71Nqzh-u2rL._AC_SX679_.jpg", DateTime.UtcNow, DateTime.UtcNow });

            migrationBuilder.InsertData("Product", columns: new[] { "product_name", "product_model", "product_description", "product_sku", "product_ean",
            "BrandId", "ProductSpecId", "ProductConditionId", "product_active","product_price","product_stock","CreatedDate", "UpdatedDate" }, values: new object[] { "Televisão", "OLED 65' 120HZ",
             "Televisao com melhor imagem do mercado","TLGMRGBP",123313113,8, 3, 1, true,299.90, 50, DateTime.UtcNow, DateTime.UtcNow });

            migrationBuilder.InsertData("ProductImage", columns: new[] { "ProductId", "image_url", "CreatedDate", "UpdatedDate" },
            values: new object[] { 5, "https://i.zst.com.br/thumbs/12/18/2e/1751031611.jpg", DateTime.UtcNow, DateTime.UtcNow });

            migrationBuilder.InsertData("ProductCompatibility", columns: new[] { "BrandId", "ProductId" },
            values: new object[] { 5, 3 });
            migrationBuilder.InsertData("ProductCompatibility", columns: new[] { "BrandId", "ProductId" },
            values: new object[] { 5, 3 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
