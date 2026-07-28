using System.Text.Json.Serialization;

namespace RestfulBookerTests.Models;

public class CreateBookingResponse
{
    [JsonPropertyName("bookingid")] public required int Bookingid { get; set; }
    [JsonPropertyName("booking")] public required Booking Booking { get; set; }
}