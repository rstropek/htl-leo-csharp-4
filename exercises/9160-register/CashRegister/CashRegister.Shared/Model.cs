using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CashRegister.Shared
{
    public class Product
    {
        [JsonPropertyName("id")]
        public int ID { get; set; }

        [JsonPropertyName("productName")]
        public string ProductName { get; set; } = string.Empty;

        [JsonPropertyName("unitPrice")]
        public decimal UnitPrice { get; set; }
    }

    public class ReceiptLine
    {
        [JsonPropertyName("id")]
        public int ID { get; set; }

        [JsonPropertyName("product")]
        public Product? Product { get; set; }

        [JsonPropertyName("amount")]
        public int Amount { get; set; }

        [JsonPropertyName("totalPrice")]
        public decimal TotalPrice { get; set; }
    }

    public class Receipt
    {
        [JsonPropertyName("id")]
        public int ID { get; set; }

        [JsonPropertyName("receiptTimestamp")]
        public DateTime ReceiptTimestamp { get; set; }

        [JsonPropertyName("receiptLines")]
        public List<ReceiptLine> ReceiptLines { get; set; } = new();

        [JsonPropertyName("totalPrice")]
        public decimal TotalPrice { get; set; }
    }

    public class ReceiptLineDto
    {
        public int ProductID { get; set; }

        public int Amount { get; set; }
    }
}
