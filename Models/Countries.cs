using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PRACTICA__.Models
{
    public class Countries
    {
        [Key]
        [JsonPropertyName("country_id")]
        public string? COUNTRY_ID { get; set; }

        [JsonPropertyName("country_name")]
        public string? COUNTRY_NAME { get; set; }

        [ForeignKey("Regions")]
        [JsonPropertyName("region_id")]
        public int REGION_ID { get; set; }

        public Regions? Regions { get; set; }
    }
}
