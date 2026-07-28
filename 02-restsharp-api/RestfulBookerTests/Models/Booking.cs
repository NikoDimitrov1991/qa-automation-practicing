using System.Text.Json.Serialization;

namespace RestfulBookerTests.Models;

public class Booking
{
    [JsonPropertyName("firstname")] public required string Firstname { get; set; }

    [JsonPropertyName("lastname")] public required string Lastname { get; set; }

    [JsonPropertyName("totalprice")] public required int Totalprice { get; set; }           

    [JsonPropertyName("depositpaid")] public required bool Depositpaid { get; set; }

    [JsonPropertyName("bookingdates")] public required BookingDates Bookingdates { get; set; }

    [JsonPropertyName("additionalneeds")] public string? Additionalneeds { get; set; }
}