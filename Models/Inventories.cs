using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PRACTICA__.Models
{
    public class Inventories
    {

        [Key]
        [JsonPropertyName("quantity")]
        public int? QUANTITY { get; set; }

        [ForeignKey("Product")]
        [JsonPropertyName("product_id")]
        public int? PRODUCT_ID { get; set; }

        public Products? Product { get; set; }

        [ForeignKey("Warehouses")]
        [JsonPropertyName("warehouse_id")]
        public int? WAREHOUSE_ID { get; set; }


        public Warehouses? Warehouses { get; set; }
    }
}
