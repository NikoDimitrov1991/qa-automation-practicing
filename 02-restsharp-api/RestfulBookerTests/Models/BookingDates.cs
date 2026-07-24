using System.Text.Json.Serialization;

namespace RestfulBookerTests.Models;

public class BookingDates
{
    [JsonPropertyName("checkin")] public string Checkin { get; set; }

    [JsonPropertyName("checkout")] public string Checkout { get; set; }
}