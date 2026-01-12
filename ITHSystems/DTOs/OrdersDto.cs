using System.Text.Json.Serialization;

namespace ITHSystems.DTOs;

public class OrdersDto
{
    [JsonPropertyName("isProduct")]
    public bool IsProduct { get; set; }

    [JsonPropertyName("batchProductId")]
    public int BatchProductId { get; set; }

    [JsonPropertyName("code")]
    public string Code { get; set; } = string.Empty;

    [JsonPropertyName("serial")]
    public string Serial { get; set; } = string.Empty;

    [JsonPropertyName("category")]
    public string Category { get; set; } = string.Empty;

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("unitMeasure")]
    public string UnitMeasure { get; set; } = string.Empty;

    [JsonPropertyName("amountOrigin")]
    public string AmountOrigin { get; set; } = string.Empty;

    [JsonPropertyName("amountDestination")]
    public string AmountDestination { get; set; } = string.Empty;

    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    [JsonPropertyName("statusCode")]
    public string StatusCode { get; set; } = string.Empty;

    [JsonPropertyName("deliveryToAddress")]
    public string DeliveryToAddress { get; set; } = string.Empty;

    [JsonPropertyName("commissionAmount")]
    public decimal CommissionAmount { get; set; }

    [JsonPropertyName("commissionPercent")]
    public decimal CommissionPercent { get; set; }

    [JsonPropertyName("netAmount")]
    public decimal NetAmount { get; set; }

    [JsonPropertyName("retentionAmount")]
    public decimal RetentionAmount { get; set; }

    [JsonPropertyName("shippingCode")]
    public string ShippingCode { get; set; } = string.Empty;

    [JsonPropertyName("customer")]
    public CustomerDto Customer { get; set; } = new();

    [JsonPropertyName("destinationCustomer")]
    public CustomerDto DestinationCustomer { get; set; } = new();

    [JsonPropertyName("agencyDocument")]
    public AgencyDocumentDto AgencyDocument { get; set; } = new();




    public int ProductBatchAssignmentId { get; set; }
    public string JWT { get; internal set; }
    public object Comment { get; internal set; }
    public int IsSecondPerson { get; internal set; }
    public object SecondPersonRelationshipId { get; internal set; }
    public object IdentificationDocumentPhoto { get; internal set; }
    public object SignatureImage { get; internal set; }
    public object IdentificationDocumentTypeId { get; internal set; }
    public object IdentificationValue { get; internal set; }
    public object Longitude { get; internal set; }
    public object Latitude { get; internal set; }
    public object EventOcurredOn { get; internal set; }
    public bool IsSelected { get; internal set; }
    public object CauseSelected { get; internal set; }
    public object WorkingForOfficeId { get; internal set; }
    public object GenderId { get; internal set; }
    //[JsonPropertyName("properties")]
    //public List<PropertyDto> Properties { get; set; } = new();



}
