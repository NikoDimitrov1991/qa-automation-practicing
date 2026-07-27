using System.Text.Json.Serialization;

namespace RestfulBookerTests.Models;

public class AuthResponse
{
    [JsonPropertyName("token")] public required string Token { get; set; }
}