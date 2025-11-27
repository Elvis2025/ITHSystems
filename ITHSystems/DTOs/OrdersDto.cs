using System.Text.Json.Serialization;

namespace ITHSystems.DTOs;

public class OrdersDto
{
    [JsonPropertyName("isProduct")]
    public bool IsProduct { get; set; }

    [JsonPropertyName("batchProductId")]
    public int BatchProductId { get; set; }

    [JsonPropertyName("code")]
    public string Code { get; set; }

    [JsonPropertyName("serial")]
    public string Serial { get; set; }

    [JsonPropertyName("category")]
    public string Category { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("unitMeasure")]
    public string UnitMeasure { get; set; }

    [JsonPropertyName("amountOrigin")]
    public string AmountOrigin { get; set; }

    [JsonPropertyName("amountDestination")]
    public string AmountDestination { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("statusCode")]
    public string StatusCode { get; set; }

    [JsonPropertyName("deliveryToAddress")]
    public string DeliveryToAddress { get; set; }

    [JsonPropertyName("commissionAmount")]
    public decimal CommissionAmount { get; set; }

    [JsonPropertyName("commissionPercent")]
    public decimal CommissionPercent { get; set; }

    [JsonPropertyName("netAmount")]
    public decimal NetAmount { get; set; }

    [JsonPropertyName("retentionAmount")]
    public decimal RetentionAmount { get; set; }

    [JsonPropertyName("shippingCode")]
    public string ShippingCode { get; set; }

    [JsonPropertyName("customer")]
    public CustomerDto Customer { get; set; }

    [JsonPropertyName("destinationCustomer")]
    public CustomerDto DestinationCustomer { get; set; }

    [JsonPropertyName("agencyDocument")]
    public AgencyDocumentDto AgencyDocument { get; set; }

    //[JsonPropertyName("properties")]
    //public List<PropertyDto> Properties { get; set; } = new();
}
