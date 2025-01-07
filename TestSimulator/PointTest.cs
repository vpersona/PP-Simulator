using Simulator;
using Xunit;

namespace TestSimulator;

public class PointTests
{
    [Theory]
    [InlineData(0, 0, Direction.Up, 0, 1)]
    [InlineData(0, 0, Direction.Down, 0, -1)]
    [InlineData(0, 0, Direction.Left, -1, 0)]
    [InlineData(0, 0, Direction.Right, 1, 0)]
    public void Next_ShouldReturnCorrectPoint(int startX, int startY, Direction direction, int expectedX, int expectedY)
    {
        // Arrange
        var point = new Point(startX, startY);

        // Act
        var result = point.Next(direction);

        // Assert
        Assert.Equal(expectedX, result.X);
        Assert.Equal(expectedY, result.Y);
    }

    [Theory]
    [InlineData(0, 0, Direction.Up, 1, 1)] 
    [InlineData(0, 0, Direction.Down, -1, -1)] 
    [InlineData(0, 0, Direction.Left, -1, 1)] 
    [InlineData(0, 0, Direction.Right, 1, -1)] 
    public void NextDiagonal_ShouldReturnCorrectPoint(int startX, int startY, Direction direction, int expectedX, int expectedY)
    {
        // Arrange
        var point = new Point(startX, startY);

        // Act
        var result = point.NextDiagonal(direction);

        // Assert
        Assert.Equal(expectedX, result.X);
        Assert.Equal(expectedY, result.Y);
    }
}
