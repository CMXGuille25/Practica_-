using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PRACTICA__.Models
{
    public class Products
    {
        [Key]
        [JsonPropertyName("product_id")]
        public int? PRODUCT_ID { get; set; }

        [JsonPropertyName("product_name")]
        public string? PRODUCT_NAME { get; set; }

        [JsonPropertyName("description")]
        public string? DESCRIPTION { get; set; }

        [JsonPropertyName("standard_cost")]
        public decimal? STANDARD_COST { get; set; }

        [JsonPropertyName("list_price")]
        public decimal? LIST_PRICE { get; set; }

        [ForeignKey("Product_categories")]
        [JsonPropertyName("category_id")]
        public int CATEGORY_ID { get; set; }

        public Product_categories? Product_categories { get; set; }
    }
}
