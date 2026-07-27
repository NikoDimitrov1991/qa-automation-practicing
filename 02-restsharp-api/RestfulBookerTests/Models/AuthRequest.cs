using System.Text.Json.Serialization;

namespace RestfulBookerTests.Models;

public class AuthRequest
{
    [JsonPropertyName("username")] public required string Username { get; set; }

    [JsonPropertyName("password")] public required string Password { get; set; }
}