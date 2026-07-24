using System.Net;
using RestSharp;
using RestfulBookerTests.Models;

namespace RestfulBookerTests;

[TestFixture]
public class BookingTests
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
    public async Task GetBooking_WithValidId_ReturnsBookingData()
    {
        // Arrange
        var request = new RestRequest("/booking/3", Method.Get);

        // Act
        var response = await _client.ExecuteAsync<Booking>(request);

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(response.Data, Is.Not.Null);
        Assert.That(response.Data.Firstname, Is.Not.Null.And.Not.Empty);
    }

    [Test]
    public async Task GetBooking_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var request = new RestRequest("/booking/999999999", Method.Get);

        // Act
        var response = await _client.ExecuteAsync<Booking>(request);

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        Assert.That(response.Data, Is.Null);
    }
}