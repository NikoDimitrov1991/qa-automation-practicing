using System.Net;
using RestSharp;
using FluentAssertions;

namespace RestfulBookerTests;

[NonParallelizable]
[TestFixture]
public class PingTests
{
    private RestClient _client;

    [SetUp]
    public void Setup()
    {
        _client = new RestClient("https://restful-booker.herokuapp.com");
    }

    [TearDown]
    public void Cleanup()
    {
        _client.Dispose();
    }

    [Test]
    public async Task Ping_ReturnsCreated()
    {
        // Arrange
        var request = new RestRequest("/ping", Method.Get);

        // Act
        var response = await _client.ExecuteAsync(request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}