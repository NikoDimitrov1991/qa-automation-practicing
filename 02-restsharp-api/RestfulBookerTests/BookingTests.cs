using System.Net;
using RestSharp;
using RestfulBookerTests.Models;
using FluentAssertions;

namespace RestfulBookerTests;

[NonParallelizable]
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
    public async Task GetBooking_AfterCreation_ReturnsCorrectData()
    {
        // Arrange - create a fresh booking to guarantee it exists
        var newBooking = BuildTestBooking();
        var created = await CreateBooking(newBooking);

        // Act
        var request = new RestRequest($"/booking/{created.Bookingid}", Method.Get);
        var response = await _client.ExecuteAsync<Booking>(request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        response.Data.Should().NotBeNull();
        response.Data.Should().BeEquivalentTo(newBooking);
    }

    [Test]
    public async Task PostBooking_WithValidData_CreatesAndReturnsBookingId()
    {
        // Arrange
        var newBooking = new Booking
        {
            Firstname = "Niko",
            Lastname = "Dimitrov",
            Totalprice = 240,
            Depositpaid = true,
            Bookingdates = new BookingDates
            {
                Checkin = "2026-08-01",
                Checkout = "2026-08-10"
            },
            Additionalneeds = "Breakfast"
        };
        var request = new RestRequest("/booking", Method.Post);
        request.AddJsonBody(newBooking);

        // Act
        var response = await _client.ExecuteAsync<CreateBookingResponse>(request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        response.Data.Should().NotBeNull();
        response.Data.Booking.Should().BeEquivalentTo(newBooking);
    }

    [Test]
    public async Task GetBooking_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var request = new RestRequest("/booking/999999999", Method.Get);

        // Act
        var response = await _client.ExecuteAsync<Booking>(request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        response.Data.Should().BeNull();
    }


    [Test]
    public async Task FullLifecycle_CreateReadDeleteVerify_AllStepsSucceed()
    {
        // Step 1: Authenticate
        var token = await AuthenticateAndGetToken();
        token.Should().NotBeNullOrEmpty();

        // Step 2: Create booking
        var newBooking = BuildTestBooking();
        var created = await CreateBooking(newBooking);
        var bookingId = created.Bookingid;

        created.Should().NotBeNull();
        bookingId.Should().BeGreaterThan(0);
        created.Booking.Should().BeEquivalentTo(newBooking);


        // Step 3: Verify create - GET booking

        // Arrange
        var getRequest = new RestRequest($"/booking/{bookingId}", Method.Get);

        // Act
        var getResponse = await _client.ExecuteAsync<Booking>(getRequest);

        // Assert
        getResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        getResponse.Data.Should().NotBeNull();
        getResponse.Data.Should().BeEquivalentTo(newBooking);

        // Step 4: Delete booking

        // Arrange
        var deleteRequest = new RestRequest($"/booking/{bookingId}", Method.Delete);
        deleteRequest.AddHeader("Cookie", $"token={token}");

        // Act
        var deleteResponse = await _client.ExecuteAsync(deleteRequest);

        // Assert
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.Created);


        // Step 5: Verify delete - GET should return 404

        // Arrange
        var verifyDeleteRequest = new RestRequest($"/booking/{bookingId}", Method.Get);

        // Act
        var verifyDeleteResponse = await _client.ExecuteAsync<Booking>(verifyDeleteRequest);

        // Assert 
        verifyDeleteResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        verifyDeleteResponse.Data.Should().BeNull();
    }

    private Booking BuildTestBooking()
    {
        return new Booking
        {
            Firstname = "Nikolay",
            Lastname = "Dim",
            Totalprice = 240,
            Depositpaid = true,
            Bookingdates = new BookingDates
            {
                Checkin = "2026-08-01",
                Checkout = "2026-08-10"
            },
            Additionalneeds = "Dinner"
        };
    }

    private async Task<CreateBookingResponse> CreateBooking(Booking booking)
    {
        var request = new RestRequest("/booking", Method.Post);
        request.AddJsonBody(booking);
        var response = await _client.ExecuteAsync<CreateBookingResponse>(request);
        return response.Data;
    }

    private async Task<string> AuthenticateAndGetToken()
    {
        var authBody = new AuthRequest
        {
            Username = "admin",
            Password = "password123"
        };
        var request = new RestRequest("/auth", Method.Post);
        request.AddJsonBody(authBody);
        var response = await _client.ExecuteAsync<AuthResponse>(request);
        return response.Data.Token;
    }
}