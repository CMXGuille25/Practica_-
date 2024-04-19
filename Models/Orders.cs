using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PRACTICA__.Models
{
    public class Orders
    {
        [Key]
        [JsonPropertyName("order_id")]
        public int? ORDER_ID { get; set; }

        [JsonPropertyName("status")]
        public string? STATUS { get; set; }

        [JsonPropertyName("order_date")]
        public DateOnly? ORDER_DATE { get; set; }

        [ForeignKey("Customers")]
        [JsonPropertyName("customer_id")]
        public int CUSTOMER_ID { get; set; }

        [ForeignKey("Employees")]
        [JsonPropertyName("salesman_id")]
        public int SALESMAN_ID { get; set; }

        public Customers? Customers { get; set; }
        public Employees? Employees { get; set; }

    }
}
