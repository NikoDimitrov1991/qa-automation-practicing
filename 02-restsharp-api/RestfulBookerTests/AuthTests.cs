using System.Net;
using RestSharp;
using RestfulBookerTests.Models;
using FluentAssertions;

namespace RestfulBookerTests;

[NonParallelizable]
[TestFixture]
public class AuthTests
{
    private RestClient _client;

    [SetUp]
    public void Setup()
    {
        _client = new RestClient("https://restful-booker.herokuapp.com");
        _client.AddDefaultHeader("Accept", "application/json");
        _client.AddDefaultHeader("User-Agent", "RestSharp-Test");
    }

    [TearDown]
    public void Cleanup()
    {
        _client.Dispose();
    }

    [Test]
    public async Task PostAuth_WithValidCredentials_ReturnsToken()
    {
        // Arrange
        var authRequest = new AuthRequest { Username = "admin", Password = "password123" };
        var request = new RestRequest("/auth", Method.Post);

        // Act
        request.AddJsonBody(authRequest);
        var response = await _client.ExecuteAsync<AuthResponse>(request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        response.Data.Token.Should().NotBeNullOrEmpty();
    }
}