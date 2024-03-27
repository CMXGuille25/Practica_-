using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PRACTICA__.Models
{
    public class Customers
    {
        [Key]
        [JsonPropertyName("customer_id")]
        public int CUSTOMER_ID { get; set; }
        [JsonPropertyName("name")]
        public string? NAME { get; set; }
        [JsonPropertyName("address")]
        public string? ADDRESS { get; set; }
        [JsonPropertyName("website")]
        public string? WEBSITE { get; set; }
        [JsonPropertyName("credit_limit")]
        public decimal CREDIT_LIMIT { get; set; }
        
    }
}
