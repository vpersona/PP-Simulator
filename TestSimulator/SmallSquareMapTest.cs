using Simulator.Maps;
using System;
using System.Drawing;
using System.Linq;
using Xunit;

namespace Simulator.Tests
{
    public class SmallSquareMapTests
    {
        private SmallSquareMap map;

        public SmallSquareMapTests()
        {
            // Tworzymy mapę o rozmiarze 10x10
            map = new SmallSquareMap(10);
        }

        [Fact]
        public void Test_Exist_ShouldReturnTrueForValidPoint()
        {
            var validPoint = new Point(5, 5);

            var result = map.Exist(validPoint);

            Assert.True(result, "Point should exist within the map bounds.");
        }

        [Fact]
        public void Test_Exist_ShouldReturnFalseForInvalidPoint()
        {
            var invalidPoint = new Point(10, 10);

            var result = map.Exist(invalidPoint);

            Assert.False(result, "Point should not exist outside the map bounds.");
        }

        [Fact]
        public void Test_Next_ShouldReturnNextPointWhenValid()
        {
            var currentPoint = new Point(4, 4);
            var direction = Direction.Up; // Przykład ruchu w górę

            var nextPoint = map.Next(currentPoint, direction);

            Assert.Equal(new Point(4, 5), nextPoint); // Następny punkt powinien być (4, 5)
        }

        [Fact]
        public void Test_Next_ShouldReturnSamePointWhenOutOfBounds()
        {
            var currentPoint = new Point(0, 0);
            var direction = Direction.Left; // Przechodzimy poza granice mapy w lewo

            var nextPoint = map.Next(currentPoint, direction);

            Assert.Equal(currentPoint, nextPoint); // Punkt powinien pozostać taki sam
        }

        [Fact]
        public void Test_NextDiagonal_ShouldReturnNextDiagonalPointWhenValid()
        {
            var currentPoint = new Point(5, 5);
            var direction = Direction.Up; //diagonal move: up

            var nextPoint = map.NextDiagonal(currentPoint, direction);

            Assert.Equal(new Point(6, 6), nextPoint); 
        }

        [Fact]
        public void Test_NextDiagonal_ShouldReturnSamePointWhenOutOfBounds()
        {
            var currentPoint = new Point(0, 0);
            var direction = Direction.Down; // outside bound

            var nextPoint = map.NextDiagonal(currentPoint, direction);

            Assert.Equal(currentPoint, nextPoint); // point is the same as it was
        }

        [Fact]
        public void Test_Constructor_ShouldThrowExceptionForInvalidSize()
        {
            // chceck if Constructor throws exception for the wrong size
            Assert.Throws<ArgumentOutOfRangeException>(() => new SmallSquareMap(4));
            Assert.Throws<ArgumentOutOfRangeException>(() => new SmallSquareMap(21));
        }

        [Fact]
        public void Test_DirectionParser_ShouldParseValidInput()
        {
            var input = "UDLR";
            var expectedDirections = new Direction[] { Direction.Up, Direction.Down, Direction.Left, Direction.Right };

            var result = DirectionParser.Parse(input);

            Assert.Equal(expectedDirections, result);
        }

        [Fact]
        public void Test_DirectionParser_ShouldReturnEmptyForInvalidInput()
        {
            var input = "XYZ"; // incorrect/invalid characters

            var result = DirectionParser.Parse(input);

            Assert.Empty(result); 
        }

        [Fact]
        public void Test_DirectionParser_ShouldHandleMixedCase()
        {
            var input = "uDlR"; // directions in mixed case

            var expectedDirections = new Direction[] { Direction.Up, Direction.Down, Direction.Left, Direction.Right };

            var result = DirectionParser.Parse(input);

            Assert.Equal(expectedDirections, result);
        }
    }
}
