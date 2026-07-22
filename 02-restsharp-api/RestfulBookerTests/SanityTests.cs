namespace RestfulBookerTests;

[TestFixture]
public class SanityTests
{
    [Test]
    public void Addition_TwoPlusTwo_ReturnsFour()
    {
        // Arrange
        int a = 2;
        int b = 2;
        int expected = 4;

        // Act
        int result = a + b;

        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void StringLength_HelloWorld_Returns11()
    {
        // Arrange
        string text = "Hello World";

        // Act
        int length = text.Length;
        int expected = 11;

        // Assert
        Assert.That(length, Is.EqualTo(expected));
    }

    [Test]
    public void StringContains_Kafka_ReturnsTrue()
    {
        // Arrange
        string topic = "bg.live.kafka.event";

        // Act
        bool containsKafka = topic.Contains("kafka");

        // Assert
        Assert.That(containsKafka, Is.True);
    }
}