using System.Net;
using RestSharp;
using RestfulBookerTests.Models;

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
    public async Task GetBooking_WithValidId_ReturnsBookingData()
    {
        // Arrange
        const int existingBookingId = 3;
        var request = new RestRequest($"/booking/{existingBookingId}", Method.Get);

        // Act
        var response = await _client.ExecuteAsync<Booking>(request);

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(response.Data, Is.Not.Null);
        Assert.That(response.Data.Firstname, Is.Not.Null.And.Not.Empty);
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
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(response.Data, Is.Not.Null);
        Assert.That(response.Data.Bookingid, Is.GreaterThan(0));
        Assert.That(response.Data.Booking.Firstname, Is.EqualTo(newBooking.Firstname));
        Assert.That(response.Data.Booking.Lastname, Is.EqualTo(newBooking.Lastname));
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


    [Test]
    public async Task FullLifecycle_CreateReadDeleteVerify_AllStepsSucceed()
    {
        // Step 1: Authenticate

        // Arrange
        var authBody = new AuthRequest
        {
            Username = "admin",
            Password = "password123"
        };
        var authRequest = new RestRequest("/auth", Method.Post);
        authRequest.AddJsonBody(authBody);

        // Act
        var authResponse = await _client.ExecuteAsync<AuthResponse>(authRequest);
        var token = authResponse.Data.Token;

        // Assert
        Assert.That(token, Is.Not.Null);
        Assert.That(token, Is.Not.Empty);

        // Step 2: Create booking

        // Arrange
        var newBooking = new Booking
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
        var createRequest = new RestRequest("/booking", Method.Post);
        createRequest.AddJsonBody(newBooking);

        // Act
        var createResponse = await _client.ExecuteAsync<CreateBookingResponse>(createRequest);
        var bookingId = createResponse.Data.Bookingid;

        // Assert
        Assert.That(createResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(createResponse.Data, Is.Not.Null);
        Assert.That(createResponse.Data.Bookingid, Is.GreaterThan(0));
        Assert.That(createResponse.Data.Booking.Firstname, Is.EqualTo(newBooking.Firstname));
        Assert.That(createResponse.Data.Booking.Lastname, Is.EqualTo(newBooking.Lastname));
        Assert.That(createResponse.Data.Booking.Totalprice, Is.EqualTo(newBooking.Totalprice));
        Assert.That(createResponse.Data.Booking.Depositpaid, Is.EqualTo(newBooking.Depositpaid));
        Assert.That(createResponse.Data.Booking.Additionalneeds, Is.EqualTo(newBooking.Additionalneeds));


        // Step 3: Verify create - GET booking

        // Arrange
        var getRequest = new RestRequest($"/booking/{bookingId}", Method.Get);

        // Act
        var getResponse = await _client.ExecuteAsync<Booking>(getRequest);

        // Assert
        Assert.That(getResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(getResponse.Data, Is.Not.Null);
        Assert.That(getResponse.Data.Firstname, Is.EqualTo(newBooking.Firstname));
        Assert.That(getResponse.Data.Lastname, Is.EqualTo(newBooking.Lastname));
        Assert.That(getResponse.Data.Additionalneeds, Is.EqualTo(newBooking.Additionalneeds));
        Assert.That(getResponse.Data.Totalprice, Is.EqualTo(newBooking.Totalprice));
        Assert.That(getResponse.Data.Depositpaid, Is.EqualTo(newBooking.Depositpaid));
        Assert.That(getResponse.Data.Bookingdates.Checkin, Is.EqualTo(newBooking.Bookingdates.Checkin));
        Assert.That(getResponse.Data.Bookingdates.Checkout, Is.EqualTo(newBooking.Bookingdates.Checkout));

        // Step 4: Delete booking

        // Arrange
        var deleteRequest = new RestRequest($"/booking/{bookingId}", Method.Delete);
        deleteRequest.AddHeader("Cookie", $"token={token}");

        // Act
        var deleteResponse = await _client.ExecuteAsync(deleteRequest);

        // Assert
        Assert.That(deleteResponse.StatusCode, Is.EqualTo(HttpStatusCode.Created));


        // Step 5: Verify delete - GET should return 404

        // Arrange
        var verifyDeleteRequest = new RestRequest($"/booking/{bookingId}", Method.Get);

        // Act
        var verifyDeleteResponse = await _client.ExecuteAsync<Booking>(verifyDeleteRequest);

        // Assert 
        Assert.That(verifyDeleteResponse.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        Assert.That(verifyDeleteResponse.Data, Is.Null);
    }
}