using FluentAssertions;

namespace RestfulBookerTests;

[NonParallelizable]
[TestFixture]
public class FluentAssertionsDemo
{
    [Test]
    public void Collection_HaveCount_ChecksCollectionSize()
    {
        // Arrange
        var topics = new List<string> { "bg.live.event", "bg.live.command", "feeds-status" };

        // Act & Assert
        topics.Should().HaveCount(3);
    }

    [Test]
    public void Collection_Contain_ChecksElementPresence()
    {
        // Arrange
        var topics = new List<string> { "bg.live.event", "bg.live.command", "feeds-status" };

        // Act & Assert
        topics.Should().ContainInOrder("bg.live.event", "bg.live.command", "feeds-status");
    }

    [Test]
    public void Exception_Throw_CatchesThrownException()
    {
        // Arrange
        Action act = () => throw new InvalidOperationException("test error");

        // Act & Assert
        act.Should().Throw<InvalidOperationException>()
            .WithMessage("test error");
    }

    [Test]
    public void Exception_NotThrow_ValidActionCompletes()
    {
        // Arrange
        Action act = () => { };

        // Act & Assert
        act.Should().NotThrow();
    }
}