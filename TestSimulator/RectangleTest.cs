using System;
using Xunit;
using Simulator;

namespace SimulatorTests
{
    public class RectangleTest
    {
        [Fact]
        public void Constructor_ShouldInitializeCorrectly_IfCoordinatesAreCorrect()
        {
            // Arrange and act
            var rect = new Rectangle(2, 3, 5, 7);

            // Assert
            Assert.Equal(2, rect.X1);
            Assert.Equal(3, rect.Y1);
            Assert.Equal(5, rect.X2);
            Assert.Equal(7, rect.Y2);
        }

        [Fact]
        public void Constructor_ShouldReorderCoordinates_IfCoordinatesAreSwapped()
        {
            // Arrange and act
            var rect = new Rectangle(5, 7, 2, 3);

            // Assert
            Assert.Equal(2, rect.X1);
            Assert.Equal(3, rect.Y1);
            Assert.Equal(5, rect.X2);
            Assert.Equal(7, rect.Y2);
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenRectangleIsSlim()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Rectangle(2, 3, 2, 7));
        }

        [Fact]
        public void Contains_ShouldReturnTrue_IfPointIsInsideRectangle()
        {
            // Arrange
            var rect = new Rectangle(2, 3, 5, 7);
            var point = new Point(3, 4);

            // Act
            var result = rect.Contains(point);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Contains_ShouldReturnFalse_IfPointIsOutOfRectangle()
        {
            // Arrange
            var rect = new Rectangle(2, 3, 5, 7);
            var point = new Point(6, 8);

            // Act
            var result = rect.Contains(point);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Contains_ShouldReturnTrue_WhenPointIsOnEdgeOfRectangle()
        {
            // Arrange
            var rect = new Rectangle(2, 3, 5, 7);
            var point = new Point(2, 3); // top-left corner

            // Act
            var result = rect.Contains(point);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void ToString_ShouldReturnCorrectStringForm()
        {
            // Arrange
            var rect = new Rectangle(2, 3, 5, 7);

            // Act
            var result = rect.ToString();

            // Assert
            Assert.Equal("(2,3):(5,7)", result);
        }
    }

    public class PointTest
    {
        [Fact]
        public void Constructor_ShouldInitializeCorrectly()
        {
            // Arrange and act
            var point = new Point(3, 4);

            // Assert
            Assert.Equal(3, point.X);
            Assert.Equal(4, point.Y);
        }

        [Theory]
        [InlineData(Direction.Up, 3, 5)]
        [InlineData(Direction.Down, 3, 3)]
        [InlineData(Direction.Left, 2, 4)]
        [InlineData(Direction.Right, 4, 4)]
        public void Next_ShouldReturnCorrectPoint_WhenDirectionIsGiven(Direction direction, int expectedX, int expectedY)
        {
            // Arrange
            var point = new Point(3, 4);

            // Act
            var result = point.Next(direction);

            // Assert
            Assert.Equal(expectedX, result.X);
            Assert.Equal(expectedY, result.Y);
        }

        [Theory]
        [InlineData(Direction.Up, 4, 5)]
        [InlineData(Direction.Down, 2, 3)]
        [InlineData(Direction.Right, 4, 3)]
        [InlineData(Direction.Left, 2, 5)]
        public void NextDiagonal_ShouldReturnCorrectPoint_WhenDirectionIsGiven(Direction direction, int expectedX, int expectedY)
        {
            // Arrange
            var point = new Point(3, 4);

            // Act
            var result = point.NextDiagonal(direction);

            // Assert
            Assert.Equal(expectedX, result.X);
            Assert.Equal(expectedY, result.Y);
        }

        [Fact]
        public void ToString_ShouldReturnCorrectStringForm()
        {
            // Arrange
            var point = new Point(3, 4);

            // Act
            var result = point.ToString();

            // Assert
            Assert.Equal("(3, 4)", result);
        }
    }
}