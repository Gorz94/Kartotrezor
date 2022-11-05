using FluentAssertions;
using Kartotrezor.Model.Entities;
using Kartotrezor.Utils;
using Xunit;

namespace Kartotest.Tests.Other
{
    public class Turn_should
    {
        [Theory]
        [InlineData(Direction.W, Turn.D, Direction.N)]
        [InlineData(Direction.W, Turn.G, Direction.S)]

        [InlineData(Direction.S, Turn.D, Direction.W)]
        [InlineData(Direction.S, Turn.G, Direction.E)]

        [InlineData(Direction.E, Turn.D, Direction.S)]
        [InlineData(Direction.E, Turn.G, Direction.N)]

        [InlineData(Direction.N, Turn.D, Direction.E)]
        [InlineData(Direction.N, Turn.G, Direction.W)]
        public void Turn_should_change_Direction(Direction start, Turn turn, Direction to)
        {
            to.Should().Be(start.Turn(turn));
        }
    }
}
