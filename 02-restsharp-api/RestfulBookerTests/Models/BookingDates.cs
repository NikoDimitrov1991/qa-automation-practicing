using System.Text.Json.Serialization;

namespace RestfulBookerTests.Models;

public class BookingDates
{
    [JsonPropertyName("checkin")] public required string Checkin { get; set; }

    [JsonPropertyName("checkout")] public required string Checkout { get; set; }
}