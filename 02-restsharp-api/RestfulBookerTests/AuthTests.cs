using System.Net;
using RestSharp;
using RestfulBookerTests.Models;

namespace RestfulBookerTests;

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
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(response.Data, Is.Not.Null);
        Assert.That(response.Data.Token, Is.Not.Null.And.Not.Empty);
    }
}