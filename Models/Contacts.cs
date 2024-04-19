using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PRACTICA__.Models
{
    public class Contacts
    {
        [Key]
        [JsonPropertyName("contact_id")]
        public int? CONTACT_ID { get; set; }

        [JsonPropertyName("first_name")]
        public string? FIRST_NAME { get; set; }

        [JsonPropertyName("last_name")]
        public string? LAST_NAME { get; set; }

        [JsonPropertyName("email")]
        public string? EMAIL { get; set; }

        [JsonPropertyName("phone")]
        public string? PHONE { get; set; }

        [ForeignKey("Customers")]
        [JsonPropertyName("customer_id")]
        public int? CUSTOMER_ID { get; set; }

        public Customers? Customers { get; set; }
    }
}
