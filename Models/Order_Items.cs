using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PRACTICA__.Models
{
    public class Order_Items
    {
        [Key]
        [JsonPropertyName("order_id")]
        public int? ORDER_ID { get; set; }

        [JsonPropertyName("item_id")]
        public int? ITEM_ID { get; set; }

        [ForeignKey("Product")]
        [JsonPropertyName("product_id")]
        public int? PRODUCT_ID { get; set; }
        public Products? Product { get; set; }

        [JsonPropertyName("quantity")]
        public int? QUANTITY { get; set; }

        [JsonPropertyName("unit_price")]
        public decimal? UNIT_PRICE { get; set; }
    }
}
