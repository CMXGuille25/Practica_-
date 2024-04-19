using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PRACTICA__.Models
{
    public class Warehouses
    {

        [Key]
        [JsonPropertyName("warehouse_id")]
        public int? WAREHOUSE_ID { get; set; }

        [JsonPropertyName("warehouse_name")]
        public string? WAREHOUSE_NAME { get; set; }

        [ForeignKey("Locations")]
        [JsonPropertyName("location_id")]
        public int? LOCATION_ID { get; set; }

        public Locations? Locations { get; set; }
    }
}
